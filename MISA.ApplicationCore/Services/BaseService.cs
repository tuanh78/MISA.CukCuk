using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity>
    {
        IBaseRepository<TEntity> _baseRepository;
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public int Add(TEntity entity)
        {
            var rowEffects = _baseRepository.Add(entity);
            return rowEffects;
        }

        public int Delete(Guid entityId)
        {
            var rowEffects = _baseRepository.Delete(entityId);
            return rowEffects;
        }

        public IEnumerable<TEntity> GetEntities()
        {
            var entities = _baseRepository.GetEntities();
            return entities;
        }

        public TEntity GetEntity(Guid entityId)
        {
            var entity = _baseRepository.GetEntity(entityId);
            return entity;
        }

        public int Update(TEntity entity, Guid entityId)
        {
            var rowEffects = _baseRepository.Update(entity, entityId);
            return rowEffects;
        }
    }
}
