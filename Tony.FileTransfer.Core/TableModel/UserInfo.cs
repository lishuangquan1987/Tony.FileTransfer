using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tony.FileTransfer.Core.TableModel
{
    [Table("UserInfo")]
   public class UserInfo
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("password")]
        public string Password { get; set; }
    }
}
