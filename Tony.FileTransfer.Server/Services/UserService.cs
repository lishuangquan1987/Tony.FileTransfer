using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony.FileTransfer.Core.Common;
using Tony.FileTransfer.Core.TableModel;
using Tony.FileTransfer.Server.DB;

namespace Tony.FileTransfer.Server.Services
{
    public class UserService : IUserService.IUserServiceBase
    {
        private ILogger<UserService> logger;
        private Func<ServerDBContext> createDBContext;
        private Random random = new Random();

        public UserService(ILogger<UserService> logger, Func<ServerDBContext> createDBContext)
        {
            this.logger = logger;
            this.createDBContext = createDBContext;
        }
        public override Task<ClientFileInfosResponse> GetFilesByRecognizedId(CommonRequest request, ServerCallContext context)
        {
            if (string.IsNullOrEmpty(request.Message) || int.TryParse(request.Message, out int recognizeId))
            {
                return Task.FromResult<ClientFileInfosResponse>(new ClientFileInfosResponse() { Result = new CommonResponse() { Result = false, ErrorCode = ErrorCodes.InValidRecognizeId } });
            }
            using (var db = createDBContext())
            {
                var machineId = db.MachineInfos.FirstOrDefault(x => x.RecognizeId == recognizeId)?.Id;
                if (!machineId.HasValue)
                {
                    return Task.FromResult(new ClientFileInfosResponse() { Result = new CommonResponse() { Result = false, ErrorCode = ErrorCodes.RecognizeIdNotExist } });
                }
                var response = new ClientFileInfosResponse();
                response.Result = new CommonResponse() { Result=true};
                response.ClientFileInfos.Add(GetFilesByMachineId(machineId.Value));
                return Task.FromResult(response);
            }
        }
        public override Task<CommonResponseWithMessage> GetRecognizedId(CommonRequest request, ServerCallContext context)
        {
            string machineIdentity = request.Message;
            if (string.IsNullOrEmpty(machineIdentity)||machineIdentity.Length!=16)
            {
                return Task.FromResult(new CommonResponseWithMessage() { Result = new CommonResponse() { Result = false, ErrorCode = ErrorCodes.InValidMachineIdentity } });
            }


            using (var dbContext = createDBContext())
            {
                var machineInfo = dbContext.MachineInfos.FirstOrDefault(x => x.MachineIdentity == machineIdentity);
                if (machineInfo != null)
                {
                    return Task.FromResult(new CommonResponseWithMessage() { Result = new CommonResponse() { Result = true }, Message = System.Text.Json.JsonSerializer.Serialize(new {RecognizeId=machineInfo.RecognizeId,Password=machineInfo.Password }) });
                }

                machineInfo = new MachineInfo()
                {
                    MachineIdentity = machineIdentity,
                    RecognizeId = GenerateRandomRecognizeId(),
                    Password = GenerateNumber(4).ToString()
                };
                dbContext.Add(machineInfo);
                if (dbContext.SaveChanges() > 0)
                {
                    return Task.FromResult(new CommonResponseWithMessage() { Result = new CommonResponse() { Result = true }, Message = System.Text.Json.JsonSerializer.Serialize(new { machineInfo.RecognizeId, machineInfo.Password }) });
                }
                else
                {
                    return Task.FromResult(new CommonResponseWithMessage() { Result = new CommonResponse() { Result = false, ErrorCode = ErrorCodes.DataBaseError } });
                }
            }
        }
        public override Task<ClientFileInfosResponse> GetFilesByUserId(CommonRequest request, ServerCallContext context)
        {
            if (string.IsNullOrEmpty(request.Message) || int.TryParse(request.Message, out int userId))
            {
                return Task.FromResult<ClientFileInfosResponse>(new ClientFileInfosResponse() { Result = new CommonResponse() { Result = false, ErrorCode = ErrorCodes.InValidUserId } });
            }
            using (var db = createDBContext())
            {
                var user = db.UserInfos.FirstOrDefault(x => x.Id == userId);
                if (user==null)
                {
                    return Task.FromResult(new ClientFileInfosResponse() { Result = new CommonResponse() { Result = false, ErrorCode = ErrorCodes.UserNotExist } });
                }

                var response = new ClientFileInfosResponse();
                response.Result = new CommonResponse() { Result = true };
                response.ClientFileInfos.Add(GetFilesByUserId(userId));
                return Task.FromResult(response);
            }
        }
        public override Task<CommonResponseWithMessage> Login(CommonRequest request, ServerCallContext context)
        {
            var doc = System.Text.Json.JsonDocument.Parse(request.Message);
            string userName = doc.RootElement.GetProperty("UserName").GetString();
            string password = doc.RootElement.GetProperty("Password").GetString();
            using (var dbcontext = createDBContext())
            {
                var user = dbcontext.UserInfos.FirstOrDefault(x => x.UserName == userName && x.Password == password);
                if (user == null)
                {
                    return Task.FromResult(new CommonResponseWithMessage() { Result = new CommonResponse() { Result = false, ErrorCode = ErrorCodes.InValidUserNameOrPassword } });
                }

                return Task.FromResult(new CommonResponseWithMessage() { Result = new CommonResponse() { Result = true }, Message = System.Text.Json.JsonSerializer.Serialize(user) });
            }
        }
        public override Task<CommonResponse> MergeMachineFileToUserFile(MergeMachineFileToUserFileRequest request, ServerCallContext context)
        {
            using (var dbcontext = createDBContext())
            {
                //check recoginize id
                var machineInfo = dbcontext.MachineInfos.FirstOrDefault(x => x.RecognizeId == request.RecognizedId);
                if (machineInfo == null)
                {
                    return Task.FromResult(new CommonResponse() { Result=false,ErrorCode = ErrorCodes.RecognizeIdNotExist});
                }
                //check user id

                var userInfo = dbcontext.UserInfos.Where(x => x.Id == request.UserId);
                if (userInfo == null)
                {
                    return Task.FromResult(new CommonResponse() { Result = false, ErrorCode = ErrorCodes.UserNotExist });
                }

                var machineFileInfos = dbcontext.MachineFileInfos.Where(x => x.MachineId == machineInfo.Id).ToList();
                //check fileid exist
                machineFileInfos = machineFileInfos.Where(x => dbcontext.FileInfos.Any(y => y.Id == x.FileId)).ToList();

                if (machineFileInfos.Count > 0)
                {
                    foreach (var machineFile in machineFileInfos)
                    {
                        //if filename is same,file id is same,user id is same,then do not add
                        var userFileInfo = dbcontext.UserFileInfos.FirstOrDefault(x => x.FileId == machineFile.FileId && x.UserId==request.UserId && Path.GetFileName(x.FileName).Equals(Path.GetFileName(machineFile.FileName)));
                        if (userFileInfo == null)
                        {
                            dbcontext.UserFileInfos.Add(new User_FileInfo()
                            {
                                CreateDate = DateTime.Now,
                                FileId = machineFile.FileId,
                                FileName = machineFile.FileName,
                                UserId = request.UserId
                            });
                        }
                    }
                    dbcontext.SaveChanges();
                }

                return Task.FromResult(new CommonResponse() { Result = true, ErrorCode = ErrorCodes.NoError });
            }
        }

