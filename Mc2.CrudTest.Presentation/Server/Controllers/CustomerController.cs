using Mc2.CrudTest.Application.DTO.Customer;
using Mc2.CrudTest.Application.Features.Auth.Commands;
using Mc2.CrudTest.Application.Features.Auth.Queries;
using Mc2.CrudTest.Domain;
using Mc2.CrudTest.Presentation.Server.ActionFilters;
using Mc2.CrudTest.Presentation.Server.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [ApiController]
    [ValidationFilter]
    [Route("[controller]")]
    public class CustomerController : BaseAPIController
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
            var response = await Mediator.Send(model).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPost]
        [Route("/InsertCustomer")]
        public async Task<IActionResult> InsertCustomer(AddOrModifyCustomerCommand model)
        {
            var response = await Mediator.Send(model).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPost]
        [Route("/UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(AddOrModifyCustomerCommand model)
        {
            var response = await Mediator.Send(model).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPost]
        [Route("/DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(DeleteCustomerCommand model)
        {
            var response = await Mediator.Send(model).ConfigureAwait(false);
            return Ok(response);
        }

    }
}
