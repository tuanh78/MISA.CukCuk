using Dapper;
using Microsoft.Extensions.Configuration;
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
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        IDbConnection _dbConnection;
        string _tableName = typeof(TEntity).Name;

        #region Constructor
        public BaseRepository(IConfiguration configuration)
        {
            // Khởi tạo kết nối với database
            string connectionString = configuration.GetConnectionString("MISACukCukConnectionString");
            _dbConnection = new MySqlConnection(connectionString);
        }
        #endregion
        public int Add(TEntity entity)
        {
            var properties = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;

                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }

            }
            // Thực thi commandText:
            var sql = $"Proc_Insert{_tableName}";
            var rowAffects = _dbConnection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
            // Trả về kết quả: (số bản ghi mới thêm được)
            return rowAffects;
        }

        public int Delete(Guid entityId)
        {
            var sql = $"Proc_Delete{_tableName}";
            var rowAffects = _dbConnection.Execute(sql, new { Id = entityId }, commandType: CommandType.StoredProcedure);
            return rowAffects;
        }

        public IEnumerable<TEntity> GetEntities()
        {
            var sql = $"Proc_Get{_tableName}s";
            var entities = _dbConnection.Query<TEntity>(sql, commandType: CommandType.StoredProcedure);
            return entities;

        }

        public TEntity GetEntity(Guid entityId)
        {
            var sql = $"Proc_Get{_tableName}ById";
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@{_tableName}Id", entityId);
            var entity = _dbConnection.Query<TEntity>(sql, dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return entity;
        }

        public int Update(TEntity entity, Guid entityId)
        {
            var properties = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;

                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }

            }

            parameters.Add($"@{_tableName}Id", entityId);
            // Thực thi commandText:
            var sql = $"Proc_Update{_tableName}";
            var rowAffects = 0;
            try
            {
                 rowAffects = _dbConnection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {

                throw ex; 
            }
            // Trả về kết quả: (số bản ghi mới thêm được)
            return rowAffects;
        }
    }
}
