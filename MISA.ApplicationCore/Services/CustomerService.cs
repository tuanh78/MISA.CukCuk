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
        public override void PreSave(List<Customer> entities)
        {
            base.PreSave(entities);
        }

        public override bool Validate(List<Customer> entities)
        {
            var result = true;
            result = base.Validate(entities);
            // thực hiện custom validate
            foreach (var item in entities)
            {
            }

            return result;
        }

        public override void AfterSave(List<Customer> entities)
        {
            base.AfterSave(entities);
            var memberService = new MembershipService();
            foreach (var item in entities)
            {
                var membership = new Membership();
                if (item.EditMode == Enums.EditMode.Add)
                {
                    membership.CustomerId = item.CustomerId;
                    membership.MembershipId = Guid.NewGuid();
                    membership.TotalPoint = 0;
                    membership.EditMode = Enums.EditMode.Add;
                }
                else if (item.EditMode == Enums.EditMode.Edit)
                {
                    // viết stored update điểm điểm theo CustomerId
                    UdpateTotalPointByCustomer(membership.CustomerId, 95);
                }
            }
        }

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