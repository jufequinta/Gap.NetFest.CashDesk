using Gap.NetFest.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gap.NetFest.CashDeskWebServices.DTO
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public string IdCustomer { get; set; }
        public DateTime Date { get; set; }
        public string NameMachinePos { get; set; }
        public ICollection<InvoiceDetails> InvoiceDetails { get; set; }

    }
}
