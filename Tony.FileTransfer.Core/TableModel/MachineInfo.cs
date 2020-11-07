using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tony.FileTransfer.Core.TableModel
{
    [Table("MachineInfo")]
    public class MachineInfo
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("recognize_id")]
        public int RecognizeId { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("machine_identity")]
        public string MachineIdentity { get; set; }
    }
}
