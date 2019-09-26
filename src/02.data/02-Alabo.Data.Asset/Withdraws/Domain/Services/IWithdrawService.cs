using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Asset.Withdraws.Domain.Entities;
using Alabo.App.Asset.Withdraws.Dtos;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Http;

namespace Alabo.App.Asset.Withdraws.Domain.Services {

    public interface IWithdrawService : IService<Withdraw, long> {

        /// <summary>
        ///     提现
        ///     只支持人民币提现
        ///     其他币种提现请先转换成人民币
        /// </summary>
        /// <param name="withDrawInput">申请提现</param>
        ServiceResult Add(WithDrawInput withDrawInput);

        /// <summary>
        ///     用户删除提现，后取消提现，审核成功的提现记录不能删除
        /// </summary>
        /// <param name="userId">会员Id</param>
        /// <param name="id">Id标识</param>
        ServiceResult Delete(long userId, long id);

        /// <summary>
        ///     获取提现详情
        /// </summary>
        /// <param name="userId">会员Id</param>
        /// <param name="id">Id标识</param>
        WithDrawShowOutput GetSingle(long userId, long id);

        /// <summary>
        ///     后台分页
        /// </summary>
        /// <param name="query">查询</param>
        PagedList<WithDrawOutput> GetPageList(object query);

        /// <summary>
        ///     后台分页
        /// </summary>
        /// <param name="query">查询</param>
        PagedList<ViewAdminWithDraw> GetAdminPageList(object query);

        /// <summary>
        ///     获取所有支持提现的货币类型
        /// </summary>
        /// <param name="userId">会员Id</param>
        IList<KeyValue> GetWithDrawMoneys(long userId);

        /// <summary>
        ///     提现审核
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        ServiceResult Check(DefaultHttpContext httpContext);

        ///// <summary>
        ///// 提现终审
        ///// </summary>
        ///// <param name="httpContext"></param>
        //
        //ServiceResult FinalCheck(DefaultHttpContext httpContext);

        /// <summary>
        ///     提现详情
        /// </summary>
        /// <param name="id">Id标识</param>
        ViewAdminWithDraw GetAdminWithDraw(long id);

        /// <summary>
        /// 提现审核
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ServiceResult WithDrawCheck(ViewWithDrawCheck view);
    }
}