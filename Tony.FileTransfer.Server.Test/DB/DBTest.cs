using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using Tony.FileTransfer.Server.DB;
using Xunit;

namespace Tony.FileTransfer.Server.Test
{
    public class DBTest
    {
        string dbFileName = "TonyFileTranster.db";
        string connectionStr;
        DbContextOptions options;

        public DBTest()
        {
            connectionStr = $"Data Source={dbFileName}";
            options = new DbContextOptionsBuilder().UseSqlite(connectionStr).Options;
        }
        
        [Fact]
        public void DBAddTest()
        {
            
            using (ServerDBContext context = new ServerDBContext(options))
            {
                var fileInfo = new Tony.FileTransfer.Core.TableModel.ServerFile()
                {
                    CreateTime = DateTime.Now,
                    Id = 1,
                    Md5 = "this is a test",
                    ServerPath = "tete"
                };
                context.Add(fileInfo);
                context.SaveChanges();
            }
        }
        [Fact]
        public void CreateDBTest()
        {
            using (ServerDBContext context = new ServerDBContext(options))
            {
                var result = context.Database.EnsureCreated();
            }
        }
        [Fact]
        public void ReadDBTest()
        {
            using (ServerDBContext context = new ServerDBContext(options))
            {
                var result = context.FileInfos.FirstOrDefault();
            }
        }
    }
}
