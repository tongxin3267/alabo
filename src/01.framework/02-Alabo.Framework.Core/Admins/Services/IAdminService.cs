using Alabo.Domains.Services;
using System.Threading.Tasks;

namespace Alabo.App.Core.Admin.Domain.Services {

    /// <summary>
    /// </summary>
    public interface IAdminService : IService {

        /// <summary>
        /// 数据初始化
        /// </summary>
        /// <param name="isTenant">是否为租户初始标识，isTenant=true的时候表示为租户执行</param>
        void DefaultInit(bool isTenant = false);

        /// <summary>
        ///     Clears the cache.
        /// </summary>
        void ClearCache();
    }
}