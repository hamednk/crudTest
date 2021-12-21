namespace Persistence
{
    using Mc2.CrudTest.Application.Interfaces;
    using Dapper;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;

    public class GenericRepository : IGenericRepository
    {
        private readonly IDbConnection Db_Read;
        private readonly IDbConnection Db_Write;
        public GenericRepository(IConfiguration configuration)
        {
            Db_Read = new SqlConnection(configuration.GetConnectionString("ReadConnection"));
            Db_Write = new SqlConnection(configuration.GetConnectionString("WriteConnection"));
        }
        public async Task<KeyValuePair<int, List<T>>> List<T>(string spName, object data)
        {
            try
            {
                var result = await Db_Read.QueryMultipleAsync(spName, data, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return new KeyValuePair<int, List<T>>(result.ReadFirstAsync<int>().Result, result.ReadAsync<T>().Result.ToList());
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<List<T>> List<T>(string nameOrQuery, object data, CommandType commandType)
        {
            try
            {
                var result = await Db_Read.QueryMultipleAsync(nameOrQuery, data, commandType: commandType).ConfigureAwait(false);
                return result.ReadAsync<T>().Result.ToList();
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<List<T>> ListWithQuery<T>(string query)
        {
            return await ListWithQueryAndParams<T>(query, null).ConfigureAwait(false);
        }

        public async Task<List<T>> ListWithQueryAndParams<T>(string query, object data)
        {
            try
            {
                var result = await Db_Read.QueryMultipleAsync(query, data, commandType: CommandType.Text).ConfigureAwait(false);
                return result.ReadAsync<T>().Result.ToList();
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<List<T>> ListWithTable<T>(string tableName)
        {
            return await ListWithTableAndParams<T>(tableName, null).ConfigureAwait(false);
        }

        public async Task<List<T>> ListWithTableAndParams<T>(string tableName, object data)
        {
            try
            {
                var result = await Db_Read.QueryMultipleAsync(tableName, data, commandType: CommandType.TableDirect).ConfigureAwait(false);
                return result.ReadAsync<T>().Result.ToList();
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<T> Add<T>(string spName, object data)
        {
            var result = await Db_Write.QueryAsync<T>(spName, data, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            return result.FirstOrDefault();
        }

        public async Task<T> Get<T>(string spName, object data)
        {
            var result = await Db_Read.QueryAsync<T>(spName, data, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            return result.SingleOrDefault();
        }

        public async Task<T> Edit<T>(string spName, object data)
        {
            var result = await Db_Write.QueryAsync<T>(spName, data, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            return result.SingleOrDefault();
        }

        public async Task<bool> Delete(string spName, string data)
        {
            var result = new List<bool>().AsEnumerable();
            result = await Db_Write.QueryAsync<bool>(spName, data, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

            return result.SingleOrDefault();
        }
    }
}
