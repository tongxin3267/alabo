using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Offline.RechargeAccount.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Offline.RechargeAccount.Repositories
{
    public class RechargeAccountLogRepository : RepositoryMongo<RechargeAccountLog, ObjectId>,
        IRechargeAccountLogRepository
    {
        public RechargeAccountLogRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}