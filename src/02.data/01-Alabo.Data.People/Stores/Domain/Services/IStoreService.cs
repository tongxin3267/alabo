using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Stores.Domain.Services {

    public interface IStoreService : IService<Entities.Store, ObjectId> {
    }
}