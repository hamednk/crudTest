namespace Persistence.Services
{
    using Mc2.CrudTest.Application.Interfaces;
    using Mc2.CrudTest.Application.Interfaces.Services;
    using Mc2.CrudTest.Application.Interfaces.Services.Base;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class GenericService : IGenericService
    {
        private readonly IGenericRepository _GenericRepository;
        public GenericService(IServiceProvider service)
        {
            _GenericRepository = (IGenericRepository)service.GetService(typeof(IGenericRepository));
        }

        private static void ErrorHandler<T>(ServiceResponse<T> result, dynamic ex)
        {
            result.SetException(ex);
        }

        public async Task<ServiceResponse<T>> Add<T>(string spName, object data)
        {
            var result = new ServiceResponse<T>();
            try
            {
                var resData = await _GenericRepository.Add<T>(spName, data).ConfigureAwait(false);
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
            var result = new ServiceResponse<List<T>>();
            try
            {
                var items = await _GenericRepository.List<T>(spName, data).ConfigureAwait(false);
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
            var result = new ServiceResponse<List<T>>();
            try
            {
                var items = await _GenericRepository.List<T>(nameOrQuery, data, commandType).ConfigureAwait(false);
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
            var result = new ServiceResponse<List<T>>();
            try
            {
                var resList = await _GenericRepository.ListWithQuery<T>(query).ConfigureAwait(false);
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
            var result = new ServiceResponse<List<T>>();
            try
            {
                var resList = await _GenericRepository.ListWithQueryAndParams<T>(query, data).ConfigureAwait(false);
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
            var result = new ServiceResponse<List<T>>();
            try
            {
                var resList = await _GenericRepository.ListWithTable<T>(tableName).ConfigureAwait(false);
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
            var result = new ServiceResponse<List<T>>();
            try
            {
                var resList = await _GenericRepository.ListWithTableAndParams<T>(tableName, data).ConfigureAwait(false);
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
            var result = new ServiceResponse<T>();

            try
            {
                result.SetData(await _GenericRepository.Get<T>(spName, data).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                result.SetException(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<T>> Edit<T>(string spName, object data)
        {
            var result = new ServiceResponse<T>();
            try
            {
                var id = await _GenericRepository.Edit<T>(spName, data).ConfigureAwait(false);
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

        public async Task<ServiceResponse<bool>> Delete(string spName, string data)
        {
            var result = new ServiceResponse<bool>();
            try
            {
                    var res  = await _GenericRepository.Delete(spName, data).ConfigureAwait(false);
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
