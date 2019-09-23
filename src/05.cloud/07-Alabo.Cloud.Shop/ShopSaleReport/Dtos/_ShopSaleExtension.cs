using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities.Extensions;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace ZKCloud.App.Core.User.Domain.Entities.Extensions {

    /// <summary>
    ///     商城销售统计
    /// </summary>
    public class ShopSaleExtension : EntityExtension {

        /// <summary>
        ///     商城销售额
        ///     用户本身的商城销售额
        /// </summary>
        public ShopSale UserShopSale { get; set; } = new ShopSale();

        /// <summary>
        ///     推荐用户销售额
        /// </summary>
        public ShopSale RecomendShopSale { get; set; } = new ShopSale();

        /// <summary>
        ///     商城销售额
        ///     间推用户销售额
        /// </summary>
        public ShopSale SecondShopSale { get; set; } = new ShopSale();

        /// <summary>
        ///     商城销售额
        ///     团队用户商城销售
        /// </summary>
        public ShopSale TeamShopSale { get; set; } = new ShopSale();

        /// <summary>
        ///     最后更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, ListShow = true, EditShow = false, SortOrder = 10002,
            Width = "160")]
        public DateTime ModifiedTime { get; set; } = DateTime.Now;
    }

    /// <summary>
    ///     商城销售统计
    /// </summary>
    public class ShopSale {

        /// <summary>
        ///     累计订单数量
        /// </summary>
        public long TotalOrderCount { get; set; }

        /// <summary>
        ///     累计购买商品数量
        ///     与订单表TotalCount对应
        /// </summary>
        public long TotalProductCount { get; set; }

        /// <summary>
        ///     商品销售价
        ///     累计商品销售价格
        ///     与订单表TotalAmount对应
        /// </summary>
        public decimal TotalPriceAmount { get; set; }

        /// <summary>
        ///     累计分润价格
        /// </summary>
        public decimal TotalFenRunAmount { get; set; }

        /// <summary>
        ///     订单实际支付的金额
        /// </summary>
        public decimal TotalPaymentAmount { get; set; }

        /// <summary>
        ///     总服务费
        /// </summary>
        public decimal TotalFeeAmount { get; set; }

        /// <summary>
        ///     累计邮费
        /// </summary>
        public decimal TotalExpressAmount { get; set; }

        /// <summary>
        ///     商城销售额
        /// </summary>
        public IList<PriceStyleSale> PriceStyleSales { get; set; } = new List<PriceStyleSale>();
    }

    public class PriceStyleSale {

        /// <summary>
        ///     商城模式Id
        /// </summary>
        [Display(Name = "商城模式Id")]
        public Guid PriceStyleId { get; set; }

        /// <summary>
        ///     商城名称
        /// </summary>
        /// <summary>
        ///     直推会员数量
        /// </summary>
        [Display(Name = "商城名称")]
        [Field(ListShow = true, EditShow = true, SortOrder = 4)]
        public string PriceStyleName { get; set; }

        /// <summary>
        ///     累计订单数量
        /// </summary>
        [Display(Name = "累计订单数量")]
        public long OrderCount { get; set; }

        /// <summary>
        ///     累计购买商品数量
        /// </summary>
        [Display(Name = "累计购买商品数量")]
        public long ProductCount { get; set; }

        /// <summary>
        ///     商品销售价
        ///     累计商品销售价格
        /// </summary>
        [Display(Name = "商品销售价")]
        public decimal PriceAmount { get; set; }

        /// <summary>
        ///     累计分润价格
        /// </summary>
        [Display(Name = "累计分润价格")]
        public decimal FenRunAmount { get; set; }

        /// <summary>
        ///     订单实际支付的金额
        /// </summary>
        [Display(Name = "订单实际支付的金额")]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        ///     服务费
        /// </summary>
        [Display(Name = "服务费")]
        public decimal FeeAmount { get; set; }
    }
}