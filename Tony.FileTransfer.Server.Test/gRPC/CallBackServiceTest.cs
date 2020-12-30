using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tony.FileTransfer.Server.Test.gRPC
{
    public class CallBackServiceTest
    {
        ICallBack.ICallBackClient client;
        public CallBackServiceTest()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions() { MaxSendMessageSize = int.MaxValue, MaxReceiveMessageSize = int.MaxValue });
            client = new ICallBack.ICallBackClient(channel);
        }

        [Fact]
        public async void RegisterTest()
        {
            string recognizeId = "455286005";
            var call= client.Register(new CommonRequest() {  Message=recognizeId});
            var tokenSource = new System.Threading.CancellationTokenSource();
            await call.ResponseStream.MoveNext(tokenSource.Token);
            var result = call.ResponseStream.Current;
            if (result.CmdType != CmdTypes.RegisterSuccess)
            {
                tokenSource.Cancel();
                return;
            }
            while (!tokenSource.IsCancellationRequested && await call.ResponseStream.MoveNext(tokenSource.Token))
            {
                var model = call.ResponseStream.Current;
                Assert.True(model.CmdType== CmdTypes.HeartBeat);
                tokenSource.Cancel();
            }
            
        }
    }
}
