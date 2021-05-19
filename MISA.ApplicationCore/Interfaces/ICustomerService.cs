using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces
{
    public interface ICustomerService
    {
        public IEnumerable<Customer> GetCustomers();
        public ServiceResult InsertCustomer(Customer customer);

    }
}
