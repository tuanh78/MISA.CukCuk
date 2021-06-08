using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MISA.Infarstructure
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        protected IDbConnection _dbConnection;
        protected string _tableName = typeof(T).Name;

        #region Constructor

        public BaseRepository(IConfiguration configuration)
        {
            // Khởi tạo kết nối với database
            string connectionString = configuration.GetConnectionString("MISACukCukConnectionString");
            _dbConnection = new MySqlConnection(connectionString);
        }

        #endregion Constructor

        public int SaveData(T entity)
        {
            try
            {
                var properties = entity.GetType().GetProperties();
                var parameters = new DynamicParameters();
                var rowAffects = 0;
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
                var editMode = (EditMode)entity.GetType().GetProperty("EditMode").GetValue(entity);
                var sql = string.Empty;
                if (editMode == EditMode.Add)
                {
                    sql = $"Proc_Insert{_tableName}";
                }
                else if (editMode == EditMode.Edit)
                {
                    sql = $"Proc_Update{_tableName}";
                }
                else if (editMode == EditMode.Delete)
                {
                    sql = $"Proc_Delete{_tableName}";
                }
                // Thực thi commandText:
                if (!string.IsNullOrWhiteSpace(sql))
                {
                    rowAffects = _dbConnection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
                }
                // Trả về kết quả: (số bản ghi mới thêm được)
                return rowAffects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Add(T entity)
        {
            try
            {
                var properties = entity.GetType().GetProperties();
                var parameters = new DynamicParameters();
                var rowAffects = 0;
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
                rowAffects = _dbConnection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
                // Trả về kết quả: (số bản ghi mới thêm được)
                return rowAffects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Delete(Guid entityId)
        {
            try
            {
                var sql = $"Proc_Delete{_tableName}";
                var rowAffects = 0;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{_tableName}Id", entityId);
                rowAffects = _dbConnection.Execute(sql, dynamicParameters, commandType: CommandType.StoredProcedure);
                return rowAffects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteMultiRecords(Guid[] guids)
        {
            _dbConnection.Open();
            MySqlTransaction mySqlTransaction = (MySqlTransaction)_dbConnection.BeginTransaction();
            int count = 0;
            try
            {
                string sql = $"Proc_Delete{_tableName}";
                foreach (Guid entityId in guids)
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add($"@{_tableName}Id", entityId);
                    var rowAffects = _dbConnection.Execute(sql, dynamicParameters, commandType: CommandType.StoredProcedure);
                    count += rowAffects;
                }
                mySqlTransaction.Commit();
            }
            catch (Exception)
            {
                mySqlTransaction.Rollback();
            }
            finally
            {
                _dbConnection.Close();
            }
            return count;
        }

        public IEnumerable<T> GetEntities()
        {
            try
            {
                var sql = $"Proc_Get{_tableName}s";
                var entities = _dbConnection.Query<T>(sql, commandType: CommandType.StoredProcedure);
                return entities;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<T> GetEntitiesPaging(int pageIndex, int pageSize, string filter)
        {
            try
            {
                var sql = $"Proc_Get{_tableName}sPaging";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@PageIndex", pageIndex);
                dynamicParameters.Add("@PageSize", pageSize);
                dynamicParameters.Add("@Filter", filter);
                var entities = _dbConnection.Query<T>(sql, dynamicParameters, commandType: CommandType.StoredProcedure);
                return entities;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T GetEntity(Guid entityId)
        {
            try
            {
                var sql = $"Proc_Get{_tableName}ById";
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{_tableName}Id", entityId);
                var entity = _dbConnection.Query<T>(sql, dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetTotalRecords()
        {
            try
            {
                var sql = $"Proc_GetTotal{_tableName}s";
                var totalRecords = _dbConnection.QueryFirst<int>(sql, commandType: CommandType.StoredProcedure);
                return totalRecords;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Update(T entity, Guid entityId)
        {
            try
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
                rowAffects = _dbConnection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
                // Trả về kết quả: (số bản ghi mới thêm được)
                return rowAffects;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}