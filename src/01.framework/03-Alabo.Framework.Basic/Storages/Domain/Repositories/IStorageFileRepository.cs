using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.Storages.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Storages.Domain.Repositories {

    public interface IStorageFileRepository : IRepository<StorageFile, ObjectId> {
    }
}