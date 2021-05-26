using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.Web.Controllers
{
    public class CustomerGroupController : BaseEntityController<CustomerGroup>
    {
        private ICustomerGroupService _customerGroupService;

        public CustomerGroupController(IBaseService<CustomerGroup> baseService, ICustomerGroupService customerGroupService) : base(baseService)
        {
            _customerGroupService = customerGroupService;
        }
    }
}