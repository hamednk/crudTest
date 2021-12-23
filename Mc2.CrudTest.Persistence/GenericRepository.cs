using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Mc2.CrudTest.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using static Dapper.SqlMapper;

namespace Mc2.CrudTest.Persistence
{
    public class GenericRepository : IGenericRepository
    {
        private readonly IDbConnection _dbRead;
        private readonly IDbConnection _dbWrite;

        public GenericRepository(IConfiguration configuration)
        {
            _dbRead = new SqlConnection(configuration.GetConnectionString("ReadConnection"));
            _dbWrite = new SqlConnection(configuration.GetConnectionString("WriteConnection"));
        }

        public async Task<KeyValuePair<int, List<T>>> List<T>(string spName, object data)
        {
            GridReader result = await _dbRead.QueryMultipleAsync(spName, data, commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);
            return new KeyValuePair<int, List<T>>(result.ReadFirstAsync<int>().Result,
                result.ReadAsync<T>().Result.ToList());
        }

        public async Task<List<T>> List<T>(string nameOrQuery, object data, CommandType commandType)
        {
            GridReader result = await _dbRead.QueryMultipleAsync(nameOrQuery, data, commandType: commandType)
                .ConfigureAwait(false);
            return result.ReadAsync<T>().Result.ToList();
        }

        public async Task<List<T>> ListWithQuery<T>(string query)
        {
            return await ListWithQueryAndParams<T>(query, null).ConfigureAwait(false);
        }

        public async Task<List<T>> ListWithQueryAndParams<T>(string query, object data)
        {
            GridReader result = await _dbRead.QueryMultipleAsync(query, data, commandType: CommandType.Text)
                .ConfigureAwait(false);
            return result.ReadAsync<T>().Result.ToList();
        }

        public async Task<List<T>> ListWithTable<T>(string tableName)
        {
            return await ListWithTableAndParams<T>(tableName, null).ConfigureAwait(false);
        }

        public async Task<List<T>> ListWithTableAndParams<T>(string tableName, object data)
        {
            GridReader result = await _dbRead.QueryMultipleAsync(tableName, data, commandType: CommandType.TableDirect)
                    .ConfigureAwait(false);
                return result.ReadAsync<T>().Result.ToList();
        }

        public async Task<T> Add<T>(string spName, object data)
        {
            IEnumerable<T> result = await _dbWrite.QueryAsync<T>(spName, data, commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);
            return result.FirstOrDefault();
        }

        public async Task<T> Get<T>(string spName, object data)
        {
            IEnumerable<T> result = await _dbRead.QueryAsync<T>(spName, data, commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);
            return result.SingleOrDefault();
        }

        public async Task<T> Edit<T>(string spName, object data)
        {
            IEnumerable<T> result = await _dbWrite.QueryAsync<T>(spName, data, commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);
            return result.SingleOrDefault();
        }

        public async Task<bool> Delete(string spName, object data)
        {
            IEnumerable<bool> result = await _dbWrite.QueryAsync<bool>(spName, data, commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);
            return result.SingleOrDefault();
        }
    }
}