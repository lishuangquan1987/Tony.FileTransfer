using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tony.FileTransfer.Core.Common
{
  
    public static class GuidExtentions
    {
        /// <summary>  
        /// 根据GUID获取16位的唯一字符串  
        /// </summary>  
        /// <param name="guid"></param>  
        /// <returns></returns>  
        public static string To16String(this Guid guid)
        {
            var i = guid.ToByteArray().Aggregate<byte, long>(1, (current, b) => current * (b + 1));
            return $"{i - DateTime.Now.Ticks:x}";
        }
        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>  
        public static long ToInt64(this Guid guid)
        {
            var buffer = guid.ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
    }
    
}
