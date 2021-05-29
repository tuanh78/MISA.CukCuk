﻿using MISA.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class Customer
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>

        [Required("Mã khách hàng")]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Họ tên khách hàng
        /// </summary>
        [Required("Họ và tên")]
        public string FullName { get; set; }

        /// <summary>
        /// Mã thẻ thành viên
        /// </summary>

        public string MemberCardCode { get; set; }

        /// <summary>
        /// Id nhóm khách hàng
        /// </summary>
        public Guid? CustomerGroupId { get; set; }

        public string CustomerGroupName { get; }

        /// <summary>
        /// Ngày sinh
        /// </summary>

        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Email
        /// </summary>

        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [Required("Số điện thoại")]
        [CheckDuplicate("Mã khách hàng", "Mã khách hàng đã tồn tại rồi nhé bro nhé !")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Tên công ty
        /// </summary>

        public string CompanyName { get; set; }

        /// <summary>
        /// Mã Tax công ty
        /// </summary>
        public string CompanyTaxCode { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>

        public string Address { get; set; }

        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case (int?)GenderEnum.Male:
                        return Properties.GenderResource.Male;

                    case (int?)GenderEnum.Female:
                        return Properties.GenderResource.Female;

                    default:
                        return Properties.GenderResource.Other;
                }
            }
        }
    }
}