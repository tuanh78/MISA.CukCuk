using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces
{
    public interface IBaseService<TEntity>
    {
        /// <summary>
        /// Lấy danh sách đối tượng
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        /// CreatedBy: PTANH (20/5/2021)
        IEnumerable<TEntity> GetEntities();
        /// <summary>
        /// Lấy đối tượng theo Id
        /// </summary>
        /// <param name="entityId">Id của đối tượng</param>
        /// <returns>Đối tượng cần tìm</returns>
        /// CreatedBy: PTANH (20/5/2021)
        TEntity GetEntity(Guid entityId);
        /// <summary>
        /// Thêm đối tượng mới
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Số bản ghi đã thêm</returns>
        /// CreatedBy: PTANH (20/5/2021)
        int Add(TEntity entity);
        /// <summary>
        /// Sửa thông tin đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần sửa thông tin</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: PTANH (20/5/2021)
        int Update(TEntity entity, Guid entityId);
        /// <summary>
        /// Xóa một đối tượng
        /// </summary>
        /// <param name="entityId">ID của đối tượng cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        int Delete(Guid entityId);
    }
}
