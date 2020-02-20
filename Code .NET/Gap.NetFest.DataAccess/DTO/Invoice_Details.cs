using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gap.NetFest.DataAccess.DTO
{
    [Table("Invoice_Details")]
    public class Invoice_Details
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("id_invoice")]
        public Guid IdInvoice { get; set; }

        [Column("id_chocolate_brand")]
        public int IdChocolate_brand { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("id_pymethod")]
        public short IdPymethod { get; set; }

        public Invoices Invoice { get; set; }

        public Chocolates_Brands Chocolate { get; set; }

        public Pay_Methods PayMethod { get; set; }

    }
}
