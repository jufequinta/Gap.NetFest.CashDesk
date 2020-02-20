using AutoMapper;
using Gap.NetFest.Core.Interface;
using Gap.NetFest.Core.Models;
using Gap.NetFest.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gap.NetFest.DataAccess.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomer
    {
        private MapperConfiguration config;

        public CustomerRepository() : base()
        {
            config = new MapperConfiguration(cfg => cfg.CreateMap<Customer, Customers>());
        }

        public Task<List<Customer>> GetRandomCustomer()
        {
            throw new NotImplementedException();
        }

        public bool SaveCustomer(Customer customer)
        {
            var mapper = new Mapper(config);
            Customers customerToSave = mapper.Map<Customers>(customer);

            if (this.GetInstance().Customers.Where(x => x.Id == customerToSave.Id).FirstOrDefault() != null)
                return true;

            this.GetInstance().Customers.Add(customerToSave);
            var result = this.GetInstance().SaveChanges();

            return result > 0 ? true : false;
        }
    }
}
