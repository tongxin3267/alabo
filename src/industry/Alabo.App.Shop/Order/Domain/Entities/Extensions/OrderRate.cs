using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.Domains.Entities.Extensions;

namespace Alabo.App.Shop.Order.Domain.Entities.Extensions {

    /// <summary>
    ///     订单评价
    /// </summary>
    public class OrderRate : EntityExtension {

        /// <summary>
        ///     买家评价
        /// </summary>
        public OrderRateInfo BuyerRate { get; set; }

        /// <summary>
        ///     卖家评价
        /// </summary>
        public OrderRateInfo SellerRate { get; set; }
    }

    /// <summary>
    ///     评价详情
    /// </summary>
    public class OrderRateInfo {

        /// <summary>
        /// 订单ID
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        ///     评价方式，好评中评，差评
        /// </summary>
        public ReviewType ReviewType { get; set; }

        /// <summary>
        ///     商品评分，描述相符
        ///     最终的商品评分需更新到Product表中
        /// </summary>
        public long ProductScore { get; set; }

        /// <summary>
        ///     物流速度 物流评分
        /// </summary>
        public long LogisticsScore { get; set; }

        /// <summary>
        ///     服务评分
        /// </summary>
        public long ServiceScore { get; set; }

        /// <summary>
        ///     评论图片或视频
        /// </summary>
        public string Images { get; set; }

        /// <summary>
        ///     评论详情
        /// </summary>
        public string Intro { get; set; }
    }
}