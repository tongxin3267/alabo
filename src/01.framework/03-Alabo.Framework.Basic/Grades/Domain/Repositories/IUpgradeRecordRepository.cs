using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.Grades.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Grades.Domain.Repositories
{
    public interface IUpgradeRecordRepository : IRepository<UpgradeRecord, ObjectId>
    {
    }
}