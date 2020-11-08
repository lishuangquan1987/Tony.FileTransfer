using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tony.FileTransfer.Server.State
{
    public class UploadState
    {
        public string Md5 { get; set; }
        public long HasUploadedByte { get; set; }
        public bool IsUploadFinish { get; set; }
        public string CacheFilePath { get; set; }
    }
}
