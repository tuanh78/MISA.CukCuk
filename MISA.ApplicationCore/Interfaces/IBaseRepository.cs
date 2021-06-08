using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Lấy danh sách đối tượng
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        /// CreatedBy: PTANH (20/5/2021)
        IEnumerable<T> GetEntities();

        /// <summary>
        /// Lấy đối tượng theo Id
        /// </summary>
        /// <param name="entityId">Id của đối tượng</param>
        /// <returns>Đối tượng cần tìm</returns>
        /// CreatedBy: PTANH (20/5/2021)
        T GetEntity(Guid entityId);

        /// <summary>
        /// Thêm đối tượng mới
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Số bản ghi đã thêm</returns>
        /// CreatedBy: PTANH (20/5/2021)
        int Add(T entity);

        /// <summary>
        /// Sửa thông tin đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần sửa thông tin</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: PTANH (20/5/2021)
        int Update(T entity, Guid entityId);

        int SaveData(T entity)


        /// <summary>
        /// Xóa một đối tượng
        /// </summary>
        /// <param name="entityId">ID của đối tượng cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: PTANH (23/5/2021)
        int Delete(Guid entityId);

        /// <summary>
        /// Lấy danh sách đối tượng qua phân trang và tìm kiếm
        /// </summary>
        /// <param name="pageIndex">Vị trí trang</param>
        /// <param name="pageSize">Số lượng bản ghi trên một trang</param>
        /// <param name="filter">Điều kiện lọc thêm</param>
        /// <returns>Danh sách đối tượng</returns>
        /// CreatedBy: PTANH (23/5/2021)
        IEnumerable<T> GetEntitiesPaging(int pageIndex, int pageSize, string filter);

        /// <summary>
        /// Lấy ra tổng số lượng bản ghi
        /// </summary>
        /// <returns>Tổng số lượng bản ghi</returns>
        /// CreatedBy: PTANH (24/05/2021)
        int GetTotalRecords();

        /// <summary>
        /// Xóa nhiều bản ghi một lần
        /// </summary>
        /// <returns>Số lượng bản ghi bị xóa</returns>
        /// CreatedBy: PTANH (24/05/2021)
        int DeleteMultiRecords(Guid[] guids);
    }
}