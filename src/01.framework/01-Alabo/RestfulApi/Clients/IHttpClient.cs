using System.Collections.Generic;
using System.Threading.Tasks;
using Alabo.Dependency;

namespace Alabo.RestfulApi.Clients
{
    public interface IHttpClient : IScopeDependency
    {
        /// <summary>
        ///     Get类型请求，同步方法
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        T Get<T>(string apiUrl, IDictionary<string, string> para = null);

        /// <summary>
        ///     Get类型请求，异步方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUrl"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string apiUrl, IDictionary<string, string> para = null);

        /// <summary>
        ///     Get类型请求，同步方法
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        T Post<T>(string apiUrl, IDictionary<string, string> para = null);

        /// <summary>
        ///     Get类型请求，异步方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUrl"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        Task<T> PostAsync<T>(string apiUrl, IDictionary<string, string> para = null);
    }
}