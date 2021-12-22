using Mc2.CrudTest.Application.Features.Auth.Commands;
using Mc2.CrudTest.Application.Features.Auth.Queries;
using Mc2.CrudTest.Application.Interfaces.Services;
using Mc2.CrudTest.Application.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class AuthService : IAuthService
    {
        private IGenericService Service { get; }

        public AuthService(IGenericService genericService)
        {
            Service = genericService;
        }

        public async Task<ServiceResponse<int>> AddOrModifyCustomer(AddOrModifyCustomerCommand command)
        {
            var response = new ServiceResponse<int>();
            try
            {
                if (command.Id.HasValue)
                    response = await Service.Edit<int>("usp_Customer_Update", command).ConfigureAwait(false);
                else
                    response = await Service.Add<int>("usp_Customer_Insert", command).ConfigureAwait(false);

                if (response.ResultStatus == ResultStatus.Successful)
                {
                    var message = "Save Successful!";
                    response.SetResult(ResultStatus.Successful, "Success", message, 200);
                }
                else
                {
                    var message = "Save Error !";
                    response.SetResult(ResultStatus.Exception, "Error", message, 400);
                }
            }
            catch (Exception ex)
            {
                response.SetResult(ResultStatus.Exception, "Error", "Has Error! Try Again!", 400, ex);
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteCustomer(DeleteCustomerCommand command)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                response = await Service.Delete("usp_Customer_Delete", new { Id = command.Id }).ConfigureAwait(false);

                if (response.ResultStatus == ResultStatus.Successful)
                {
                    var message = "Delete Successful!";
                    response.SetResult(ResultStatus.Successful, "Success", message, 200);
                }
                else
                {
                    var message = "Delete Error !";
                    response.SetResult(ResultStatus.Exception, "Error", message, 400);
                }
            }
            catch (Exception ex)
            {
                response.SetResult(ResultStatus.Exception, "Error", "Has Error! Try Again!", 400, ex);
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetCustomersQueryResult>>> GetCustomers(GetCustomersQuery command)
        {
            var response = new ServiceResponse<List<GetCustomersQueryResult>>();
            try
            {
                response = await Service.List<GetCustomersQueryResult>("usp_Customer_List", command, System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                response.SetResult(ResultStatus.Exception, "Error", "Has Error! Try Again!", 400, ex);
            }

            return response;
        }


    }
}
