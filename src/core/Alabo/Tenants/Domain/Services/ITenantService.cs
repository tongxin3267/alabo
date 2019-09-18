using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Tenants.Domain.Entities;

namespace Alabo.Tenants.Domain.Services {

    /// <summary>
    ///     ITenantService
    /// </summary>
    public interface ITenantService : IService<Tenant, ObjectId> {

        /// <summary>
        /// ��ȡ��ǰ�⻧��վ��
        /// </summary>
        /// <returns></returns>
        TenantSite Site();

        /// <summary>
        /// ��ʼ���⻧վ��
        /// </summary>
        void InitSite();
    }
}