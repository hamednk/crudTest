using Mc2.CrudTest.Application.Interfaces.Services;
using Mc2.CrudTest.Application.Interfaces.Services.Base;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Mc2.CrudTest.Domain;

namespace Mc2.CrudTest.Application.Features.Auth.Commands
{
    public partial class AddOrModifyCustomerCommand : Customer, IRequest<ServiceResponse<int>>
    {

    }

    public class AddOrModifyCustomerCommandHandler : IRequestHandler<AddOrModifyCustomerCommand, ServiceResponse<int>>
    {
        private readonly IAuthService _auth;
        public AddOrModifyCustomerCommandHandler(IAuthService auth)
        {
            this._auth = auth;
        }

        public async Task<ServiceResponse<int>> Handle(AddOrModifyCustomerCommand request, CancellationToken cancellationToken)
        {
            ServiceResponse<int> response = await _auth.AddOrModifyCustomer(request);
            return response;
        }

    }
}
