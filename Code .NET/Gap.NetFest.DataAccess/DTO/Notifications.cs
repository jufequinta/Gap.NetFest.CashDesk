using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Gap.NetFest.DataAccess.DTO
{
    [Table("Notifications")]
    public class Notifications
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_provider")]
        public int IdProvider { get; set; }

        [Column("id_type")]
        public short IdType { get; set; }

        [Column("periodicity")]
        public int Periodicity { get; set; }
    }
}
