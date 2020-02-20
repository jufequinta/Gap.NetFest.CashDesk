using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gap.NetFest.DataAccess.DTO
{
    [Table("Invoices")]
    public class Invoices
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("id_customer")]
        public string IdCustomer { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("name_machine_pos")]
        public string NameMachinePos { get; set; }

        public List<Invoice_Details> InvoiceDetails { get; set; }

        public Customers Customer { get; set; }
    }
}
