using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        private ICustomerRepository _customerRepository;

        public CustomerService(IBaseRepository<Customer> baseRepository, ICustomerRepository customerRepository) : base(baseRepository)

        {
            _customerRepository = customerRepository;
        }

        //public override ServiceResult Add(Customer entity)
        //{
        //    var customerByCode = _customerRepository.GetCustomerByCode(entity.CustomerCode);
        //    var customerByPhoneNumber = _customerRepository.GetCustomerByPhoneNumber(entity.PhoneNumber);
        //    if (customerByCode != null)
        //    {
        //        return -1;
        //    }
        //    else if (customerByPhoneNumber != null)
        //    {
        //        return -2;
        //    }

        //    return base.Add(entity);
        //}

        public override int Update(Customer entity, Guid entityId)
        {
            var customerByCode = _customerRepository.GetCustomerByCodeAndAnthorId(entityId, entity.CustomerCode);
            var customerByPhoneNumber = _customerRepository.GetCustomerByPhoneNumberAndAnthorId(entityId, entity.PhoneNumber);
            if (customerByCode != null)
            {
                return -1;
            }
            else if (customerByPhoneNumber != null)
            {
                return -2;
            }
            return base.Update(entity, entityId);
        }
    }
}