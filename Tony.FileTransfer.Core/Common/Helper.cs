using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tony.FileTransfer.Core.Common
{
    public class Helper
    {
        private static DateTime baseDateTime = new DateTime(1970, 1, 1, 8, 0, 0);
        public static string GetMD5HashFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(fs);
                fs.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        public static long ConvertTimeToLong(DateTime dt)
        {
            return (dt - baseDateTime).Ticks;
        }
        public static DateTime ConvertLongToTime(long tick)
        {
            return baseDateTime + new TimeSpan(tick);
        }

    }
}
