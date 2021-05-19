using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infarstructure
{
    public class CustomerContext : ICustomerContext
    {
        IDbConnection dbConnection;

        #region Constructor
        public CustomerContext(IConfiguration configuration)
        {
            // Khởi tạo kết nối với database
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            dbConnection = new MySqlConnection(connectionString);
        }

        #endregion

        #region Method
        //Lấy toàn bộ danh sách khách hàng
        /// <summary>
        /// Lấy toàn bộ danh sách khách hàng
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        /// CreatedBy: PTANH (19/5/2021)
        public IEnumerable<Customer> GetCustomers()
        {
            // Kết nối tới CSDL: 
            // Khởi tạo các commandText:
            var customers = dbConnection.Query<Customer>("Proc_GetCustomers", commandType: CommandType.StoredProcedure);
            // Trả về dữ liệu
            return customers;
        }
        // Lấy thông tin khách hàng theo mã khách hàng:

        // Thêm mới khách hàng:
        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customer">Đối tượng khách hàng</param>
        /// <returns>Số lượng khách hàng được thêm mới</returns>
        /// CreatedBy: PTANH (19/5/2021)
        public int InsertCustomer(Customer customer)
        {
            // Xử lý các kiểu dữ liệu (mapping dataType):
            var properties = customer.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(customer);
                var propertyType = property.PropertyType;

                if(propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }

            }
            // Thực thi commandText:
            var rowAffects = dbConnection.Execute("Proc_InsertCustomer", parameters, commandType: CommandType.StoredProcedure);
            // Trả về kết quả: (số bản ghi mới thêm được)
            return rowAffects;
        }

        // Sửa thông tin khách hàng:

        // Xóa khách hàng theo khóa chính:

        /// <summary>
        /// Lấy khách hàng theo mã khách hàng
        /// </summary>
        /// <param name="code">Mã khách hàng</param>
        /// <returns>Đối tượng khách hàng</returns>
        /// CreatedBy: PTANH (19/5/2021)
        public Customer GetCustomerByCode(string code)
        {
            // Thực thi commandText:
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add("@CustomerCode", code);
            var customer = dbConnection.Query<Customer>("Proc_GetCustomerByCode", dynamicParam, commandType:CommandType.StoredProcedure).FirstOrDefault();
            return customer;
        }
        #endregion
    }
}