        public override Task<CommonResponse> ChangePasswordByRecognizedId(CommonRequest request, ServerCallContext context)
        {
            var doc = System.Text.Json.JsonDocument.Parse(request.Message);
            var recognizeId = doc.RootElement.GetProperty("RecognizeId").GetInt32();
            var newPassword = doc.RootElement.GetProperty("Password").GetString();
            using (var dbcontext = createDBContext())
            {
                var model = dbcontext.MachineInfos.Where(x => x.RecognizeId == recognizeId).FirstOrDefault();
                if (model == null)
                {
                    return Task.FromResult(new CommonResponse() { Result = false, ErrorCode = ErrorCodes.RecognizeIdNotExist });
                }

                model.Password = newPassword;
                var result = dbcontext.SaveChanges();

                return Task.FromResult(new CommonResponse() { Result = result > 0 ? true : false, ErrorCode = result > 0 ? ErrorCodes.NoError : ErrorCodes.DataBaseError });
            }
        }

        public override Task<CommonResponse> Register(CommonRequest request, ServerCallContext context)
        {
            var doc= System.Text.Json.JsonDocument.Parse(request.Message);
            string userName = doc.RootElement.GetProperty("UserName").GetString();
            string passWord = doc.RootElement.GetProperty("Password").GetString();

            if (string.IsNullOrEmpty(userName))
            {
                return Task.FromResult(new CommonResponse() { Result = false, ErrorCode = ErrorCodes.UserNameIsNull });
            }

            if (string.IsNullOrEmpty(passWord))
            {
                return Task.FromResult(new CommonResponse() { Result = false, ErrorCode = ErrorCodes.InValidPassword });
            }

            using (var dbcontext = createDBContext())
            {
                if (dbcontext.UserInfos.Any(x => x.UserName == userName))
                {
                    return Task.FromResult(new CommonResponse() { Result = false, ErrorCode = ErrorCodes.UserNameHasExist });
                }

                dbcontext.UserInfos.Add(new UserInfo()
                {
                    UserName = userName,
                    Password = passWord
                });
                var result = dbcontext.SaveChanges();
                if (result > 0)
                {
                    return Task.FromResult(new CommonResponse() { Result = true, ErrorCode = ErrorCodes.NoError });
                }
                else
                {
                    return Task.FromResult(new CommonResponse() { Result = false, ErrorCode = ErrorCodes.DataBaseError });
                }
            }
        }
        private IEnumerable<ClientFileInfo> GetFilesByMachineId(int machineId)
        {
            using (var dbContext = createDBContext())
            {
                var machine_files = dbContext.MachineFileInfos.Where(x => x.MachineId == machineId);
                foreach (var machine_file in machine_files)
                {
                    var fileInfo = dbContext.FileInfos.FirstOrDefault(x => x.Id == machine_file.FileId);
                    if (fileInfo == null) continue;
                    yield return new ClientFileInfo()
                    {
                        FileId = machine_file.FileId,
                        CreatedDate = fileInfo.CreateTime.HasValue ? Helper.ConvertTimeToLong(fileInfo.CreateTime.Value) : 0,
                        FileName = machine_file.FileName,
                        FileSize = fileInfo.Size
                    };
                }
            }
        }

