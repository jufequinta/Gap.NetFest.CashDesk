using Gap.NetFest.Core.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gap.NetFest.Core.Models
{
    public class Invoice
    {
        #region Properties
        public Guid Id { get; set; }
        public string IdCustomer { get; set; }
        public DateTime Date { get; set; }
        public string NameMachinePos { get; set; }
        public ICollection<InvoiceDetails> InvoiceDetails { get; set; }

        private IInvoice InvoceRepository { get; set; }

        public string MessageToSend { get; set; }
        #endregion

        /// <summary>
        /// Constructor Empty.
        /// </summary>
        public Invoice()
        {
            this.InvoiceDetails = new List<InvoiceDetails>();
        }

        public Invoice(IInvoice invoceRepository) {
            this.InvoceRepository = invoceRepository;
            this.InvoiceDetails = new List<InvoiceDetails>();
        }

        public bool SaveInvoce()
        {
            return this.InvoceRepository.SaveInvoice(this);
        }

        public bool SaveInvoceMessage()
        {
            return this.InvoceRepository.SaveInvoice(this.MessageToSend);
        }

        public void DequeueMessage()
        {
            this.MessageToSend = this.InvoceRepository.DequeueMessage();
        }

        public void SetRepository(IInvoice invoceRepository) {
            this.InvoceRepository = invoceRepository;
        }       
    }
}
