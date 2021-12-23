using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mc2.CrudTest.Application.Features.Auth.Commands.AddOrModifyCustomer;
using Mc2.CrudTest.Application.Features.Auth.Commands.DeleteCustomer;
using Mc2.CrudTest.Application.Features.Auth.Queries.GetCustomers;
using Mc2.CrudTest.Application.Interfaces.Services;
using Mc2.CrudTest.Application.Interfaces.Services.Base;

namespace Mc2.CrudTest.Persistence.Services
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
            ServiceResponse<int> response = new();
            try
            {
                if (command.Id.HasValue)
                    response = await Service.Edit<int>("usp_Customer_Update", command).ConfigureAwait(false);
                else
                    response = await Service.Add<int>("usp_Customer_Insert", command).ConfigureAwait(false);

                if (response.ResultStatus == ResultStatus.Successful)
                {
                    string message = "Save Successful!";
                    response.SetResult(ResultStatus.Successful, "Success", message, 200);
                }
                else
                {
                    string message = "Save Error !";
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
            ServiceResponse<bool> response = new();
            try
            {
                response = await Service.Delete("usp_Customer_Delete", new {command.Id }).ConfigureAwait(false);

                if (response.ResultStatus == ResultStatus.Successful)
                {
                    string message = "Delete Successful!";
                    response.SetResult(ResultStatus.Successful, "Success", message, 200);
                }
                else
                {
                    string message = "Delete Error !";
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
            ServiceResponse<List<GetCustomersQueryResult>> response = new();
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
