using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Grades.Domain.Repositories {

    public class UpgradeRecordRepository : RepositoryMongo<UpgradeRecord, ObjectId>, IUpgradeRecordRepository {

        public UpgradeRecordRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}