using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Mc2.CrudTest.Application.Interfaces;
using Mc2.CrudTest.Application.Interfaces.Services;
using Mc2.CrudTest.Application.Interfaces.Services.Base;

namespace Mc2.CrudTest.Persistence.Services
{
    public class GenericService : IGenericService
    {
        private readonly IGenericRepository _genericRepository;
        public GenericService(IServiceProvider service)
        {
            _genericRepository = (IGenericRepository)service.GetService(typeof(IGenericRepository));
        }

        private static void ErrorHandler<T>(ServiceResponse<T> result, dynamic ex)
        {
            result.SetException(ex);
        }

        public async Task<ServiceResponse<T>> Add<T>(string spName, object data)
        {
            ServiceResponse<T> result = new ServiceResponse<T>();
            try
            {
                T resData = await _genericRepository.Add<T>(spName, data).ConfigureAwait(false);
                result.SetData(resData);
            }
            catch (SqlException ex)
            {
                ErrorHandler(result, ex);
            }
            catch (Exception ex)
            {
                ErrorHandler(result, ex);
            }
            return result;
        }

        public async Task<ServiceResponse<List<T>>> List<T>(string spName, object data)
        {
            ServiceResponse<List<T>> result = new ServiceResponse<List<T>>();
            try
            {
                KeyValuePair<int, List<T>> items = await _genericRepository.List<T>(spName, data).ConfigureAwait(false);
                result.SetData(items.Value, items.Key);
            }
            catch (SqlException ex)
            {
                ErrorHandler(result, ex);
            }
            catch (Exception ex)
            {
                ErrorHandler(result, ex);
            }
            return result;
        }

        public async Task<ServiceResponse<List<T>>> List<T>(string nameOrQuery, object data, CommandType commandType)
        {
            ServiceResponse<List<T>> result = new ServiceResponse<List<T>>();
            try
            {
                List<T> items = await _genericRepository.List<T>(nameOrQuery, data, commandType).ConfigureAwait(false);
                result.SetData(items, items.Count);
            }
            catch (SqlException ex)
            {
                ErrorHandler(result, ex);
            }
            catch (Exception ex)
            {
                ErrorHandler(result, ex);
            }
            return result;
        }

        public async Task<ServiceResponse<List<T>>> ActionWithQuery<T>(string query)
        {
            ServiceResponse<List<T>> result = new ServiceResponse<List<T>>();
            try
            {
                List<T> resList = await _genericRepository.ListWithQuery<T>(query).ConfigureAwait(false);
                result.SetData(resList, resList.Count);
            }
            catch (SqlException ex)
            {
                ErrorHandler(result, ex);
            }
            catch (Exception ex)
            {
                ErrorHandler(result, ex);
            }

            return result;
        }

        public async Task<ServiceResponse<List<T>>> ActionWithQueryAndParams<T>(string query, object data)
        {
            ServiceResponse<List<T>> result = new ServiceResponse<List<T>>();
            try
            {
                List<T> resList = await _genericRepository.ListWithQueryAndParams<T>(query, data).ConfigureAwait(false);
                result.SetData(resList, resList.Count);
            }
            catch (SqlException ex)
            {
                ErrorHandler(result, ex);
            }
            catch (Exception ex)
            {
                ErrorHandler(result, ex);
            }

            return result;
        }

        public async Task<ServiceResponse<List<T>>> ActionWithTable<T>(string tableName)
        {
            ServiceResponse<List<T>> result = new ServiceResponse<List<T>>();
            try
            {
                List<T> resList = await _genericRepository.ListWithTable<T>(tableName).ConfigureAwait(false);
                result.SetData(resList, resList.Count);
            }
            catch (SqlException ex)
            {
                ErrorHandler(result, ex);
            }
            catch (Exception ex)
            {
                ErrorHandler(result, ex);
            }

            return result;
        }

        public async Task<ServiceResponse<List<T>>> ActionWithTableAndParams<T>(string tableName, object data)
        {
            ServiceResponse<List<T>> result = new ServiceResponse<List<T>>();
            try
            {
                List<T> resList = await _genericRepository.ListWithTableAndParams<T>(tableName, data).ConfigureAwait(false);
                result.SetData(resList, resList.Count);
            }
            catch (SqlException ex)
            {
                ErrorHandler(result, ex);
            }
            catch (Exception ex)
            {
                ErrorHandler(result, ex);
            }

            return result;
        }

        public async Task<ServiceResponse<T>> Get<T>(string spName, object data)
        {
            ServiceResponse<T> result = new ServiceResponse<T>();

            try
            {
                result.SetData(await _genericRepository.Get<T>(spName, data).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                result.SetException(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<T>> Edit<T>(string spName, object data)
        {
            ServiceResponse<T> result = new ServiceResponse<T>();
            try
            {
                T id = await _genericRepository.Edit<T>(spName, data).ConfigureAwait(false);
                if (id != null)
                    result.SetData(id);
                else
                    result.SetException("سطر تکراری است"); //ErrorResource.DuplicateRow);

                return result;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ServiceResponse<bool>> Delete(string spName, object data)
        {
            ServiceResponse<bool> result = new ServiceResponse<bool>();
            try
            {
                    bool res  = await _genericRepository.Delete(spName, data).ConfigureAwait(false);
                    result.SetData(res);
            }
            catch (Exception ex)
            {
                result.SetException(ex);
            }

            return result;
        }
    }
}
