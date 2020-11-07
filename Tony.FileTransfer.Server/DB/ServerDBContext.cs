using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Tony.FileTransfer.Core.TableModel;

namespace Tony.FileTransfer.Server.DB
{
    public class ServerDBContext:DbContext
    {
        public ServerDBContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Core.TableModel.ServerFile> FileInfos { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<MachineInfo> MachineInfos { get; set; }
        public DbSet<User_FileInfo> UserFileInfos { get; set; }
        public DbSet<User_MachineInfo> UserMachineInfos { get; set; }
        public DbSet<Machine_FileInfo> MachineFileInfos { get; set; }

    }
}
