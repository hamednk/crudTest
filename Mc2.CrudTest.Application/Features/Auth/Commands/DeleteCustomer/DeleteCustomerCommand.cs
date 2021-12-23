using System.Threading;
using System.Threading.Tasks;
using Mc2.CrudTest.Application.Interfaces.Services;
using Mc2.CrudTest.Application.Interfaces.Services.Base;
using Mc2.CrudTest.Domain;
using MediatR;

namespace Mc2.CrudTest.Application.Features.Auth.Commands.DeleteCustomer
{
    public partial class DeleteCustomerCommand : Customer,IRequest<ServiceResponse<bool>>
    {
      
    }

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ServiceResponse<bool>>
    {
        private readonly IAuthService _auth;
        public DeleteCustomerCommandHandler(IAuthService auth)
        {
            this._auth = auth;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            ServiceResponse<bool> response = await _auth.DeleteCustomer(request);
            return response;
        }

    }
}
