namespace Mc2.CrudTest.Application.Interfaces.Services
{
    using Mc2.CrudTest.Application.Interfaces.Services.Base;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public interface IGenericService 
    {
        Task<ServiceResponse<List<T>>> List<T>(string spName , object data);
        Task<ServiceResponse<List<T>>> List<T>(string nameOrQuery, object data, CommandType commandType);
        Task<ServiceResponse<List<T>>> ActionWithQuery<T>(string query);
        Task<ServiceResponse<List<T>>> ActionWithQueryAndParams<T>(string query, object data);
        Task<ServiceResponse<List<T>>> ActionWithTable<T>(string tableName);
        Task<ServiceResponse<List<T>>> ActionWithTableAndParams<T>(string tableName, object data);
        Task<ServiceResponse<T>> Get<T>(string spName , object data);
        Task<ServiceResponse<T>> Add<T>(string spName , object data);
        Task<ServiceResponse<T>> Edit<T>(string spName , object data);
        Task<ServiceResponse<bool>> Delete(string spName, string data);
    }
}
