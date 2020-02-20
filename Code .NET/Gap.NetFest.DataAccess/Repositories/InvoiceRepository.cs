using AutoMapper;
using Gap.NetFest.Core.Interface;
using Gap.NetFest.Core.Models;
using Gap.NetFest.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gap.NetFest.DataAccess.Repositories
{
    public class InvoiceRepository : BaseRepository, IInvoice
    {
        private MapperConfiguration config;

        public InvoiceRepository() : base()
        {
            config = new MapperConfiguration(cfg => cfg.CreateMap<Invoice, Invoices>());
        }


        /// <summary>
        /// Save invoice.
        /// </summary>
        /// <param name="invoiceSimulation"></param>
        /// <returns></returns>
        public bool SaveInvoice(Invoice invoiceSimulation)
        {

            //var mapper = new Mapper(config);
            Invoices invoice = MapInvoices(invoiceSimulation);
            this.Context.Invoices.Add(invoice);
            this.Context.SaveChanges();
            //var result_invoices = this.Context == null ? 1 : 0;
            return true;
        }

        /// <summary>
        /// Mapped Invoices.
        /// </summary>
        /// <param name="invoiceSimulation"></param>
        /// <returns></returns>
        public Invoices MapInvoices(Invoice invoiceSimulation)
        {
            var invoice = new Invoices()
            {
                Date = invoiceSimulation.Date,
                Id = invoiceSimulation.Id,
                IdCustomer = invoiceSimulation.IdCustomer,
                NameMachinePos = invoiceSimulation.NameMachinePos,
                InvoiceDetails = invoiceSimulation.InvoiceDetails.Select(x => new Invoice_Details
                {
                    Id = x.Id,
                    IdChocolate_brand = x.IdChocolate_brand,
                    IdInvoice = x.IdInvoice,
                    IdPymethod = x.IdPymethod,
                    Quantity = x.Quantity
                }).ToList()
            };

            return invoice;

        }

        public string DequeueMessage()
        {
            throw new NotImplementedException();
        }

        public bool SaveInvoice(string invoiceSimulation)
        {
            throw new NotImplementedException();
        }
    }
}
