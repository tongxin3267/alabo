using System;
using System.Threading.Tasks;
using Alabo.App.Asset.Accounts.Dtos;
using Alabo.App.Asset.Recharges.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Asset.Accounts.Domain.Services
{
    /// <summary>
    ///     账号充值
    /// </summary>
    public interface IAccountPayService : IService
    {
        Task<Tuple<ServiceResult, RechageAccountOutput>> BuyAsync(RechargeAccountInput rechargeAccount);

        /// <summary>
        ///     支付成功回调函数
        /// </summary>
        /// <param name="tradeId"></param>
        void AfterPaySuccess(long tradeId);
    }
}