using Mc2.CrudTest.Application.Interfaces.Services;
using Mc2.CrudTest.Application.Interfaces.Services.Base;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Features.Auth.Commands
{
    public partial class DeleteCustomerCommand : IRequest<ServiceResponse<bool>>
    {
        public string Email { get; set; }
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
            var response = await Auth.DeleteCustomer(request);
            return response;
        }

    }
}
