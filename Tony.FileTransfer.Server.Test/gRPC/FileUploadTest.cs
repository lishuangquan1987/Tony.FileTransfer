using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Tony.FileTransfer.Core.Common;
using Xunit;

namespace Tony.FileTransfer.Server.Test.gRPC
{
    public class FileUploadTest
    {
        IFileUpload.IFileUploadClient client;
        public FileUploadTest()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001",new GrpcChannelOptions() { MaxSendMessageSize=int.MaxValue, MaxReceiveMessageSize=int.MaxValue});
            client = new IFileUpload.IFileUploadClient(channel);
        }
        [Fact]
        public async void UploadFileTest1()
        {
            var call = client.UploadWithStream();
            for (int i = 0; i < 100; i++)
            {
                await call.RequestStream.WriteAsync(new UploadWithStreamRequest() { });
            }
            await call.RequestStream.CompleteAsync();
        }
        [Fact]
        public  void UploadFileTest()
        {
            UploadFile(@"E:\DownLoad\dbeaver-ce-7.2.4-x86_64-setup.exe");
        }
        [Fact]
        public void UploadFileTest2()
        {
            UploadFile(@"E:\DownLoad\【许仔诚品】PotPlayerSetup64_108.zip");
        }
        private async void UploadFile(string fileName)
        {
            var md5 = Helper.GetMD5HashFromFile(fileName);
            var checkFileResult = client.CheckFileExist(new CheckFileExistRequest() { Md5 = md5 });
            if (!checkFileResult.Result)
            {
                if (checkFileResult.ErrorCode != ErrorCodes.NoError)
                {
                    Console.WriteLine($"check file:{fileName} error,error code:{checkFileResult.ErrorCode}");
                    return;
                }
                CancellationTokenSource tokenSource = new CancellationTokenSource() { };
                var header = new Grpc.Core.Metadata();
                header.Add(Const.MD5_KEY, md5);
                var call = client.UploadWithStream(cancellationToken: tokenSource.Token, headers: header);
                var result = await call.ResponseStream.MoveNext(tokenSource.Token);

                int size = 102400;
                long totalLenth = new FileInfo(fileName).Length;
                long hasUploadedLenth = 0;

                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    while (hasUploadedLenth < totalLenth)
                    {
                        byte[] bytes = new byte[size];
                        var count = fs.Read(bytes, 0, size);
                        if (count > 0)
                        {
                            var bytesToSend = new byte[count];
                            Array.Copy(bytes, 0, bytesToSend, 0, count);

                            await call.RequestStream.WriteAsync(new UploadWithStreamRequest() { Data = Google.Protobuf.ByteString.CopyFrom(bytesToSend) });

                            if (await call.ResponseStream.MoveNext(tokenSource.Token))
                            {
                                var response = call.ResponseStream.Current;

                                if (!response.Result)
                                {
                                    Console.WriteLine("传输失败：" + response.ErrorCode);
                                    break;
                                }
                            }
                            hasUploadedLenth += count;
                        }
                    }
                }

                await call.RequestStream.CompleteAsync();

                if (hasUploadedLenth == totalLenth)
                {
                    client.FinishUpload(new FinishUploadRequest()
                    {
                        ClientFileName = fileName,
                        IsFastUpload = false,
                        Md5 = md5,
                        RecognizeId = 123456789,
                    });
                }
            }
            else
            {
                client.FinishUpload(new FinishUploadRequest()
                {
                    ClientFileName = fileName + "test",
                    IsFastUpload = true,
                    Md5 = md5,
                    RecognizeId = 123456789,
                });
            }
        }
    }
}
