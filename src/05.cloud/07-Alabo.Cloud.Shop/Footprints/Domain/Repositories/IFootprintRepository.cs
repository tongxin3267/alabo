using Alabo.Cloud.Shop.Footprints.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.Footprints.Domain.Repositories {

    public interface IFootprintRepository : IRepository<Footprint, ObjectId> {
    }
}