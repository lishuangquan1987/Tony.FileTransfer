using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tony.FileTransfer.Server.State;

namespace Tony.FileTransfer.Server.Services
{
    public class CallbackService : ICallBack.ICallBackBase
    {
        public override async Task Register(CommonRequest request, IServerStreamWriter<CallbackResponse> responseStream, ServerCallContext context)
        {
            string recognizeIdStr = request.Message;
            if (string.IsNullOrEmpty(recognizeIdStr) || !int.TryParse(recognizeIdStr, out int recognizeId))
            {
                await responseStream.WriteAsync(new CallbackResponse() { CmdType = CmdTypes.RegisterFail, Message = "Invalid RecognizeId" });
                return;
            }
            ClientManager.Instance.AddClient(recognizeId);
            await responseStream.WriteAsync(new CallbackResponse() { CmdType = CmdTypes.RegisterSuccess });
            while (!context.CancellationToken.IsCancellationRequested)
            {
                if (ClientManager.Instance.GetMessage(recognizeId, out var message))
                {
                    await responseStream.WriteAsync(message);
                }
                await Task.Delay(10);
            }
            ClientManager.Instance.RemoveClient(recognizeId);
        }
    }
}
