using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Tony.FileTransfer.Core.Common;
using Tony.FileTransfer.Core.TableModel;
using Tony.FileTransfer.Server.DB;
using Tony.FileTransfer.Server.State;

namespace Tony.FileTransfer.Server.Services
{
    public class FileUploadService : IFileUpload.IFileUploadBase
    {
        private ILogger logger;
        public FileUploadService(ILogger logger)
        {
            this.logger = logger;
        }

        public override Task<CommonResponse> CheckFileExist(CheckFileExistRequest request, ServerCallContext context)
        {
            logger.LogInformation($"start CheckFileExist md5[{request.Md5}]");
            bool result = false;
            try
            {
                using (var dbContext = new ServerDBContext())
                {
                    result = dbContext.FileInfos.Any(x => x.Md5 == request.Md5);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, $"parameter md5[{request.Md5}]");
                return Task.FromResult(new CommonResponse() { Result = false, ErrorCode = ErrorCodes.DataBaseError });
            }
            return Task.FromResult(new CommonResponse() { Result = result });
        }


        public override async Task UploadWithStream(IAsyncStreamReader<UploadWithStreamRequest> requestStream, IServerStreamWriter<CommonResponse> responseStream, ServerCallContext context)
        {
            //check call context
            if (!context.UserState.ContainsKey(ConstStr.MD5_KEY))
            {
                await responseStream.WriteAsync(new CommonResponse() { Result = false, ErrorCode = ErrorCodes.BadCallContext });
                return;
            }

            //check the file's md5 in cache
            string md5 = context.UserState.ContainsKey(ConstStr.MD5_KEY).ToString();
            if (UploadStateManager.Instance.UploadStateCache.ContainsKey(md5))
            {
                await responseStream.WriteAsync(new CommonResponse() { Result = false, ErrorCode = ErrorCodes.FileAlreadyExistInServerCache });
                return;
            }

            //check the file's md5 in db.if the file's md5 is in db ,the file is already saved in server
            using (var dbContext = new ServerDBContext())
            {
                if (dbContext.FileInfos.Any(x => x.Md5 == md5))
                {
                    await responseStream.WriteAsync(new CommonResponse() { Result = false, ErrorCode = ErrorCodes.FileAlreadyExistInServerDb });
                    return;
                }
            }

            string cacheFilePath = GetCacheFilePath(md5);

            logger.LogDebug($"start save file ,cache path[{cacheFilePath}]");

            //save cache
            UploadStateManager.Instance.UploadStateCache.Add(md5, new UploadState() { HasUploadedByte = 0, IsUploadFinish = false, Md5 = md5, CacheFilePath = cacheFilePath });

            try
            {
                using (var fileStream = new FileStream(cacheFilePath, FileMode.Create, FileAccess.Write))
                {
                    //save file 
                    while (await requestStream.MoveNext())
                    {
                        var request = requestStream.Current;
                        var bytes = request.Data.ToArray();
                        fileStream.Write(bytes, 0, bytes.Length);
                        //update cache
                        UploadStateManager.Instance.UploadStateCache[md5].HasUploadedByte += bytes.Length;
                        await responseStream.WriteAsync(new CommonResponse() { Result = true });
                    }
                }

            }
            catch (Exception e)
            {
                logger.LogError(e, "upload error");
            }
        }


        public override Task<CommonResponse> FinishUpload(FinishUploadRequest request, ServerCallContext context)
        {
            using (var dbContext = new ServerDBContext())
            {
                if (request.IsFastUpload)
                {
                    return Task.FromResult(FinshFastUpload(request));
                }
                else
                {
                    return Task.FromResult(FinishNormalUpload(request));
                }
            }
        }

