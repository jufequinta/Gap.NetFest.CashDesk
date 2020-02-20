using Gap.NetFest.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gap.NetFest.Core.Interface
{
    public interface ICustomer
    {
        Task<List<Customer>> GetRandomCustomer();
        bool SaveCustomer(Customer customer);
    }
}
