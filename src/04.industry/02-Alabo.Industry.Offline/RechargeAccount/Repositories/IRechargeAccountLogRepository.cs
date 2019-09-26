using Alabo.Domains.Repositories;
using Alabo.Industry.Offline.RechargeAccount.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Offline.RechargeAccount.Repositories
{
    public interface IRechargeAccountLogRepository : IRepository<RechargeAccountLog, ObjectId>
    {
    }
}
