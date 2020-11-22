using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tony.FileTransfer.Server.State;

namespace Tony.FileTransfer.Server.Services
{
    public class CallbackService:ICallBack.ICallBackBase
    {
        public override Task Register(CommonRequest request, IServerStreamWriter<CallbackResponse> responseStream, ServerCallContext context)
        {
            string recognizeIdStr= request.Message;
            if (string.IsNullOrEmpty(recognizeIdStr) || !int.TryParse(recognizeIdStr, out int recognizeId))
            {
               return responseStream.WriteAsync(new CallbackResponse() { CmdType = CmdTypes.RegisterFail, Message = "Invalid RecognizeId" });
            }
            ClientManager.Instance.AddClient(recognizeId, responseStream);
            responseStream.WriteAsync(new CallbackResponse() { CmdType = CmdTypes.RegisterSuccess });
            while (!context.CancellationToken.IsCancellationRequested)
            {
                //调用只能在当前上下文调用！！！
                Thread.Sleep(1000);
            }
            return Task.Delay(10);
        }
    }
}
