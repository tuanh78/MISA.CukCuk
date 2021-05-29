using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Enums
{
    /// <summary>
    /// Các số quy định giới tính
    /// </summary>
    /// CreatedBy: PTANH(27/05/2021)
    public enum GenderEnum
    {
        /// <summary>
        /// Giới tính nam
        /// </summary>
        Male = 0,

        /// <summary>
        /// Giới tính nữ
        /// </summary>
        Female = 1,

        /// <summary>
        /// Giới tính khác
        /// </summary>
        Other = 2
    }

    /// <summary>
    /// MISACode để xác định các trạng thái validate
    /// </summary>
    /// CreatedBy: PTANH(27/05/2021)
    public enum MISAServiceCode
    {
        /// <summary>
        /// Dữ liệu hợp lệ
        /// </summary>
        BadRequest = 400,

        /// <summary>
        /// Mã khách hàng không hợp lệ
        /// </summary>
        Duplicate = 600,

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
        EmptyValue = 610,

        /// <summary>
        /// Tạo thành công
        /// </summary>

        Created = 401,

        /// <summary>
        /// Không có dữ liệu
        /// </summary>
        NoContent = 204
    }
}