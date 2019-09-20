using System.Threading.Tasks;
using Alabo.App.Core.Admin.Domain.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Validations.Aspects;

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
        /// 检查数据完整性
        /// </summary>
        /// <returns></returns>
        Task CheckData();

        /// <summary>
        ///     清空数据库脚本
        /// </summary>
        ServiceResult TruncateTable([Valid]TruncateInput truncateInput);

        /// <summary>
        /// 获取视图
        /// </summary>
        /// <returns></returns>
        TruncateInput TruncateView(object id);

        /// <summary>
        ///     Clears the cache.
        /// </summary>
        void ClearCache();
    }
}