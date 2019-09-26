using Alabo.Domains.Entities.Extensions;

namespace Alabo.Industry.Shop.Orders.Domain.Entities.Extensions
{
    /// <summary>
    ///     订单留言
    /// </summary>
    public class OrderMessage : EntityExtension
    {
        /// <summary>
        ///     买家留言
        /// </summary>
        /// <value>
        ///     The user message.
        /// </value>
        public string BuyerMessage { get; set; }

        /// <summary>
        ///     卖家留言
        /// </summary>
        public string SellerMessge { get; set; }

        /// <summary>
        ///     平台留言
        /// </summary>
        public string PlatplatformMessage { get; set; }
    }

    /// <summary>
    ///     订单备注
    /// </summary>
    public class OrderRemark : EntityExtension
    {
        /// <summary>
        ///     订单ID
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        ///     买家备注
        /// </summary>
        /// <value>
        ///     The user message.
        /// </value>
        public string BuyerRemark { get; set; }

        /// <summary>
        ///     卖家备注
        /// </summary>
        public string SellerRemark { get; set; }

        /// <summary>
        ///     平台备注
        /// </summary>
        public string PlatplatformRemark { get; set; }
    }
}