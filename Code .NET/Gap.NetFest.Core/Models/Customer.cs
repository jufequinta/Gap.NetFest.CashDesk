using AutoMapper;
using Gap.NetFest.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gap.NetFest.Core.Models
{
    public class Customer
    {
        #region Properties

        /// <summary>
        /// Last Name of Customer.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Name of Customer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id of Customer.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Id Customer Long
        /// </summary>
        public int Identity { get; set; }

        private ICustomer CustomerRepository;

        #endregion

        /// <summary>
        /// Constructor Empty.
        /// </summary>
        public Customer()
        {
        }

        public Customer(ICustomer customerRepository)
        {
            CustomerRepository = customerRepository;
        }

        /// <summary>
        /// Get List Customer.
        /// </summary>
        /// <returns>Get Customers.</returns>
        public List<Customer> GetCustomer()
        {
            return CustomerRepository.GetRandomCustomer().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Generate the Customers to buy chocolates.
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetRandomCustomer()
        {
            var customers = this.CustomerRepository.GetRandomCustomer().GetAwaiter().GetResult();
            List<Customer> customersToBills = new List<Customer>();
            int identity = 1;
            customers.ForEach(x => { x.Identity = identity; identity++; });
            var max = customers.Max(x => x.Identity);
            var min = customers.Min(x => x.Identity);

            Random random = new Random();

            var lenCustomerBills = random.Next(5, 10);
            
            for (int i = 0; i < lenCustomerBills; i++)
            {
                var flag = false;
                var customerRandom = random.Next(min, max);
                while (!flag)
                {
                    var customerTMP = customers.Where(x => x.Identity == customerRandom).FirstOrDefault();
                    if (!customersToBills.Exists(x => x.Identity == customerTMP.Identity))
                    {
                        customersToBills.Add(customers.Where(x => x.Identity == customerRandom).FirstOrDefault());
                        flag = true;
                    }
                    else
                    {
                        customerRandom = random.Next(min, max);
                    }
                }
            }

            return customersToBills;
        }

        /// <summary>
        /// Save Customer.
        /// </summary>
        /// <returns></returns>
        public bool SaveCustomer()
        {
            return this.CustomerRepository.SaveCustomer(this);
        }

    }
}
