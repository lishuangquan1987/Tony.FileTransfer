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
        private Dictionary<int, ConcurrentQueue<CallbackResponse>> dicMessage = new Dictionary<int, ConcurrentQueue<CallbackResponse>>();
        public static ClientManager Instance;
        static ClientManager()
        {
            Instance = new ClientManager();
        }

        public ClientManager()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(3000);
                    SendCmdToAllClient(CmdTypes.HeartBeat);
                }
            });
        }

        public void AddClient(int recognizeId)
        {
           
            if (!dicMessage.ContainsKey(recognizeId))
            {
                dicMessage.Add(recognizeId, new ConcurrentQueue<CallbackResponse>());
            }
        }

        public void RemoveClient(int recognizeId)
        {
            if (dicMessage.ContainsKey(recognizeId))
            {
                dicMessage.Remove(recognizeId);
            }
        }
        private void SendCmdToAllClient(CmdTypes cmdType, string msg = "")
        {
            foreach (var key in dicMessage.Keys)
            {
                SendCmdToClient(key, cmdType, msg);
            }
        }

        public void SendCmdToClient(int recognizeId, CmdTypes cmdType, [System.Diagnostics.CodeAnalysis.NotNull] string msg)
        {
            if (dicMessage.ContainsKey(recognizeId))
            {
                dicMessage[recognizeId].Enqueue(new CallbackResponse() { CmdType = cmdType, Message = msg });
            }

        }

        public bool GetMessage(int recognize,out CallbackResponse message)
        {
            message = null;
            if (!dicMessage.ContainsKey(recognize)) return false;

            return dicMessage[recognize].TryDequeue(out message);
        }
    }
    
}
