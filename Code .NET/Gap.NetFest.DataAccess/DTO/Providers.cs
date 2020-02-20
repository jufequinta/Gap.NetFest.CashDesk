using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gap.NetFest.DataAccess.DTO
{
    [Table("Providers")]
    public class Providers
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name_provider")]
        public string NameProvider { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }

        public ICollection<Chocolates_Brands> Chocolates { get; set; }
    }
}
