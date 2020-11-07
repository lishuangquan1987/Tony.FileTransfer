using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tony.FileTransfer.Core.TableModel
{
    [Table("Machine_FileInfo")]
    public class Machine_FileInfo
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("machine_id")]
        public int MachineId { get; set; }
        [Column("file_id")]
        public int FileId { get; set; }
        [Column("file_name")]
        public string FileName { get; set; }
        [Column("create_date")]
        public DateTime? CreateDate { get; set; }
    }
}
