using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tony.FileTransfer.Core.TableModel
{
    [Table("User_FileInfo")]
    public class User_FileInfo
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("file_id")]
        public int FileId { get; set; }
        [Column("file_name")]
        public string FileName { get; set; }
        [Column("create_date")]
        public DateTime? CreateDate { get; set; }
    }
}
