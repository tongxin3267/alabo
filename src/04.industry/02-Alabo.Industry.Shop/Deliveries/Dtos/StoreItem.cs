using System.Collections.Generic;
using Alabo.App.Asset.Coupons.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Industry.Shop.Orders.Domain.Enums;
using Alabo.Industry.Shop.Products.Dtos;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Alabo.Industry.Shop.Deliveries.Dtos
{
    /// <summary>
    ///     店铺对象
    /// </summary>
    public class StoreItem
    {
        /// <summary>
        ///     店铺Id
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))] public ObjectId StoreId { get; set; }

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
        ///     快递方式
        /// </summary>
        public ExpressType ExpressType { get; set; } = ExpressType.Express;

        /// <summary>
        ///     用户可以用的优惠券
        /// </summary>
        public List<UserCoupon> Coupons { get; set; }
    }
}