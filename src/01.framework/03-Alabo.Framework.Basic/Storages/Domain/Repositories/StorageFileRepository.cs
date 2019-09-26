using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.Storages.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Storages.Domain.Repositories {

    public class StorageFileRepository : RepositoryMongo<StorageFile, ObjectId>, IStorageFileRepository {

        public StorageFileRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}