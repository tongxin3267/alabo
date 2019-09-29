using System.Collections.Generic;
using Alabo.Domains.Query.Dto;
using Alabo.Industry.Shop.Deliveries.Dtos;

namespace Alabo.Industry.Shop.Orders.Dtos
{
    /// <summary>
    ///     店铺和商品Sku
    ///     确认下单页面
    /// </summary>
    public class StoreProductSku : EntityDto
    {
        /// <summary>
        ///     总数量
        ///     包括店铺所有的商品
        /// </summary>
        public long TotalCount { get; set; } = 0;

        /// <summary>
        ///     总金额
        ///     包括店铺所有商品的金额，和
        /// </summary>
        public decimal TotalAmount { get; set; } = 0;

        /// <summary>
        ///     店铺商品合计
        /// </summary>
        public List<StoreItem> StoreItems { get; set; } = new List<StoreItem>();

        /// <summary>
        ///     Gets or sets the allow moneys.
        ///     改订单允许使用的资产
        /// </summary>
        /// <value>
        ///     The allow moneys.
        /// </value>
        public IList<OrderMoneyItem> AllowMoneys { get; set; } = new List<OrderMoneyItem>();

        /// <summary>
        ///     Gets or sets the sign.
        ///     购物签名，每次购物时签名都不一样
        /// </summary>
        /// <value>
        ///     The sign.
        /// </value>
        public string Sign { get; set; }
    }
}