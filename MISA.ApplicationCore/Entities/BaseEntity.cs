using MISA.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class BaseEntity
    {
        /// <summary>
        /// Trường khóa chính
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>

        public string CreatedBy { get; set; }

        /// <summary>
        /// Trạng thái của object
        /// </summary>
        public EditMode EditMode { get; set; }
    }
}