using MongoDB.Bson;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Tenants.Domain.Entities;

namespace Alabo.Tenants.Domain.Repositories
{
    /// <summary>
    ///     TenantRepository
    /// </summary>
    public class TenantRepository : RepositoryMongo<Tenant, ObjectId>, ITenantRepository
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public TenantRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}