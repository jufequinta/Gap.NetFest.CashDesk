using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gap.NetFest.DataAccess.DTO
{
    [Table("Pay_Methods")]
    public class Pay_Methods
    {
        [Key]
        [Column("id")]
        public short Id { get; set; }

        [Column("description")]
        public string Description { get; set; }

        public ICollection<Invoice_Details> InvoiceDetails { get; set; }
    }
}
