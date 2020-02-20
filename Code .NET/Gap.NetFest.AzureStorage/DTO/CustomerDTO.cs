using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gap.NetFest.AzureStorage.DTO
{
    public class CustomerDTO : TableEntity
    {

        /// <summary>
        /// Customer Azure Table
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <param name="CompleteName">Complete Name.</param>
        public CustomerDTO(string Id, string CompleteName)
        {
            this.PartitionKey = Id;
            this.RowKey = CompleteName;
        }

        /// <summary>
        /// Constructor Empty.
        /// </summary>
        public CustomerDTO() { }

        /// <summary>
        /// Last Name of Customer.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Name of Customer
        /// </summary>
        public string Name { get; set; }

        public string Id { get { return this.PartitionKey; } }
    }
}
