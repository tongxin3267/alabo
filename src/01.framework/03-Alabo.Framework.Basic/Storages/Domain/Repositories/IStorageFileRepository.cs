using MongoDB.Bson;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Common.Domain.Repositories {

    public interface IStorageFileRepository : IRepository<StorageFile, ObjectId> {
    }
}