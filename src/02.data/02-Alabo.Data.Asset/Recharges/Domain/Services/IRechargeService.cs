using System;
using System.Collections.Generic;
using Alabo.App.Asset.Pays.Domain.Entities;
using Alabo.App.Asset.Recharges.Domain.Entities;
using Alabo.App.Asset.Recharges.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Microsoft.AspNetCore.Http;

namespace Alabo.App.Asset.Recharges.Domain.Services
{
    /// <summary>
    ///     充值
    /// </summary>
    public interface IRechargeService : IService<Recharge, long>
    {
        /// <summary>
        ///     线上充值
        /// </summary>
        /// <param name="view"></param>
        Tuple<ServiceResult, Pay> AddOnline(RechargeOnlineAddInput view);

        /// <summary>
        ///     线下充值
        /// </summary>
        /// <param name="view"></param>
        ServiceResult AddOffOnline(RechargeAddInput view);

        /// <summary>
        ///     用户删除充值，后取消充值，审核成功的充值记录不能删除
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="userId">用户Id</param>
        ServiceResult Delete(long userId, long id);

        /// <summary>
        ///     后台分页
        /// </summary>
        /// <param name="query"></param>
        PagedList<RechargeOutput> GetPageList(object query);

        /// <summary>
        ///     获取所有支持充值的货币类型
        /// </summary>
        IList<KeyValue> GetRechargeMoneys();

        /// <summary>
        ///     审核
        /// </summary>
        /// <param name="httpContext"></param>
        ServiceResult Check(DefaultHttpContext httpContext);

        RechargeDetail GetSingle(long id);
    }
}