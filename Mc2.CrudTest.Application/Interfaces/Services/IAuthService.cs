using Mc2.CrudTest.Application.Features.Auth.Commands.AddOrModifyCustomer;
using Mc2.CrudTest.Application.Features.Auth.Commands.DeleteCustomer;
using Mc2.CrudTest.Application.Features.Auth.Queries.GetCustomers;

namespace Mc2.CrudTest.Application.Interfaces.Services
{
    using Mc2.CrudTest.Application.Features.Auth.Commands;
    using Mc2.CrudTest.Application.Features.Auth.Queries;
    using Mc2.CrudTest.Application.Interfaces.Services.Base;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAuthService
    {
        Task<ServiceResponse<int>> AddOrModifyCustomer(AddOrModifyCustomerCommand command);
        Task<ServiceResponse<bool>> DeleteCustomer(DeleteCustomerCommand command);
        Task<ServiceResponse<List<GetCustomersQueryResult>>> GetCustomers(GetCustomersQuery command);
    }
}
