﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Alabo.App.Core.Finance.Domain.Dtos.WithDraw;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.ViewModels.WithDraw;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Finance.Domain.Services {

    /// <summary>
    ///     提现服务
    /// </summary>
    public interface IWithDrawService : IService {

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
        ///     获取用户提现列表
        /// </summary>
        /// <param name="parameter">参数</param>
        PagedList<Trade> GetUserList(WithDrawApiInput parameter);

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