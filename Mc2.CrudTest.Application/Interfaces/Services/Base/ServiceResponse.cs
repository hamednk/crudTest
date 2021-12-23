namespace Mc2.CrudTest.Application.Interfaces.Services.Base
{
    using Mc2.CrudTest.Application.DTO.JsonConverter;
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string UrlAddress { get; set; }
        public List<string> Errors { get; set; }

        [JsonIgnore]
        public Exception Exception { get; set; }

        [JsonConverter(typeof(EnumDisplayValueConverter))]
        public ResultStatus ResultStatus { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object ExtraData { get; set; }

        public int Total { get; set; }

        public int ResponseCode { get; set; }

        public void SetData(T value, int total = -1, string message = null)
        {
            Data = value;
            IDictionary dictionary = value as IDictionary;
            if (dictionary != null)
            {
                ResultStatus = dictionary.Count == 0 ? ResultStatus.DataNotFound : ResultStatus.Successful;
                total = total == -1 ? dictionary.Count : total;
            }
            else if (value != null && value is IList)
            {
                ResultStatus = ((IList)value).Count == 0 ? ResultStatus.DataNotFound : ResultStatus.Successful;
                total = total == -1 ? ((IList)value).Count : total;
            }
            else
            {
                ResultStatus = value == null ? ResultStatus.DataNotFound : ResultStatus.Successful;
                total = value == null ? total : 1;
            }

            Exception = null;
            Total = total;
            Title = ResultStatus != ResultStatus.Successful ? "عملیات ناموفق" : "عملیات موفق";
            Message = ResultStatus != ResultStatus.Successful ? "در حاضر اطلاعاتی موجود نمی باشد" : (string.IsNullOrEmpty(message) ? "اطلاعات شما با موفقیت انجام شد" : message);
            ResponseCode = 200;
        }
        public void SetWarning(string message)
        {
            ResultStatus = ResultStatus.UnSuccessful;
            Exception = null;
            Message = message;
            ResponseCode = 400;
        }

        public void SetException(SqlException ex)
        {
            ResultStatus = ResultStatus.Exception;
            Exception = ex;
            Message = ex != null ? ex.Message : string.Empty;
            switch (ex.Number)
            {
                case 2601:
                    Message = "اطلاعات وارد شده تکراری می باشد";
                    break;
            }
        }
        public void SetException(Exception ex)
        {
            ResultStatus = ResultStatus.Exception;
            Exception = ex;
            Message = ex != null ? ex.Message : string.Empty;
        }

        public void SetStatus(ResultStatus st)
        {
            ResultStatus = st;
            Exception = null;
            Message = "";
        }

        public void SetException(string message)
        {
            ResultStatus = ResultStatus.Exception;
            Message = message;
        }
        public void SetResult(ResultStatus resultStatus, string title = "", string message = "", int responseCode = 0, Exception exception = null)
        {
            ResultStatus = resultStatus;
            Message = message;
            Title = title;
            Exception = exception;
            ResponseCode = resultStatus != ResultStatus.Successful ? 400 : 200;
        }
        public void SetResult(ResultStatus resultStatus = ResultStatus.Exception, string title = "", List<string> errors = null, int responseCode = 400)
        {
            ResultStatus = resultStatus;
            Title = title;
            Errors = errors;
            ResponseCode = responseCode;
        }

        public void SetMessage(string message)
        {
            Message = message;
        }

        public void SetMessage(string message, string title, ResultStatus resultStatus)
        {
            Message = message;
            Title = title;
            ResultStatus = resultStatus;
        }

        public void SetUrlDashboardPanel(string urlAddress)
        {
            UrlAddress = urlAddress;
        }

        public void ClearException()
        {
            Exception = null;
        }
    }
}