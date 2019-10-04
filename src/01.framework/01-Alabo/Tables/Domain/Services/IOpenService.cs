using Alabo.Domains.Services;
using System.Threading.Tasks;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Tables.Domain.Services {

    /// <summary>
    ///     授权服务
    /// </summary>
    public interface IOpenService : IService {

        /// <summary>
        ///     平台预留手机号码
        /// </summary>
        string OpenMobile { get; }

        /// <summary>
        ///     发送单条 使用异步方法发送短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendRawAsync(string mobile, string message);

        /// <summary>
        ///     同步方式发送单条
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="message"></param>
        ApiResult SendRaw(string mobile, string message);

        /// <summary>
        ///     生成验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        long GenerateVerifiyCode(string mobile);

        /// <summary>
        ///     校验验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        bool CheckVerifiyCode(string mobile, long code);

        /// <summary>
        ///     验证手机格式
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        bool CheckMobile(string mobile);
    }
}