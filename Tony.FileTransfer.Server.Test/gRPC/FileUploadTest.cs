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
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
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
        public async void UploadFileTest()
        {
            string fileName = @"E:\DownLoad\elasticsearch-7.3.2-windows-x86_64.zip";
            var md5 = Helper.GetMD5HashFromFile(fileName);
            if (!client.CheckFileExist(new CheckFileExistRequest() { Md5 = md5 }).Result)
            {
                CancellationTokenSource tokenSource = new CancellationTokenSource() { };
                var call = client.UploadWithStream(cancellationToken:tokenSource.Token);
                int size = 1024;
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
                            await call.RequestStream.WriteAsync(new UploadWithStreamRequest() { Data = Google.Protobuf.ByteString.CopyFrom(bytes) });

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
