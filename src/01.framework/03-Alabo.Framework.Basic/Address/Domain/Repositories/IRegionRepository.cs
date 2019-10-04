using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.Regions.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Address.Domain.Repositories {

    public interface IRegionRepository : IRepository<Region, ObjectId> {
    }
}