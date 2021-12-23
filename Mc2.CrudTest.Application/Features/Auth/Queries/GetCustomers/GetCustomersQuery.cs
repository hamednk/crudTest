﻿using Mc2.CrudTest.Application.Interfaces.Services;
using Mc2.CrudTest.Application.Interfaces.Services.Base;
using Mc2.CrudTest.Application.DTO.Customer;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Features.Auth.Queries
{
    public partial class GetCustomersQuery : CustomerSearchDto, IRequest<ServiceResponse<List<GetCustomersQueryResult>>>
    {

    }

    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, ServiceResponse<List<GetCustomersQueryResult>>>
    {
        private readonly IAuthService _service;
        public GetCustomersQueryHandler(IAuthService service)
        {
            this._service = service;
        }

        public async Task<ServiceResponse<List<GetCustomersQueryResult>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            ServiceResponse<List<GetCustomersQueryResult>> response = await _service.GetCustomers(request);
            return response;
        }

    }
}


