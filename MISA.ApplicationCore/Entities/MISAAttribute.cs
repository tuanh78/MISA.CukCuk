using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class MISAAttribute
    {
    }

    /// <summary>
    /// Attribute để xác định check bắt buộc nhập
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : Attribute
    {
        /// <summary>
        /// Tên của property
        /// </summary>
        public string PropertyName;

        /// <summary>
        /// Câu thông báo tùy chỉnh
        /// </summary>
        public string ErrorMessage;

        public Required(string propertyName, string errorMessage = null)
        {
            this.PropertyName = propertyName;
            this.ErrorMessage = errorMessage;
        }
    }

    /// <summary>
    /// Attribute để xác định check trùng
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckDuplicate : Attribute
    {
        /// <summary>
        /// Tên của property
        /// </summary>
        public string PropertyName;

        /// <summary>
        /// Câu thông báo tùy chỉnh
        /// </summary>
        public string ErrorMessage;

        public CheckDuplicate(string propertyName, string errorMessage = null)
        {
            this.PropertyName = propertyName;
            this.ErrorMessage = errorMessage;
        }
    }
}