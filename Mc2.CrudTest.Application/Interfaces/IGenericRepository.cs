namespace Mc2.CrudTest.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;
    public interface IGenericRepository
    {
        Task<KeyValuePair<int, List<T>>> List<T>(string spName , object data);
        Task<List<T>> List<T>(string nameOrQuery, object data, CommandType commandType);
        Task<List<T>> ListWithQuery<T>(string query);
        Task<List<T>> ListWithQueryAndParams<T>(string query, object data);
        Task<List<T>> ListWithTable<T>(string tableName);
        Task<List<T>> ListWithTableAndParams<T>(string tableName, object data);
        Task<T> Get<T>(string spName, object data);
        Task<T> Add<T>(string spName , object data);
        Task<T> Edit<T>(string spName , object data);
        Task<bool> Delete(string spName, string data);
    }
}
