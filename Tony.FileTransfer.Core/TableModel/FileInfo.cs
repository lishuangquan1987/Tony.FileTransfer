using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tony.FileTransfer.Core.Common;

namespace Tony.FileTransfer.Core.TableModel
{
    [Table("FileInfo")]
    public class ServerFile
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Unique]
        [Column("md5")]
        public string Md5 { get; set; }
        [Column("server_path")]
        public string ServerPath { get; set; }
        [Column("create_time")]
        public DateTime? CreateTime { get; set; }
        [Column("size")]
        public long Size { get; set; }
    }
}
