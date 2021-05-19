using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces
{
    public interface ICustomerContext
    {
        public IEnumerable<Customer> GetCustomers();
        public int InsertCustomer(Customer customer);
        public Customer GetCustomerByCode(string code);

    }
}
