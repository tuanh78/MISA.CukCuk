using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infarstructure
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Customer GetCustomerByCodeAndAnthorId(Guid customerId, string customerCode)
        {
            try
            {
                var sql = $"Proc_Get{_tableName}ByCodeAndAnthorId";
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{_tableName}Id", customerId);
                dynamicParameters.Add($"@{_tableName}Code", customerCode);
                var customer = _dbConnection.Query<Customer>(sql, dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return customer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Customer GetCustomerByCode(string customerCode)
        {
            try
            {
                var sql = $"Proc_Get{_tableName}ByCode";
                var customer = _dbConnection.Query<Customer>(sql, new { CustomerCode = customerCode }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                return customer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Customer GetCustomerByPhoneNumber(string phoneNumber)
        {
            try
            {
                var sql = $"Proc_Get{_tableName}ByPhoneNumber";
                var customer = _dbConnection.Query<Customer>(sql, new { phoneNumber = phoneNumber }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                return customer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Customer GetCustomerByPhoneNumberAndAnthorId(Guid customerId, string phoneNumber)
        {
            try
            {
                var sql = $"Proc_Get{_tableName}ByPhoneNumberAndAnthorId";
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{_tableName}Id", customerId);
                dynamicParameters.Add($"@PhoneNumber", phoneNumber);
                var customer = _dbConnection.Query<Customer>(sql, dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return customer;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}