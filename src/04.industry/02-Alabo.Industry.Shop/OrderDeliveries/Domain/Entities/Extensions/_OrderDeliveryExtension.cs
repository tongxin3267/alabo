using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities.Extensions;
using Alabo.Industry.Shop.Orders.Domain.Entities;
using Alabo.Users.Entities;
using MongoDB.Bson;
using ZKCloud.Open.LogisticsTracking.kdniao;

namespace Alabo.Industry.Shop.OrderDeliveries.Domain.Entities.Extensions
{
    /// <summary>
    ///     发货记录表
    /// </summary>
    public class OrderDeliveryExtension : EntityExtension
    {
        /// <summary>
        ///     操作时管理员
        /// </summary>
        public User AdminUser { get; set; }

        /// <summary>
        ///     商品发货信息
        /// </summary>
        public IList<ProductDeliveryInfo> ProductDeliveryInfo { get; set; } = new List<ProductDeliveryInfo>();

        /// <summary>
        ///     物流跟踪信息
        ///     从Open上的Api接口获取,根据快递单Guid和快递单号
        /// </summary>
        public LogisticsTracking LogisticsTracking { get; set; }

        /// <summary>
        ///     订单数据
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        ///     发货记录备注
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    ///     发货商品信息
    ///     根据OrderProduct来
    /// </summary>
    public class ProductDeliveryInfo
    {
        /// <summary>
        ///     Gets or sets the store identifier.
        /// </summary>
        /// <value>
        ///     The store identifier.
        /// </value>
        public ObjectId StoreId { get; set; }

        /// <summary>
        ///     商品ID
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        ///     Gets or sets the product sku identifier.
        ///     商品SkuId
        /// </summary>
        /// <value>
        ///     The product sku identifier.
        /// </value>
        public long ProductSkuId { get; set; }

        /// <summary> 商品SKu货号 </summary>
        public string Bn { get; set; }

        /// <summary>
        ///     商品购买数量
        /// </summary>
        public long BuyCount { get; set; }

        /// <summary>
        ///     发货数量
        /// </summary>
        public long Count { get; set; }

        /// <summary>
        ///     交易时商品对应规格属性说明,属性、规格的文字说明  比如：绿色 XL
        /// </summary>
        public string PropertyValueDesc { get; set; }

        /// <summary>
        ///     商品名称
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     缩略图的URL,通过主图生成
        ///     缩略图地址,根据后台设置规则生成，商城通用访问或显示地址,通用用于列表页或首页显示用格式：“OriginalUrl_宽X高.文件后缀”,参考淘宝
        /// </summary>
        [Display(Name = "缩略图的URL")]
        public string ThumbnailUrl { get; set; }

        /// <summary> 销售价 用户购买的价格，和订单相关，生成订单的时候，使用这个价格 </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     显示价格
        /// </summary>
        /// <value>
        ///     The display price.
        /// </value>
        public string DisplayPrice { get; set; }

        /// <summary>
        ///     Gets or sets the price style identifier.
        ///     货币类型
        /// </summary>
        /// <value>
        ///     The price style identifier.
        /// </value>
        public Guid PriceStyleId { get; set; }
    }
}