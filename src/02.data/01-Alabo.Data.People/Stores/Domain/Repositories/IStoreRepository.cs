using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.App.Shop.Store.Domain.Repositories {

    public interface IStoreRepository : IRepository<Entities.Store, ObjectId> {
    }
}