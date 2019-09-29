using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Industry.Offline.RechargeAccount.Domain.Dtos;
using Alabo.Industry.Offline.RechargeAccount.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Offline.RechargeAccount.Services
{
    public interface IRechargeAccountLogService : IService<RechargeAccountLog, ObjectId>
    {
        /// <summary>
        ///     ≥‰÷µ
        /// </summary>
        /// <param name="rechargeAccount"></param>
        /// <returns></returns>
        ServiceResult Add(RechargeAccountInput rechargeAccount);
    }
}