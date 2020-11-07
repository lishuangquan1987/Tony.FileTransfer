using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tony.FileTransfer.Core.TableModel
{
    [Table("FileInfo")]
    public class ServerFile
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("md5")]
        public string Md5 { get; set; }
        [Column("server_path")]
        public string ServerPath { get; set; }
        [Column("create_time")]
        public DateTime? CreateTime { get; set; }
    }
}
