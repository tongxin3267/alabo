using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.Address.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Address.Domain.Repositories
{
    public interface IRegionRepository : IRepository<Region, ObjectId>
    {
    }
}