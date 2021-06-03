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
    /// Attribute check rỗng
    /// </summary>
    public class MISARequired : Attribute
    {
        /// <summary>
        /// Tên thuộc tính
        /// </summary>
        public string PropertyName;

        /// <summary>
        /// Câu thông báo tùy chỉnh
        /// </summary>
        public string ErrorMessage;

        public MISARequired(string propertyName, string errorMessage = null)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
    }

    /// <summary>
    /// Attribute kiểm tra độ dài dữ liệu
    /// </summary>
    public class MISALength : Attribute
    {
        /// <summary>
        /// Tên thuộc tính
        /// </summary>
        public string PropertyName;

        /// <summary>
        /// Thông báo lỗi tùy chỉnh
        /// </summary>
        public string ErrorMessage;

        public int CharacterLength;

        public MISALength(string propertyName, string errorMessage, int characterLength)
        {
            this.PropertyName = propertyName;
            this.ErrorMessage = errorMessage;
            this.CharacterLength = characterLength;
        }
    }

    /// <summary>
    /// Attribute check dữ liệu ngày tháng
    /// </summary>
    public class MISADatetime : Attribute
    {
        /// <summary>
        /// Tên thuộc tính
        /// </summary>
        public string PropertyName;

        /// <summary>
        /// Câu thông báo lỗi tùy chỉnh
        /// </summary>
        public string ErrorMessage;

        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        public DateTime StartDay;

        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public DateTime EndDay;

        public MISADatetime(string propertyName, string errorMessage, DateTime startDay)
        {
            this.PropertyName = propertyName;
            this.ErrorMessage = errorMessage;
            //this.StartDay = datestartDay;
            //this.EndDay = endDay;
        }
    }

    ///// <summary>
    ///// Attribute để xác định check trùng
    ///// </summary>
    //[AttributeUsage(AttributeTargets.Property)]
    //public class CheckDuplicate : Attribute
    //{
    //    /// <summary>
    //    /// Tên của property
    //    /// </summary>
    //    public string PropertyName;

    //    /// <summary>
    //    /// Câu thông báo tùy chỉnh
    //    /// </summary>
    //    public string ErrorMessage;

    //    public CheckDuplicate(string propertyName, string errorMessage = null)
    //    {
    //        this.PropertyName = propertyName;
    //        this.ErrorMessage = errorMessage;
    //    }
    //}
}