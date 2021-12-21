using Mc2.CrudTest.Application.Interfaces.Services.Base;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;

namespace Mc2.CrudTest.Presentation.Server.Controllers.Base
{
    public static class Gateway
    {
        public static ServiceResponse<T> CallAPI<T>(this object modelInput, string baseAddress, string routeName)
        {
            string finalResult = string.Empty;
            var response = new ServiceResponse<T>();

            try
            {
                if (WebRequest.Create(baseAddress + routeName) is HttpWebRequest req)
                {
                    req.Method = "POST";
                    req.ContentType = "application/json";
                    req.ServicePoint.Expect100Continue = false;

                    var json = JsonConvert.SerializeObject(modelInput);
                    var bytes = Encoding.UTF8.GetBytes(json);
                    req.GetRequestStream().Write(bytes, 0, bytes.Length);

                    var result = req.GetResponse() as HttpWebResponse;

                    if (result != null)
                    {
                        var responseStream = result.GetResponseStream();
                        if (responseStream != null)
                        {
                            finalResult = new StreamReader(responseStream).ReadToEnd();
                            response = JsonConvert.DeserializeObject<ServiceResponse<T>>(finalResult);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var webResponse = ex.Response as HttpWebResponse;
                    if (response != null)
                    {

                    }
                    else
                    {
                        // no http status code available
                    }
                }
                else
                {
                    // no http status code available
                }
                response.SetResult(ResultStatus.Exception, "Error", ex.Message);
            }

            return response;
        }
    }
}
