using System;
using System.Collections.Generic;
using System.Text;

namespace Gap.NetFest.Core.Models
{
    public class PosMachine
    {
        public int NumberMachine { get; set; }
        public Store StoreAssigned { get; set; }

        public Invoice GenerateBill(List<Chocolate> chocolates, Customer customer,Invoice invoice)
        {
            // 1. Step, Creation Invoice.
            invoice.Id = Guid.NewGuid();
            invoice.IdCustomer = customer.Id;
            invoice.Date = DateTime.Now;

            // 2. Create Detail Invoice
            chocolates.ForEach(x =>
            {
                var invoiceDetail = new InvoiceDetails();
                invoiceDetail.IdChocolate_brand = x.Id;
                invoiceDetail.IdInvoice = invoice.Id;
                invoiceDetail.IdPymethod = (short)GenerateTypePayMethod();
                invoiceDetail.Quantity = x.Amount;

                invoice.InvoiceDetails.Add(invoiceDetail);
            });


            return invoice;
        }

        private PayMethod GenerateTypePayMethod()
        {
            var random = new Random();
            switch (random.Next(1, 4))
            {
                case 1:
                    return PayMethod.DebitCard;
                case 2:
                    return PayMethod.CreditCard;
                default:
                    return PayMethod.Cash;
            }
        }
    }

    public enum PayMethod
    {
        DebitCard = 1,
        CreditCard = 2,
        Cash = 3
    }
}
