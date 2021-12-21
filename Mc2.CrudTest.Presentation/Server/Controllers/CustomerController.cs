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

        [HttpGet]
        [Route("/GetCustomers")]
        public async Task<List<GetCustomersQueryResult>> Get()
        {
            var response = await Mediator.Send(new GetCustomersQuery()).ConfigureAwait(false);
            return response.Data;
        }


        [HttpPost]
        [Route("/InsertCustomer")]
        public async Task<IActionResult> InsertCustomer(AddOrModifyCustomerCommand model)
        {
            var response = await Mediator.Send(model).ConfigureAwait(false);
            return Ok(response);
        }

    }
}
