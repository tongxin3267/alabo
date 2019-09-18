using System.Collections.Specialized;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Admin.Domain.Services {

    /// <summary>
    ///     APP服务接口
    /// </summary>
    public interface IAppService : IService {

        /// <summary>
        ///     APP名称集合
        /// </summary>
        NameValueCollection AppNameCollection();

        /// <summary>
        /// 验证支付密码
        /// </summary>
        /// <returns></returns>
        ServiceResult VerifySafePassword();
    }
}