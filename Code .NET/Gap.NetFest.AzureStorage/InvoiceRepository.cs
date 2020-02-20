using Gap.NetFest.Core.Interface;
using Gap.NetFest.Core.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gap.NetFest.AzureStorage
{
    public class InvoiceRepository : IInvoice
    {

        private CloudStorageAccount _storageAccount;
        private CloudQueueClient queueClient;
        private CloudQueue queue;
        /// <summary>
        /// Constructor
        /// </summary>
        public InvoiceRepository()
        {
            _storageAccount = new CloudStorageAccount(
                new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
                ConectionResource.stochocolate, ConectionResource.stochocolateKey), true);
            queueClient = _storageAccount.CreateCloudQueueClient();
            queue = queueClient.GetQueueReference("queueinvoices");
            queue.CreateIfNotExistsAsync();
        }

        public bool SaveInvoice(Invoice invoiceSimulation)
        {
            string messageToQueue = JsonConvert.SerializeObject(invoiceSimulation, Formatting.Indented);
            return this.SaveInvoiceAsync(messageToQueue).GetAwaiter().GetResult();
        }

        public bool SaveInvoice(string invoiceSimulation)
        {
            return this.SaveInvoiceAsync(invoiceSimulation).GetAwaiter().GetResult();
        }

        public string DequeueMessage()
        {
            return this.DequeueMessageMethod().GetAwaiter().GetResult();
        }

        private async Task<bool> SaveInvoiceAsync(string messageToQueue)
        {

            CloudQueueMessage message = new CloudQueueMessage(messageToQueue);
            await queue.AddMessageAsync(message);
            return true;
        }

        private async Task<string> DequeueMessageMethod()
        {
            string message = string.Empty;
            bool exists = await queue.ExistsAsync();
            if (exists)
            {
                CloudQueueMessage retrievedMessage = await queue.GetMessageAsync();
                if (retrievedMessage != null)
                {
                    message = retrievedMessage.AsString;
                    await queue.DeleteMessageAsync(retrievedMessage);
                }
            }

            return message;
        }
    }
}
