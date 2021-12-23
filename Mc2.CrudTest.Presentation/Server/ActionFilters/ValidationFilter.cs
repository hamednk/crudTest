using Mc2.CrudTest.Application.Interfaces.Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace Mc2.CrudTest.Presentation.Server.ActionFilters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ServiceResponse<List<string>> result = new ServiceResponse<List<string>>();

                List<string> data = context.ModelState
                     .Values
                     .SelectMany(v => v.Errors.Select(b => b.ErrorMessage))
                     .ToList();


                result.SetResult(ResultStatus.ValidationError, "خطا", data);

                context.Result = new JsonResult(result)
                {
                    StatusCode = 400
                };
            }
        }
    }
}
