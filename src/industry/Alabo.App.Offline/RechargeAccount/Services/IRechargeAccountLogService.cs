using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Offline.RechargeAccount.Entities;
using Alabo.Domains.Entities;
using Alabo.App.Offline.RechargeAccount.Domain.Dtos;

namespace Alabo.App.Offline.RechargeAccount.Services
{
    public interface IRechargeAccountLogService : IService<RechargeAccountLog, ObjectId>
    {

        /// <summary>
        /// ≥‰÷µ
        /// </summary>
        /// <param name="rechargeAccount"></param>
        /// <returns></returns>
        ServiceResult Add(RechargeAccountInput rechargeAccount);
    }
}
