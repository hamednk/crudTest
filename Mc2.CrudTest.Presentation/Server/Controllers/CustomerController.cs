﻿using Mc2.CrudTest.Presentation.Server.ActionFilters;
using Mc2.CrudTest.Presentation.Server.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mc2.CrudTest.Application.Features.Auth.Commands.AddOrModifyCustomer;
using Mc2.CrudTest.Application.Features.Auth.Commands.DeleteCustomer;
using Mc2.CrudTest.Application.Features.Auth.Queries.GetCustomers;
using Mc2.CrudTest.Application.Interfaces.Services.Base;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [ApiController]
    [ValidationFilter]
    [Route("[controller]")]
    public class CustomerController : BaseApiController
    {
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }
         
        [HttpPost]
        [Route("/GetCustomers")]
        public async Task<IActionResult> Get(GetCustomersQuery model)
        {
            ServiceResponse<List<GetCustomersQueryResult>> response = await Mediator.Send(model).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPost]
        [Route("/InsertCustomer")]
        public async Task<IActionResult> InsertCustomer(AddOrModifyCustomerCommand model)
        {
            ServiceResponse<int> response = await Mediator.Send(model).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPost]
        [Route("/UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(AddOrModifyCustomerCommand model)
        {
            ServiceResponse<int> response = await Mediator.Send(model).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPost]
        [Route("/DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(DeleteCustomerCommand model)
        {
            ServiceResponse<bool> response = await Mediator.Send(model).ConfigureAwait(false);
            return Ok(response);
        }

    }
}
