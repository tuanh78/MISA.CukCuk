using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        /// <summary>
        /// Lấy khách hàng theo mã khách hàng
        /// </summary>
        /// <param name="customerCode">Mã khách hàng</param>
        /// <returns>Đối tượng khách hàng</returns>
        /// CreatedBy: PTANH (21/5/2021)
        public Customer GetCustomerByCode(string customerCode);

        /// <summary>
        /// Lấy khách hàng theo số điện thoại
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại khách hàng</param>
        /// <returns>Đối tượng khách hàng</returns>
        /// CreatedBy: PTANH (21/5/2021)
        public Customer GetCustomerByPhoneNumber(string phoneNumber);

        /// <summary>
        /// Lấy khách hàng theo mã khách hàng nhưng khác Id của khách hàng hiện tại
        /// </summary>
        /// <param name="customerId">Id của khách hàng hiện tại</param>
        /// <param name="customerCode">Mã của khách hàng hiện tại</param>
        /// <returns>Đối tượng khách hàng</returns>
        public Customer GetCustomerByCodeAndAnthorId(Guid customerId, string customerCode);

        /// <summary>
        /// Lấy khách hàng theo số điện thoại nhưng khác Id của khách hàng hiện tại
        /// </summary>
        /// <param name="customerId">Id của khách hàng hiện tại</param>
        /// <param name="phoneNumber">Số điện thoại của khách hàng hiện tại</param>
        /// <returns>Đối tượng khách hàng</returns>
        public Customer GetCustomerByPhoneNumberAndAnthorId(Guid customerId, string phoneNumber);
    }
}