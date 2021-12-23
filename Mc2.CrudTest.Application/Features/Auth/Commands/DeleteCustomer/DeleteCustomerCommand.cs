using Mc2.CrudTest.Application.Interfaces.Services;
using Mc2.CrudTest.Application.Interfaces.Services.Base;
using Mc2.CrudTest.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Features.Auth.Commands
{
    public partial class DeleteCustomerCommand : Customer,IRequest<ServiceResponse<bool>>
    {
      
    }

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ServiceResponse<bool>>
    {
        private readonly IAuthService Auth;
        public DeleteCustomerCommandHandler(IAuthService Auth)
        {
            this.Auth = Auth;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            ServiceResponse<bool> response = await Auth.DeleteCustomer(request);
            return response;
        }

    }
}
