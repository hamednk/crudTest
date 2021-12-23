using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Mc2.CrudTest.Presentation.Server.Controllers.Base
{
    [ApiController]
    [Produces("application/json")]
    public abstract class BaseAPIController : Controller
    {
        public BaseAPIController()
        {
            CultureInfo newCulture = new CultureInfo("fa-IR", false);
            newCulture.DateTimeFormat.LongDatePattern = "yyyy/MM/dd HH:mm:ss";
            newCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            newCulture.DateTimeFormat.DateSeparator = "/";

            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;
        }
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public override JsonResult Json(object data)
        {
            return Json(data, JsonDefaultSerializerSettings());
        }

        public override OkObjectResult Ok([ActionResultObjectValue] object value)
        {
            OkObjectResult response = new OkObjectResult(value);
            dynamic result = value;

            response.Value = value;
            response.StatusCode = 200;
 
            return response;
        }

        protected JsonSerializerSettings JsonDefaultSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter>() { new StringEnumConverter() },
                DateParseHandling = DateParseHandling.DateTime,
                DateFormatString = "yyyy/MM/dd",
                Culture = CultureInfo.CurrentCulture,
                Formatting = Formatting.Indented,
            };
        }
 
    }
}
