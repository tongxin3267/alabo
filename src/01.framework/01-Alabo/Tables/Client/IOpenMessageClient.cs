using System.Collections.Generic;
using System.Threading.Tasks;
using ZKCloud.Open.ApiBase.Models;
using ZKCloud.Open.ApiBase.Services;
using ZKCloud.Open.Message.Models;

namespace Alabo.Tables.Client
{
    public interface IOpenMessageClient : IRestClient
    {
        /// <summary>
        ///     同步方法发送一条短信
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mobile">手机号码</param>
        /// <param name="message">信息</param>
        /// <param name="userIpAddress">发送用户的IP地址</param>
        /// <returns></returns>
        ApiResult SendRaw(string token, string mobile, string message, string userIpAddress);

        /// <summary>
        ///     异步方法发送一条短信
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mobile"></</param>
        /// <param name="message">信息</param>
        /// <param name="userIpAddress">发送用户的IP地址</param>
        /// <returns></returns>
        Task<ApiResult> SendRawAsync(string token, string mobile, string message, string userIpAddress);

        /// <summary>
        ///     异步方法发送短信验证码
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mobile"></</param>
        /// <param name="userIpAddress">发送用户的IP地址</param>
        /// <returns></returns>
        ApiResult SendMobileVerifiyCode(string token, string mobile, string userIpAddress);

        /// <summary>
        ///     验证手机验证码
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mobile"></</param>
        /// <param name="userIpAddress">发送用户的IP地址</param>
        /// <returns></returns>
        ApiResult CheckMobileVerifiyCode(string token, string mobile, string userIpAddress, long code);

        /// <summary>
        ///     根据模板发送一条短信
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mobile"></param>
        /// <param name="templateId"></param>
        /// <param name="userIpAddress"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        ApiResult SendTemplate(string token, string mobile, long templateId, string userIpAddress,
            IDictionary<string, string> parameters = null);

        /// <summary>
        ///     根据模板发送一条短信
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mobile"></param>
        /// <param name="templateId"></param>
        /// <param name="userIpAddress"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<ApiResult> SendTemplateAsync(string token, string mobile, long templateId, string userIpAddress,
            IDictionary<string, string> parameters = null);

        /// <summary>
        ///     获取模板
        /// </summary>
        /// <param name="token"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<ApiResult<MessageTemplate>> GetTemplate(string token, long code);
    }
}