using System;
using System.Collections.Generic;
using System.Text;

namespace Gap.NetFest.Core.Models
{
    public class InvoiceDetails
    {
        public long Id { get; set; }

        public Guid IdInvoice { get; set; }

        public int IdChocolate_brand { get; set; }

        public int Quantity { get; set; }

        public short IdPymethod { get; set; }

        public Invoice Invoice { get; set; }

        public Chocolate Chocolate { get; set; }

        public Pay_Method PayMethod { get; set; }

        public InvoiceDetails() { }
    }
}
