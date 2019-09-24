using MongoDB.Bson;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Market.DataBackup.Domain.Repositories {

    public class DataBackupRepository : RepositoryMongo<Entities.DataBackup, ObjectId>, IDataBackupRepository {

        public DataBackupRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}