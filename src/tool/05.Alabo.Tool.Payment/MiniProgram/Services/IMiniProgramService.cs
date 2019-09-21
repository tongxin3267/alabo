using Alabo.App.Core.ApiStore.MiniProgram.Dtos;
using Alabo.Domains.Services;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.ApiStore.MiniProgram.Services {

    /// <summary>
    /// </summary>
    /// <seealso cref="Alabo.Domains.Services.IService" />
    public interface IMiniProgramService : IService {

        /// <summary>
        ///     Logins the specified login input.
        /// </summary>
        /// <param name="loginInput">The login input.</param>
        ApiResult<LoginOutput> Login(LoginInput loginInput);

        ApiResult<LoginOutput> PubLogin(LoginInput loginInput);
    }
}