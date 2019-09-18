using Alabo.App.Core.ApiStore.MiniProgram.Dtos;
using ZKCloud.Open.ApiBase.Models;

/// <summary>
/// </summary>
namespace Alabo.App.Core.ApiStore.MiniProgram.Clients {

    /// <summary>
    /// </summary>
    public interface IMiniProgramClient : IApiStoreClient {

        /// <summary>
        ///     Logins the asynchronous. https://mp.weixin.qq.com/debug/wxadoc/dev/api/api-login.html#wxloginobject
        /// </summary>
        ApiResult<SessionOutput> Login(LoginInput miniProgramLoginInput);

        ApiResult<SessionOutput> PubLogin(LoginInput miniProgramLoginInput);
    }
}