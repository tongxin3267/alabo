using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Storages.Domain.Repositories {

    public interface IStorageFileRepository : IRepository<StorageFile, ObjectId> {
    }
}