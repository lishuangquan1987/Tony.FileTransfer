using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace Tony.FileTransfer.Server.State
{
    public class ClientManager
    {
        private ConcurrentDictionary<int, Grpc.Core.IServerStreamWriter<CallbackResponse>> dicClient = new ConcurrentDictionary<int, Grpc.Core.IServerStreamWriter<CallbackResponse>>();
        public static ClientManager Instance;
        static ClientManager()
        {
            Instance = new ClientManager();
        }

        public ClientManager()
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    SendCmdToClient(CmdTypes.HeartBeat);
                }
            });
        }

        public void AddClient(int recognizeId, Grpc.Core.IServerStreamWriter<CallbackResponse> client)
        {
            if (dicClient.ContainsKey(recognizeId))
            {
                dicClient.TryRemove(recognizeId, out Grpc.Core.IServerStreamWriter<CallbackResponse> c);
            }
            dicClient.TryAdd(recognizeId,client);
        }
        public void RemoveClient(int recognizeId)
        {
            if (dicClient.ContainsKey(recognizeId))
            {
                dicClient.TryRemove(recognizeId, out Grpc.Core.IServerStreamWriter<CallbackResponse> c);
            }
        }
        public  void SendCmdToClient(CmdTypes cmdType, string msg = null)
        {
            foreach (var key in dicClient.Keys)
            {
                dicClient[key].WriteAsync(new CallbackResponse() {  CmdType=cmdType,Message=msg});
            }
        }

    }
}
