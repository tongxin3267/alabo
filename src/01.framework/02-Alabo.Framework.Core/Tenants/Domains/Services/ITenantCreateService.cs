using Alabo.Framework.Core.Tenants.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.Framework.Core.Tenants.Domains.Services {

    /// <summary>
    /// ITenantCreateService
    /// </summary>
    public interface ITenantCreateService : IService {

        /// <summary>
        /// 删除租户
        /// </summary>
        /// <param name="tenantInit"></param>
        /// <returns></returns>
        ServiceResult DeleteTenant(TenantInit tenantInit);

        /// <summary>
        /// 初始化租户默认数据
        /// 包括创建管理员，权限等
        /// </summary>
        /// <param name="tenantInit"></param>
        /// <returns></returns>
        ServiceResult InitTenantDefaultData(TenantInit tenantInit);

        /// <summary>
        /// 租户的验证的Token
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="siteId"></param>
        /// <returns></returns>
        string Token(string tenant, string siteId);

        /// <summary>
        /// 初始化租户模板数据
        /// </summary>
        /// <param name="tenantInit"></param>
        /// <returns></returns>
        ServiceResult InitTenantTheme(TenantInit tenantInit);

        /// <summary>
        /// init tenant database
        /// </summary>
        /// <returns></returns>
        ServiceResult InitTenantDatabase(string tenant);

        /// <summary>
        /// 检查租户是否存在
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        ServiceResult HaveTenant(string tenant);

        /// <summary>
        /// 检查租户不存在
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        ServiceResult NoTenant(string tenant);
    }
}