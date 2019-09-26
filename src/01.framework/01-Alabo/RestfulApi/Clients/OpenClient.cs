using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.RestfulApi.Clients
{
    public class OpenClient : ClientBase, IOpenClient
    {
        public T Get<T>(string apiUrl, IDictionary<string, string> para = null)
        {
            var uri = BuildQueryUri(apiUrl);

            try
            {
                var result = Connector.Get(uri, para);
                var apiResult = DataFormatter.ToObject<ApiResult<T>>(result);
                if (apiResult.Status == ResultStatus.Success)
                    return apiResult.Result;
                throw new SystemException($"{apiUrl}请求失败,{apiResult.Message}");
            }
            catch (Exception ex)
            {
                throw new SystemException($"{apiUrl}网络请求失败,{ex.Message}");
            }
        }

        public async Task<T> GetAsync<T>(string apiUrl, IDictionary<string, string> para = null)
        {
            var uri = BuildQueryUri(apiUrl);
            try
            {
                var result = await Connector.GetAsync(uri, para);
                var apiResult = DataFormatter.ToObject<ApiResult<T>>(result);
                if (apiResult.Status == ResultStatus.Success)
                    return apiResult.Result;
                throw new SystemException($"{apiUrl}请求失败,{apiResult.Message}");
            }
            catch (Exception ex)
            {
                throw new SystemException($"{apiUrl}网络请求失败,{ex.Message}");
            }
        }

        public T Post<T>(string apiUrl, object para = null)
        {
            var uri = BuildQueryUri(apiUrl);
            try
            {
                var data = DataFormatter.FromObject(para);
                var result = Connector.Post(uri, data);
                return DataFormatter.ToObject<T>(result);
            }
            catch (Exception ex)
            {
                throw new SystemException($"{apiUrl}网络请求失败,{ex.Message}");
            }
        }

        public async Task<T> PostAsync<T>(string apiUrl, object para = null)
        {
            var uri = BuildQueryUri(apiUrl);
            try
            {
                var data = DataFormatter.FromObject(para);
                var result = await Connector.PostAsync(uri, data);
                return DataFormatter.ToObject<T>(result);
            }
            catch (Exception ex)
            {
                throw new SystemException($"{apiUrl}网络请求失败,{ex.Message}");
            }
        }
    }
}