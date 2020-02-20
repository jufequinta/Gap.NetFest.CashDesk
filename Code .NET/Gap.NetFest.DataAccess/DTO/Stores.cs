using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gap.NetFest.DataAccess.DTO
{
    [Table("Stores")]
    public class Stores
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("pos_machine_amount")]
        public short PosMachineAmount { get; set; }
    }
}
