using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Enums
{
    /// <summary>
    /// MISACode để xác định các trạng thái validate
    /// </summary>
    public enum MISACode
    {
        /// <summary>
        /// Dữ liệu hợp lệ
        /// </summary>
        IsValid = 100,

        /// <summary>
        /// Mã khách hàng không hợp lệ
        /// </summary>
        DuplicateCustomerCode = 600,

        /// <summary>
        /// Dữ liệu không hợp lệ
        /// </summary>
        InValid = 420,

        /// <summary>
        /// Thành công
        /// </summary>
        Success = 200,

        /// <summary>
        /// Dữ liệu trống
        /// </summary>
        EmptyValue = 610
    }
}