        private int GenerateRandomRecognizeId()
        {
            using (var dbcontext = createDBContext())
            {
                int recognizeId = 0;
                while (true)
                {
                    recognizeId = GenerateNumber(9);
                    if (!dbcontext.MachineInfos.Any(x => x.RecognizeId == recognizeId))
                    {
                        break;
                    }
                }
                return recognizeId;
            }
        }
        private int GenerateNumber(int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                if (i == 0)
                {
                    sb.Append(random.Next(1, 9));
                }
                else
                {
                    sb.Append(random.Next(0, 9));
                }
            }
            return int.Parse(sb.ToString());
        }

        private IEnumerable<ClientFileInfo> GetFilesByUserId(int userId)
        {
            using (var dbcontext = createDBContext())
            {
                var list = from user in dbcontext.UserInfos

                           join userFileMap in dbcontext.UserFileInfos
                           on user.Id equals userFileMap.UserId

                           join file in dbcontext.FileInfos
                           on userFileMap.FileId equals file.Id

                           where user.Id == userId
                           select new ClientFileInfo()
                           {
                               CreatedDate = userFileMap.CreateDate.HasValue ? Helper.ConvertTimeToLong(userFileMap.CreateDate.Value) : 0,
                               FileId = userFileMap.FileId,
                               FileName = userFileMap.FileName,
                               FileSize = file.Size
                           };
                return list;
            }
        }
    }
}
