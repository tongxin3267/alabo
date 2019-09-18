using System;
using System.Collections.Generic;
using MongoDB.Bson;
using Alabo.App.Shop.Coupons.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.App.Shop.Product.Domain.Dtos;
using Alabo.Domains.Entities;

namespace Alabo.App.Shop.Store.Domain.Dtos {

    /// <summary>
    ///     店铺对象
    /// </summary>
    public class StoreItem {

        /// <summary>
        ///     店铺Id
        /// </summary>
        public long StoreId { get; set; }

        /// <summary>
        ///     店铺名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        ///     运费
        /// </summary>

        public decimal ExpressAmount { get; set; }

        /// <summary>
        ///     实际店铺运费
        /// </summary>
        public decimal CalculateExpressAmount { get; set; } = 0.0m;

        /// <summary>
        ///     店铺商品数量统计
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        ///     店铺商品金额统计
        ///     包括商品金额和快递费金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     店铺商品Sku信息
        /// </summary>
        public IList<ProductSkuItem> ProductSkuItems { get; set; } = new List<ProductSkuItem>();

        /// <summary>
        ///     Gets or sets the express templates.
        ///     快递模板
        /// </summary>
        /// <value>
        ///     The express templates.
        /// </value>
        public List<KeyValue> ExpressTemplates { get; set; } = new List<KeyValue>();

        /// <summary>
        /// 快递方式
        /// </summary>
        public ExpressType ExpressType { get; set; } = ExpressType.Express;

        /// <summary>
        /// 用户可以用的优惠券
        /// </summary>
        public List<UserCoupon> Coupons { get; set; }
    }
}