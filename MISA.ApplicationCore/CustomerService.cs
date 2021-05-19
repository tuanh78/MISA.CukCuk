using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore
{
    public class CustomerService : ICustomerService
    {
        ICustomerContext customerContext;

        #region Constructor
        public CustomerService(ICustomerContext _customerContext)
        {
            customerContext = _customerContext;
        }
        #endregion

        #region Method
        // Lấy danh sách khách hàng:
        /// <summary>
        /// Lấy danh sách khách hàng
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        /// CreatedBy: PTANH (19/5/2021)
        public IEnumerable<Customer> GetCustomers()
        {
            var customers = customerContext.GetCustomers();
            return customers;
        }
        /// <summary>
        /// Thêm mới khách hàng 
        /// </summary>
        /// <param name="customer">Đối tượng khách hàng</param>
        /// <returns>Đối tượng ServiceResult</returns>
        public ServiceResult InsertCustomer(Customer customer) 
        {
            var serviceResult = new ServiceResult();
            // Check các trường bắt buộc nhập
            var properties = customer.GetType().GetProperties();
            foreach(var property in properties)
            {
                if(property.GetValue(customer) == null)
                {
                    var msg = new
                    {
                        devMsg = new { fieldName = $"{property}", msg = $"{property} không được để trống" },
                        userMsg = $"{property} không được phép để trống"
                    };
                    serviceResult.MisaCode = MISACode.NotValid;
                    serviceResult.Messenger = $"{property} Không được để trống";
                    serviceResult.Data = msg;
                    return serviceResult;
                }
            }

            // Check trùng mã:
            var res = customerContext.GetCustomerByCode(customer.CustomerCode);
            if(res != null)
            {
                var msg = new
                {
                    devMsg = new { fieldName = "CustomerCode", msg = "Mã khách hàng đã tồn tại" },
                    userMsg = "Mã khách hàng đã tồn tại",
                    Code = 900
                };
                serviceResult.MisaCode = MISACode.NotValid;
                serviceResult.Messenger = "Mã khách hàng đã tồn tại";
                serviceResult.Data = msg;
                return serviceResult;
            }

            var rowAffects = customerContext.InsertCustomer(customer);
            serviceResult.MisaCode = MISACode.Success;
            serviceResult.Messenger = "Thêm khách hàng thành công";
            serviceResult.Data = rowAffects;
            return serviceResult;

        }

        // Sửa thông tin khách hàng:

        // Xóa khách hàng:
        #endregion
    }
}
