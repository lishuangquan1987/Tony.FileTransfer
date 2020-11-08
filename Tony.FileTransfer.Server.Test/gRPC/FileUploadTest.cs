using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Text;
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
        public async void UploadFileTest()
        {
            var call = client.UploadWithStream();
            for (int i = 0; i < 100; i++)
            {
               await call.RequestStream.WriteAsync(new UploadWithStreamRequest() {});
            }
            await call.RequestStream.CompleteAsync();
        }
    }
}
