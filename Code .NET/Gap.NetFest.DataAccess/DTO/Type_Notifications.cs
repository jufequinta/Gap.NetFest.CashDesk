using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gap.NetFest.DataAccess.DTO
{
    [Table("Type_Notifications")]
    public class Type_Notifications
    {
        [Key]
        [Column("id")]
        public short Id { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}
