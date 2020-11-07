using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;

namespace Tony.FileTransfer.Server.Services
{
    public class FileUploadService:IFileUpload.IFileUploadBase
    {
        public override Task<CommonResponse> CheckFileExist(CheckFileExistRequest request, ServerCallContext context)
        {
            return base.CheckFileExist(request, context);
        }
        public override async Task<CommonResponse> UploadWithStream(IAsyncStreamReader<UploadWithStreamRequest> requestStream, ServerCallContext context)
        {
            Console.WriteLine("enter");
            while (await requestStream.MoveNext())
            {
                Console.WriteLine("receive"+requestStream.Current.Index);
                Thread.Sleep(1000);
            }
            return new CommonResponse() { Result = true };
        }
    }
}
