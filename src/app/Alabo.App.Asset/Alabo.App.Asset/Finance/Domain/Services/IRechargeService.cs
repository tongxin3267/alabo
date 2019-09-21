using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Alabo.App.Core.Finance.Domain.Dtos.Recharge;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.ViewModels.Recharge;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Finance.Domain.Services {

    /// <summary>
    ///     充值
    /// </summary>
    public interface IRechargeService : IService {

        /// <summary>
        /// 储值卡充值
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ServiceResult StoredValueRecharge(StoredValueInput view);

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
        ///     充值详情
        /// </summary>
        /// <param name="id">主键ID</param>
        ViewAdminRecharge GetAdminRecharge(long id);

        /// <summary>
        ///     审核
        /// </summary>
        /// <param name="httpContext"></param>
        ServiceResult Check(DefaultHttpContext httpContext);

        /// <summary>
        ///     获取用户充值列表
        /// </summary>
        /// <param name="parameter"></param>
        PagedList<Trade> GetUserList(RechargeAddInput parameter);

        RechargeDetail GetSingle(long id);
    }
}