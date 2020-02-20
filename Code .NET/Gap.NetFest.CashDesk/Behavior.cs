using Gap.NetFest.AzureStorage;
using Gap.NetFest.Core.Interface;
using Gap.NetFest.Core.Models;
using Gap.NetFest.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Storage = Gap.NetFest.AzureStorage;
using Database = Gap.NetFest.DataAccess;

namespace Gap.NetFest.CashDesk
{
    public class Behavior
    {

        public Behavior()
        {
            this.Id = Guid.NewGuid();
        }

        public StateProcess InProcess { get; set; }
        public Guid Id { get; set; }
        public Store StoreCurrent { get; set; }

        public int countIteration { get; set; }

        public bool IsCustomerModel { get { return this.CustomerModel == null ? false : true; } }

        private List<PosMachine> PostMachines { get; set; }
        private TypeCustomerRepository TypeCustomer { get; set; }
        private Customer CustomerModel = null;
        private Chocolate ChocolateModel = null;

        /// <summary>
        /// Inicialización.
        /// </summary>
        public void Start(IConfiguration configuration)
        {
            IChocolate chocolateRepository = new ChocolateRepository();

            this.ChocolateModel = new Chocolate(chocolateRepository);
            this.PostMachines = new List<PosMachine>();
            this.ChangeCustomerRepository(TypeCustomerRepository.Storage);

            for (int i = 0; i < this.StoreCurrent.PosMachineAmount; i++)
            {
                this.PostMachines.Add(new PosMachine() { NumberMachine = i, StoreAssigned = this.StoreCurrent });
            }

            this.InProcess = StateProcess.ReadyToProcess;
        }

        /// <summary>
        /// Ciclo.
        /// </summary>
        public void Loop()
        {
            String loopIdentifier = Guid.NewGuid().ToString();
            Random random = new Random();
            var indexPOSMachine = 0;
            try
            {
                System.Console.WriteLine("Store simulation begins " + this.StoreCurrent.Name);

                if (this.TypeCustomer == TypeCustomerRepository.DataBase)
                    this.ChangeCustomerRepository(TypeCustomerRepository.Storage);

                List<Customer> listCustomer = this.CustomerModel.GetRandomCustomer();

                listCustomer.ForEach(x =>
                {
                    var chocolateAmount = random.Next(1, 5);
                    var result = this.ChocolateModel.GetRandomChocolates(chocolateAmount);

                    if (this.TypeCustomer == TypeCustomerRepository.Storage)
                        this.ChangeCustomerRepository(TypeCustomerRepository.DataBase);

                    this.MappedCustomerModel(x);
                    this.CustomerModel.SaveCustomer();

                    if (indexPOSMachine == this.PostMachines.Count)
                        indexPOSMachine = 0;

                    IInvoice invoiceRepository = new Storage.InvoiceRepository();
                    var invoice = new Invoice(invoiceRepository);
                    invoice = this.PostMachines[indexPOSMachine].GenerateBill(result, this.CustomerModel, invoice);
                    invoice.NameMachinePos = this.StoreCurrent.Name + " - POS#: " + (indexPOSMachine + 1).ToString();
                    invoice.SaveInvoce();
                    indexPOSMachine++;
                });
                System.Console.WriteLine("Store simulator ending " + this.StoreCurrent.Name);
            }
            catch (Exception exe)
            {
                System.Console.WriteLine("Error: " + exe.Message + " -- " + exe.StackTrace);
                System.Console.WriteLine("Store simulator ending: " + this.StoreCurrent.Name);
            }
        }

        /// <summary>
        /// Set the Customer Repository.
        /// </summary>es
        /// <param name="typeCustomer"></param>
        private void ChangeCustomerRepository(TypeCustomerRepository typeCustomer)
        {
            ICustomer customerRepository;
            this.TypeCustomer = typeCustomer;
            switch (typeCustomer)
            {
                case TypeCustomerRepository.Storage:
                    customerRepository = Storage.CustomerRepository.GetInstance();
                    break;
                case TypeCustomerRepository.DataBase:
                    customerRepository = new Database.Repositories.CustomerRepository();
                    break;
                default:
                    customerRepository = Storage.CustomerRepository.GetInstance();
                    this.TypeCustomer = TypeCustomerRepository.Storage;
                    break;
            }

            this.CustomerModel = new Customer(customerRepository);
        }

        /// <summary>
        /// Mapped Customer Model.
        /// </summary>
        /// <param name="customerToMapped"></param>
        private void MappedCustomerModel(Customer customerToMapped)
        {
            this.CustomerModel.Id = customerToMapped.Id;
            this.CustomerModel.LastName = customerToMapped.LastName;
            this.CustomerModel.Name = customerToMapped.Name;
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