        private CommonResponse FinshFastUpload(FinishUploadRequest request)
        {
            ErrorCodes errorCode = ErrorCodes.NoError;
            if (!CheckUploadFinishInfo(request.Md5, request.RecognizeId, request.UserName, out errorCode))
            {
                return new CommonResponse() { Result = false, ErrorCode = errorCode };
            }
            using (var dbContext = new ServerDBContext())
            {
                var fileInfo = dbContext.FileInfos.FirstOrDefault(x => x.Md5 == request.Md5);
                
                var machineInfo = dbContext.MachineInfos.FirstOrDefault(x => x.RecognizeId == request.RecognizeId);
                
                if (!string.IsNullOrEmpty(request.UserName))
                {
                    var userInfo = dbContext.UserInfos.FirstOrDefault(x => x.UserName == request.UserName);
                   
                    var userMachineInfo = dbContext.UserMachineInfos.Where(x => x.MachineId == machineInfo.Id && x.UserId == userInfo.Id);
                   
                    //update File 
                    AddOrUpdateUserFile(userInfo.Id, fileInfo.Id, request.ClientFileName);
                }
                return new CommonResponse() { Result = AddOrUpdateMachineFile(machineInfo.Id, fileInfo.Id, request.ClientFileName) };
            }
        }
        private CommonResponse FinishNormalUpload(FinishUploadRequest request)
        {
            ErrorCodes errorCode = ErrorCodes.NoError;
            if (!CheckUploadFinishInfo(request.Md5, request.RecognizeId, request.UserName, out errorCode))
            {
                return new CommonResponse() { Result = false, ErrorCode = errorCode };
            }

            //change cache file name
            if (UploadStateManager.Instance.UploadStateCache.ContainsKey(request.Md5))
            {
                return new CommonResponse() { Result = false, ErrorCode = ErrorCodes.FileNotExistInServerCache };   
            }
            var state = UploadStateManager.Instance.UploadStateCache[request.Md5];
            string cachePath = state.CacheFilePath;
            if (!File.Exists(cachePath))
            {
                return new CommonResponse() { Result = false, ErrorCode = ErrorCodes.CacheFileNotExist };
            }
            string newFilePath = Path.Combine(Path.GetDirectoryName(cachePath), request.Md5);
            File.Move(cachePath,newFilePath,true);

            //compare md5
            if (Helper.GetMD5HashFromFile(newFilePath) != request.Md5)
            {
                return new CommonResponse() { Result = false, ErrorCode = ErrorCodes.Md5CompareError };
            }

            using (var dbContext = new ServerDBContext())
            {
                ServerFile fileInfo = new ServerFile()
                {
                    CreateTime = DateTime.Now,
                    Md5 = request.Md5,
                    ServerPath = newFilePath,
                };
                dbContext.Add(fileInfo);

                var machineInfo = dbContext.MachineInfos.FirstOrDefault(x => x.RecognizeId == request.RecognizeId);

                if (!string.IsNullOrEmpty(request.UserName))
                {
                    var userInfo = dbContext.UserInfos.FirstOrDefault(x => x.UserName == request.UserName);

                    var userMachineInfo = dbContext.UserMachineInfos.Where(x => x.MachineId == machineInfo.Id && x.UserId == userInfo.Id);

                    //update File 
                    AddOrUpdateUserFile(userInfo.Id, fileInfo.Id, request.ClientFileName);
                }
                return new CommonResponse() { Result = AddOrUpdateMachineFile(machineInfo.Id, fileInfo.Id, request.ClientFileName) };
            }
        }

        private bool CheckUploadFinishInfo(string md5, int recognizedId, string userName, out ErrorCodes errorCode)
        {
            using (var dbContext = new ServerDBContext())
            {
                var fileInfo = dbContext.FileInfos.FirstOrDefault(x => x.Md5 == md5);
                if (fileInfo == null)
                {
                    errorCode = ErrorCodes.FileNotExistInServerDb;
                    return false;
                }
                //check RecognizeId
                var machineInfo = dbContext.MachineInfos.FirstOrDefault(x => x.RecognizeId == recognizedId);
                if (machineInfo == null)
                {
                    errorCode = ErrorCodes.RecognizeIdNotExist;
                    return false;
                }
                //check user has login
                if (!string.IsNullOrEmpty(userName))
                {
                    var userInfo = dbContext.UserInfos.FirstOrDefault(x => x.UserName == userName);
                    if (userInfo == null)
                    {
                        errorCode = ErrorCodes.UserNotExist;
                        return false;
                    }
                    //check user and machine info
                    var userMachineInfo = dbContext.UserMachineInfos.Where(x => x.MachineId == machineInfo.Id && x.UserId == userInfo.Id);
                    if (userMachineInfo == null)
                    {
                        errorCode = ErrorCodes.UserMachineNotBind;
                        return false;
                    }
                }
                errorCode = ErrorCodes.NoError;
                return true;
            }
        }

        private string GetCacheFilePath(string md5)
        {
            string cacheDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");
            if (!Directory.Exists(cacheDir)) Directory.CreateDirectory(cacheDir);

            return Path.Combine(cacheDir, md5 + ".tonycachefile");
        }

        private bool AddOrUpdateUserFile(int userId, int fileId, string fileName)
        {
            using (var dbContext = new ServerDBContext())
            {
                var userFileInfo = dbContext.UserFileInfos.FirstOrDefault(x => x.UserId == userId && x.FileId == fileId && x.FileName.Contains(Path.GetFileName(fileName)));
                if (userFileInfo != null)
                {
                    //update
                    dbContext.Update(userFileInfo);
                }
                else
                {
                    dbContext.Add(new User_FileInfo()
                    {
                        CreateDate = DateTime.Now,
                        FileId = fileId,
                        FileName = fileName,
                        UserId = userId
                    });
                }
                return dbContext.SaveChanges() > 1;
            }
        }
        private bool AddOrUpdateMachineFile(int machineId, int fileId, string fileName)
        {
            using (var dbContext = new ServerDBContext())
            {
                var machineFileInfo = dbContext.MachineFileInfos.FirstOrDefault(x => x.Id == machineId && x.FileId == fileId && x.FileName.Contains(Path.GetFileName(fileName)));
                if (machineFileInfo != null)
                {
                    //update
                    dbContext.Update(machineFileInfo);
                }
                else
                {
                    dbContext.Add(new Machine_FileInfo()
                    {
                        CreateDate = DateTime.Now,
                        FileId = fileId,
                        FileName = fileName,
                        MachineId = machineId
                    });
                }
                return dbContext.SaveChanges() > 1;
            }
        }
    }
}
