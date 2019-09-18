using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Offline.RechargeAccount.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.App.Offline.RechargeAccount.Repositories;

namespace Alabo.App.Offline.RechargeAccount.Repositories
{
    public class RechargeAccountLogRepository : RepositoryMongo<RechargeAccountLog, ObjectId>, IRechargeAccountLogRepository
    {
        public RechargeAccountLogRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
