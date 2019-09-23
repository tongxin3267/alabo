using MongoDB.Bson;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Tasks.Domain.Repositories {

    public class UpgradeRecordRepository : RepositoryMongo<UpgradeRecord, ObjectId>, IUpgradeRecordRepository {

        public UpgradeRecordRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}