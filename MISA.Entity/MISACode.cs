using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Entity
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
        /// Dữ liệu không hợp lệ
        /// </summary>
        NotValid = 400,
        /// <summary>
        /// Thành công
        /// </summary>
        Success = 200
    }
}
