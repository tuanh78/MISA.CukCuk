using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Services
{
    public class BaseService<T> : IBaseService<T>
    {
        protected IBaseRepository<T> _baseRepository;

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public virtual int Add(T entity)
        {
            var rowEffects = _baseRepository.Add(entity);
            return rowEffects;
        }

        public int Delete(Guid entityId)
        {
            var rowEffects = _baseRepository.Delete(entityId);
            return rowEffects;
        }

        public int DeleteMultiRecords(Guid[] guids)
        {
            int rowEffects = _baseRepository.DeleteMultiRecords(guids);
            return rowEffects;
        }

        public IEnumerable<T> GetEntities()
        {
            var entities = _baseRepository.GetEntities();
            return entities;
        }

        public ServiceResult<T> GetEntitiesPaging(int pageIndex, int pageSize, string filter)
        {
            ServiceResult<T> serviceResult;
            if (pageIndex < 1 || pageSize < 1)
            {
                serviceResult = new ServiceResult<T>()
                {
                    Messenger = "Dữ liệu không hợp lệ",
                    Data = { },
                    MisaCode = MISACode.InValid
                };
            }
            var entities = _baseRepository.GetEntitiesPaging(pageIndex, pageSize, filter);
            serviceResult = new ServiceResult<T>()
            {
                Messenger = "Lấy dữ liệu thành công",
                Data = entities,
                MisaCode = MISACode.Success
            };

            return serviceResult;
        }

        public T GetEntity(Guid entityId)
        {
            var entity = _baseRepository.GetEntity(entityId);
            return entity;
        }

        public int GetTotalRecords()
        {
            var totalRecords = _baseRepository.GetTotalRecords();
            return totalRecords;
        }

        public virtual int Update(T entity, Guid entityId)
        {
            var rowEffects = _baseRepository.Update(entity, entityId);
            return rowEffects;
        }
    }
}