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
        private ServiceResult _serviceResult;

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _serviceResult = new ServiceResult();
            _baseRepository = baseRepository;
        }

        public virtual ServiceResult Add(T entity)
        {
            // validate dữ liệu
            ValidateObject(entity);
            if (_serviceResult.MisaServiceCode == MISAServiceCode.BadRequest)
            {
                return _serviceResult;
            }
            var rowEffects = _baseRepository.Add(entity);
            if (rowEffects == 0)
            {
                _serviceResult = new ServiceResult() { Data = rowEffects, Messenger = { "Tạo thất bại" }, MisaServiceCode = MISAServiceCode.NoContent };
            }
            else
            {
                _serviceResult = new ServiceResult() { Data = rowEffects, Messenger = new List<string> { "Tạo thành công" }, MisaServiceCode = MISAServiceCode.Created };
            }
            return _serviceResult;
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

        public ServiceResult GetEntitiesPaging(int pageIndex, int pageSize, string filter)
        {
            ServiceResult serviceResult;
            if (pageIndex < 1 || pageSize < 1)
            {
                serviceResult = new ServiceResult()
                {
                    Messenger = { Properties.Resources.Error_Invalid },
                    Data = { },
                    MisaServiceCode = MISAServiceCode.InValid
                };
            }
            var entities = _baseRepository.GetEntitiesPaging(pageIndex, pageSize, filter);
            serviceResult = new ServiceResult()
            {
                Messenger = new List<string> { Properties.Resources.Msg_Success },
                Data = entities,
                MisaServiceCode = MISAServiceCode.Success
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

        private void ValidateObject(T entity)
        {
            var properties = typeof(T).GetProperties();
            _serviceResult.Messenger = new List<string>();
            foreach (var property in properties)
            {
                var propValue = property.GetValue(entity);
                // Nếu có attribute là Required thì thực hiện kiểm tra bắt buộc nhập
                if (property.IsDefined(typeof(MISARequired), true) && (propValue == null || propValue.ToString() == string.Empty))
                {
                    var requiredAttribute = property.GetCustomAttributes(typeof(MISARequired), true).FirstOrDefault();
                    if (requiredAttribute != null)
                    {
                        var propertyText = (requiredAttribute as MISARequired).PropertyName;
                        var errorMessage = (requiredAttribute as MISARequired).ErrorMessage;
                        _serviceResult.Messenger.Add(errorMessage == null ? $"{propertyText} {Properties.Resources.Error_Required}".ToString() : errorMessage.ToString());
                    }
                    _serviceResult.MisaServiceCode = MISAServiceCode.BadRequest;
                }

                if (property.IsDefined(typeof(MISALength), true))
                {
                    var lengthAttribute = property.GetCustomAttributes(typeof(MISALength), true).FirstOrDefault();
                    var characterLength = (lengthAttribute as MISALength).CharacterLength;
                    if (lengthAttribute != null && propValue.ToString().Length > characterLength)
                    {
                        var propertyText = (lengthAttribute as MISALength).PropertyName;
                        var errorMessage = (lengthAttribute as MISALength).ErrorMessage;
                        _serviceResult.Messenger.Add(errorMessage == null ? $"{propertyText} {Properties.Resources.Error_Length } {characterLength} ký tự".ToString() : errorMessage.ToString());
                    }
                }

                if (property.IsDefined(typeof(MISADatetime), true))
                {
                    var datetimeAttribute = property.GetCustomAttributes(typeof(MISADatetime), true).FirstOrDefault();
                    var startDay = (datetimeAttribute as MISADatetime).StartDay;
                    var endDay = (datetimeAttribute as MISADatetime).EndDay;
                    if (datetimeAttribute != null)
                    {
                        if (property.GetType() != typeof(DateTime))
                        {
                            var propertyText = (datetimeAttribute as MISADatetime).PropertyName;
                            var errorMessage = (datetimeAttribute as MISADatetime).ErrorMessage;
                            _serviceResult.Messenger.Add(errorMessage == null ? $"{propertyText} {Properties.Resources.Error_TypeOf } ngày tháng năm".ToString() : errorMessage.ToString());
                        }
                        DateTime dayCheck = DateTime.Parse(propValue.ToString());
                        if (DateTime.Compare(startDay, dayCheck) < 0)
                        {
                            var propertyText = (datetimeAttribute as MISADatetime).PropertyName;
                            var errorMessage = (datetimeAttribute as MISADatetime).ErrorMessage;
                            _serviceResult.Messenger.Add(errorMessage == null ? $"{propertyText} {Properties.Resources.Error_StartDay } {startDay.ToString()}".ToString() : errorMessage.ToString());
                        }

                        if (DateTime.Compare(endDay, dayCheck) > 0)
                        {
                            var propertyText = (datetimeAttribute as MISADatetime).PropertyName;
                            var errorMessage = (datetimeAttribute as MISADatetime).ErrorMessage;
                            _serviceResult.Messenger.Add(errorMessage == null ? $"{propertyText} {Properties.Resources.Error_EndDay } {endDay.ToString()}".ToString() : errorMessage.ToString());
                        }
                    }
                }
            }

            //foreach (var property in properties)
            //{
            //    var propValue = property.GetValue(entity);
            //    var propName = property.Name;
            //    var talbeName = typeof(T).ToString();
            //    // Nếu có attribute là duplicate thì thực hiện kiểm tra bắt buộc nhập
            //    if (property.IsDefined(typeof(CheckDuplicate), true))
            //    {
            //        var requiredAttribute = property.GetCustomAttributes(typeof(CheckDuplicate), true).FirstOrDefault();
            //        if (requiredAttribute != null)
            //        {
            //            var propertyText = (requiredAttribute as CheckDuplicate).PropertyName;
            //            var errorMessage = (requiredAttribute as CheckDuplicate).ErrorMessage.ToString();
            //            var sql = $"Proc_Get{talbeName}By{propName}";
            //            var entity = _baseRepository;
            //            _serviceResult.Messenger.Add(errorMessage == null ? $"{propertyText} {Properties.Resources.Error_Duplicate}".ToString() : errorMessage);
            //        }
            //        _serviceResult.MisaServiceCode = MISAServiceCode.BadRequest;
            //    }
            //}
        }
    }
}