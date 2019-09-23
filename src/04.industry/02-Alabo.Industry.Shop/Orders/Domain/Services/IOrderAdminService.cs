﻿using System;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Order.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Order.Domain.Services {

    /// <summary>
    ///     订单后台操作相关的接口
    /// </summary>
    public interface IOrderAdminService : IService {

        /// <summary>
        ///     根据订单和用户，创建支付记录
        ///     一个订单创建一条支付记录
        /// </summary>
        /// <param name="singlePayInput"></param>
        Tuple<ServiceResult, Pay> AddSinglePay(SinglePayInput singlePayInput);

        /// <summary>
        ///     Gets the admin page list.
        ///     获取后台订单列表
        /// </summary>
        /// <param name="query">参数</param>
        PagedList<AdminOrderList> GetAdminPageList(object query);

        /// <summary>
        ///     获取订单详情
        /// </summary>
        /// <param name="id">主键ID</param>
        ViewAdminOrder GetViewAdminOrder(long id, long UserId);

        /// <summary>
        /// 产品库存更新
        /// </summary>
        void ProductStockUpdate();
    }
}