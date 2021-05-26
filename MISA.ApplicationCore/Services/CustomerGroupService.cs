using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Services
{
    public class CustomerGroupService : BaseService<CustomerGroup>, ICustomerGroupService
    {
        private ICustomerGroupRepository _customerGroupRepository;

        public CustomerGroupService(IBaseRepository<CustomerGroup> baseRepository, ICustomerGroupRepository customerGroupRepository) : base(customerGroupRepository)
        {
            _customerGroupRepository = customerGroupRepository;
        }
    }
}