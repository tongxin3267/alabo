using System.Collections.Generic;
using Alabo.App.Core.Finance.Domain.Dtos.Recharge;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.ViewModels.Recharge;
using Alabo.App.Core.Themes.DiyModels.Lists;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Finance.Domain.Services {

    public interface ITradeService : IService<Trade, long> {

        /// <summary>
        ///     获取单个的trade
        /// </summary>
        /// <param name="id">主键ID</param>
        Trade GetSingle(long id);

        #region 充值

        /// <summary>
        ///     线上充值
        /// </summary>
        /// <param name="view"></param>

        //Tuple<ServiceResult, Pay> AddOnline(RechargeAddInput view);

        ///// <summary>
        /////     线下充值
        ///// </summary>
        ///// <param name="view"></param>
        //
        //ServiceResult AddOffOnline(RechargeAddInput view);

        ///// <summary>
        /////     线下充值审核
        ///// </summary>
        ///// <param name="httpContext"></param>
        //
        //ServiceResult Check(DefaultHttpContext httpContext);

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
        ///     获取用户充值列表
        /// </summary>
        /// <param name="parameter"></param>
        ListOutput GetUserList(RechargeApiInput parameter);

        /// <summary>
        ///     获取所有支持充值的货币类型
        /// </summary>
        Dictionary<string, object> GetRechargeMoneys();

        /// <summary>
        ///     充值详情
        /// </summary>
        /// <param name="id">主键ID</param>
        ViewAdminRecharge GetAdminRecharge(long id);

        #endregion 充值
    }
}