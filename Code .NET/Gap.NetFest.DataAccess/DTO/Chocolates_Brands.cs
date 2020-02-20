using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gap.NetFest.DataAccess.DTO
{
    [Table("Chocolates_Brands")]
    public class Chocolates_Brands
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Column("id_provider")]
        public int IdProvider { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        public Providers Provider { get; set; }

        public ICollection<Invoice_Details> InvoiceDetails { get; set; }
    }
}
