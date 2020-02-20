using AutoMapper;
using Gap.NetFest.AzureStorage.DTO;
using Gap.NetFest.Core.Interface;
using Gap.NetFest.Core.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gap.NetFest.AzureStorage
{
    public class CustomerRepository : ICustomer
    {
        private CloudStorageAccount _storageAccount;
        private CloudTableClient _tableClient;
        private CloudTable _tableCustomer;
        private MapperConfiguration config;
        /// <summary>
        /// Constructor
        /// </summary>
        private CustomerRepository()
        {
            _storageAccount = new CloudStorageAccount(
                new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
                ConectionResource.stochocolate, ConectionResource.stochocolateKey), true);
            _tableClient = _storageAccount.CreateCloudTableClient();
            _tableCustomer = _tableClient.GetTableReference(ConectionResource.stoTable);

            config = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDTO, Customer>());
        }

        /// <summary>
        /// Get the instance of Conection AzureTableDB
        /// </summary>
        public static CustomerRepository GetInstance()
        {
            return new CustomerRepository();
        }

        public bool GenerateSimulationInvoice(Invoice invoiceSimulation)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Customer>> GetRandomCustomer()
        {
            var ListCustomers = new List<Customer>();
            var mapper = new Mapper(config);
            TableQuery<CustomerDTO> partitionScanQuery = new TableQuery<CustomerDTO>();

            TableContinuationToken token = null;

            // Read entities from each query segment.
            do
            {
                TableQuerySegment<CustomerDTO> segment = await _tableCustomer.ExecuteQuerySegmentedAsync(partitionScanQuery, token);
                token = segment.ContinuationToken;
                segment.Results.ForEach(x =>
                {
                    ListCustomers.Add(mapper.Map<Customer>(x));
                });
            } while (token != null);

            return ListCustomers;
        }

        public bool SaveCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
