using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Tony.FileTransfer.Core.TableModel;

namespace Tony.FileTransfer.Server.DB
{
    public class ServerDBContext : DbContext
    {
        public ServerDBContext()
        {
        }
        /// <summary>
        /// ctor for unit test
        /// </summary>
        /// <param name="options"></param>
        public ServerDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Core.TableModel.ServerFile> FileInfos { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<MachineInfo> MachineInfos { get; set; }
        public DbSet<User_FileInfo> UserFileInfos { get; set; }
        public DbSet<User_MachineInfo> UserMachineInfos { get; set; }
        public DbSet<Machine_FileInfo> MachineFileInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=TonyFileTranster.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
