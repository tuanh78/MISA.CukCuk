using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.Web.Controllers
{
    public class CustomersController : BaseEntityController<Customer>
    {
        private ICustomerService _customerService;

        public CustomersController(IBaseService<Customer> baseService, ICustomerService customerService) : base(baseService)
        {
            _customerService = customerService;
        }

        public override IActionResult Post([FromBody] Customer customer)
        {
            var serviceResult = _customerService.Add(customer);
            if (serviceResult.MisaServiceCode == MISAServiceCode.BadRequest)
            {
                return BadRequest(serviceResult);
            }
            else if (serviceResult.MisaServiceCode < MISAServiceCode.BadRequest)
            {
                return NoContent();
            }
            return Created("Thêm thành công", serviceResult);
        }

        public override IActionResult Put(Guid id, [FromBody] Customer entity)
        {
            var rowEffects = _customerService.Update(entity, id);
            if (rowEffects == -1)
            {
                return BadRequest("Mã khách hàng đã tồn tại !");
            }
            else if (rowEffects == -2)
            {
                return BadRequest("Số điện thoại đã tồn tại !");
            }
            else if (rowEffects < 1)
            {
                return NoContent();
            }
            return Ok(rowEffects);
        }
    }
}