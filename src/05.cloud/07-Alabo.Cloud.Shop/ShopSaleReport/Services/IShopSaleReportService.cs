using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Shop.Order.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.Cloud.Shop.ShopSaleReport.Services {

    /// <summary>
    /// 线上商城业绩统计
    /// </summary>
    public interface IShopSaleReportService : IService {

        /// <summary>
        ///     更新订单业绩
        ///     更新到表User_UserMap.ShopSale中
        /// </summary>
        /// <param name="orderId"></param>
        void UpdateUserSaleInfo(long orderId);

        /// <summary>
        ///     会员销售统计表
        /// </summary>
        /// <param name="query"></param>
        PagedList<ViewShopSale> GetShopSalePagedList(object query);

        /// <summary>
        ///     会员销售统计表
        /// </summary>
        /// <param name="query"></param>
        PagedList<ViewPriceStyleSale> GetViewPriceStyleSalePagedList(object query);
    }
}