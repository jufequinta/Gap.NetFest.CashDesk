using Gap.NetFest.AzureStorage;
using Gap.NetFest.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Gap.NetFest.ServicesBackground
{
    public class Behavior
    {

        public Behavior()
        {
            this.Id = Guid.NewGuid();
        }

        public StateProcess InProcess { get; set; }
        public Guid Id { get; set; }
        private Invoice InvoiceModel = null;
        public bool IsInvoiceModel { get { return this.InvoiceModel == null ? false : true; } }

        public int countIteration { get; set; }

        /// <summary>
        /// Inicialización.
        /// </summary>
        public void Start(IConfiguration configuration)
        {
            var invoiceRepository = new InvoiceRepository();
            InvoiceModel = new Invoice(invoiceRepository);
            this.InProcess = StateProcess.ReadyToProcess;
        }

        /// <summary>
        /// Ciclo.
        /// </summary>
        public void Loop()
        {
            String loopIdentifier = Guid.NewGuid().ToString();
            try
            {
                System.Console.WriteLine("Store simulation begins " + loopIdentifier);
                this.InvoiceModel.DequeueMessage();
                if (!string.IsNullOrEmpty(this.InvoiceModel.MessageToSend))
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(" http://cashdeskwebservice.azurewebsites.net");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpStatusCode statusCode = SaveInvoiceAsync(this.InvoiceModel.MessageToSend, client).GetAwaiter().GetResult();
                }
                System.Console.WriteLine("Store simulator ending " + loopIdentifier);
            }
            catch (Exception exe)
            {
                if (!string.IsNullOrEmpty(this.InvoiceModel.MessageToSend))
                {
                    this.InvoiceModel.SaveInvoceMessage();
                }
                System.Console.WriteLine("Error: " + exe.Message + " -- " + exe.StackTrace);
                System.Console.WriteLine("Store simulator ending: " + loopIdentifier);
            }
        }

        private async Task<HttpStatusCode> SaveInvoiceAsync(string invoice, HttpClient client)
        {
            var content = new StringContent(invoice, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(
                "api/CashDesk", content);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.StatusCode;
        }
    }



    public enum StateProcess
    {
        Inprocess = 1,
        ReadyToProcess = 0,
        RestarToProcess = 2
    }

    public enum TypeCustomerRepository
    {
        Storage = 1,
        DataBase = 2
    }
}
