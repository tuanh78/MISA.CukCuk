using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore;
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
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomerService customerService;
        #region Constructor
        public CustomersController(ICustomerService _customerService)
        {
            customerService = _customerService;
        }
        #endregion

        #region Method
        // GET: api/<CustomersController>
        [HttpGet]
        public IActionResult Get()
        {
            var customers = customerService.GetCustomers();
            return Ok(customers);
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CustomersController>
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            var serviceResult = customerService.InsertCustomer(customer);
            if(serviceResult.MisaCode == MISACode.NotValid)
            {
                return BadRequest("Mã khách hàng đã tồn tại");
            }
            else if(serviceResult.MisaCode == MISACode.Success && (int)serviceResult.Data > 0)
            {
                return Ok("Them khach hang thanh cong");
            }
            else
            {
                return NoContent();
            }

        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        #endregion
    }
}
