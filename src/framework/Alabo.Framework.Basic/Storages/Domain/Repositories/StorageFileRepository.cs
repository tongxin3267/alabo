using MongoDB.Bson;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Common.Domain.Repositories {

    public class StorageFileRepository : RepositoryMongo<StorageFile, ObjectId>, IStorageFileRepository {

        public StorageFileRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}