using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Grades.Domain.Repositories {

    public interface IUpgradeRecordRepository : IRepository<UpgradeRecord, ObjectId> {
    }
}