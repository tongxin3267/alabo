using System;
using System.Collections.Generic;
using Alabo.Industry.Shop.Orders.Domain.Enums;

namespace Alabo.Industry.Shop.Orders.ViewModels {

    public class ViewBuyProduct {
        public long ProductId { get; set; }

        /// <summary>
        ///     商品Guid
        /// </summary>
        public Guid ProductGuid { get; set; }

        /// <summary>
        ///     购买商品的SkuId
        /// </summary>
        public Guid SkuId { get; set; }

        /// <summary>
        ///     购买商品数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        ///     行业Id
        /// </summary>
        public int IndustryId { get; set; }

        /// <summary>
        ///     商品行业实体 Id
        /// </summary>
        public long EntityId { get; set; }

        /// <summary>
        ///     获取或设置地址 Id
        /// </summary>
        public long AddressId { get; set; }

        /// <summary>
        ///     附加信息，用于前端显示
        /// </summary>
        public List<string> AttachInfo { get; set; } = new List<string>();

        /// <summary>
        ///     促销优惠活动ID
        /// </summary>
        public long PromotionalId { get; set; }

        /// <summary>
        ///     用户选择的出行时间（用来确定是否有活动）
        /// </summary>
        public string IndustryInfo { get; set; }

        /// <summary>
        ///     订单扩展信息，标识将产生的订单类型，直接购买的商品才能使用，购物车里只能是一般流程商品
        /// </summary>
        public OrderType Type { get; set; }

        /// <summary>
        ///     订单扩展信息，预约服务项
        /// </summary>
        public long SupplyId { get; set; }

        /// <summary>
        ///     预约项Id（“,”分隔）
        /// </summary>
        public string BookingRecordIds { get; set; }

        /// <summary>
        ///     服务是否预约
        /// </summary>
        public bool IsBooking { get; set; }

        /// <summary>
        ///     获取来源
        /// </summary>
        public string From { get; set; }
    }
}