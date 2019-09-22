using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Tenants.Domain.Entities;

namespace Alabo.Tenants.Domain.Services {

    /// <summary>
    ///     ITenantService
    /// </summary>
    public interface ITenantService : IService<Tenant, ObjectId> {

        /// <summary>
        /// 获取当前租户的站点
        /// </summary>
        /// <returns></returns>
        TenantSite Site();

        /// <summary>
        /// 初始化租户站点
        /// </summary>
        void InitSite();
    }
}