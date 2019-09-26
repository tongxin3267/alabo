using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.Grades.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Grades.Domain.Repositories
{
    public class UpgradeRecordRepository : RepositoryMongo<UpgradeRecord, ObjectId>, IUpgradeRecordRepository
    {
        public UpgradeRecordRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}