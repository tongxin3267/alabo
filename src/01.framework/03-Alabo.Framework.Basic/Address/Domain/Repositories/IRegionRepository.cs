using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Address.Domain.Repositories {

    public interface IRegionRepository : IRepository<Region, ObjectId> {
    }
}