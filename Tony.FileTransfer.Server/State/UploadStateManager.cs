using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Tony.FileTransfer.Server.State
{
    public class UploadStateManager
    {
        private static object lockObj = new object();
        private static UploadStateManager instance;
        public static UploadStateManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new UploadStateManager();
                        }
                    }
                }
                return instance;
            }
        }
        private  Dictionary<string, UploadState> uploadStateCache = new Dictionary<string, UploadState>();
        public  Dictionary<string, UploadState> UploadStateCache
        {
            get { return uploadStateCache; }
        }
    }
}
