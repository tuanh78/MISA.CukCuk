using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntityController<T> : ControllerBase
    {
        protected IBaseService<T> _baseService;

        public BaseEntityController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }

        // GET: api/<BaseEntityController>
        [HttpGet]
        public IActionResult Get()
        {
            var entities = _baseService.GetEntities();
            if (entities == null)
            {
                return NoContent();
            }
            return Ok(entities);
        }

        // GET api/<BaseEntityController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var entity = _baseService.GetEntity(id);
            if (entity == null)
            {
                return NoContent();
            }
            return Ok(entity);
        }

        // POST api/<BaseEntityController>
        [HttpPost]
        public virtual IActionResult Post([FromBody] T entity)
        {
            var serviceResult = _baseService.Add(entity);
            if (serviceResult.MisaServiceCode == MISAServiceCode.NoContent)
            {
                return NoContent();
            }
            return Created("Them thanh cong", serviceResult);
        }

        // PUT api/<BaseEntityController>/5
        [HttpPut("{id}")]
        public virtual IActionResult Put(Guid id, [FromBody] T entity)
        {
            var rowEffects = _baseService.Update(entity, id);
            if (rowEffects < 1)
            {
                return NoContent();
            }
            return Ok(rowEffects);
        }

        // DELETE api/<BaseEntityController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var rowEffects = _baseService.Delete(id);
            if (rowEffects < 1)
            {
                return NoContent();
            }
            return Ok(rowEffects);
        }

        [HttpGet("paging")]
        public IActionResult Paging([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] string filter)
        {
            var entities = _baseService.GetEntitiesPaging(pageIndex, pageSize, filter);
            IList<T> collectionEntities = (IList<T>)entities.Data;
            if (entities.MisaServiceCode == MISAServiceCode.InValid)
            {
                return BadRequest(entities);
            }
            else if (collectionEntities.Count == 0)
            {
                return NoContent();
            }
            return Ok(entities.Data);
        }

        [HttpGet("total-record")]
        public IActionResult GetTotalRecords()
        {
            var totalRecords = _baseService.GetTotalRecords();
            return Ok(totalRecords);
        }

        [HttpDelete("multiple-records")]
        public IActionResult DeleteMultipleRecords(Guid[] guids)
        {
            var rowEffects = _baseService.DeleteMultiRecords(guids);
            return Ok(rowEffects);
        }
    }
}