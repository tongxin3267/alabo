using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Offline.RechargeAccount.Entities;

namespace Alabo.App.Offline.RechargeAccount.Repositories
{
    public interface IRechargeAccountLogRepository : IRepository<RechargeAccountLog, ObjectId>
    {
    }
}
