using Alabo.Domains.Repositories;
using Alabo.Tenants.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Tenants.Domain.Repositories
{
    /// <summary>
    ///     ITenantRepository
    /// </summary>
    public interface ITenantRepository : IRepository<Tenant, ObjectId>
    {
    }
}