using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Stores.Domain.Repositories {

    public interface IStoreRepository : IRepository<Entities.Store, ObjectId> {
    }
}