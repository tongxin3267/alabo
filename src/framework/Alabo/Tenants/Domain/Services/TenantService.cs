using System.Collections.Generic;
using MongoDB.Bson;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Helpers;
using Alabo.RestfulApi.Clients;
using Alabo.Tenants.Domain.Entities;

namespace Alabo.Tenants.Domain.Services {

    /// <summary>
    ///     TenantService
    /// </summary>
    public class TenantService : ServiceBase<Tenant, ObjectId>, ITenantService {

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="repository"></param>
        public TenantService(IUnitOfWork unitOfWork, IRepository<Tenant, ObjectId> repository) : base(unitOfWork,
            repository) {
        }

        public void InitSite() {
            // 不存在时才初始化
            if (!Exists()) {
                var apiUrl = "Api/Site/GetTenantSite";
                var sign = HttpWeb.Tenant;
                var siteId = Runtime.RuntimeContext.Current.WebsiteConfig.OpenApiSetting.Id;
                IDictionary<string, string> parameters = new Dictionary<string, string>
                {
                    {"siteId", siteId},
                    {"sign", sign}
                };
                var tenantSite = Ioc.Resolve<IOpenClient>().Get<Tenant>(apiUrl, parameters);
                if (tenantSite != null) {
                    Add(tenantSite);
                }
            }
        }

        /// <summary>
        /// 获取租户的站点
        /// </summary>
        /// <returns></returns>
        public TenantSite Site() {
            return ObjectCache.GetOrSet(() => {
                var find = FirstOrDefault();
                if (find == null) {
                    InitSite();
                    find = FirstOrDefault();
                }
                return find?.Site;
            }, "TenantSite").Value;
            throw new System.NotImplementedException();
        }
    }
}