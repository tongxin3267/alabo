using System;

namespace Alabo.Industry.Shop.Products.Dtos
{
    /// <summary>
    ///     Sku价格
    /// </summary>
    public class SkuPrice
    {
        /// <summary>
        ///     Gets or sets the sku identifier.
        /// </summary>
        /// <value>
        ///     The sku identifier.
        /// </value>
        public long ProductSkuId { get; set; }

        /// <summary>
        ///     Gets or sets the product identifier.
        /// </summary>
        /// <value>
        ///     The product identifier.
        /// </value>
        public long ProductId { get; set; }

        /// <summary>
        ///     Gets or sets the price.
        /// </summary>
        /// <value>
        ///     The price.
        /// </value>
        public decimal Price { get; set; }

        /// <summary>
        ///     最低现金抵现比例
        /// </summary>
        public decimal MinCashRate { get; set; }

        /// <summary>
        ///     Gets or sets the price style identifier.
        /// </summary>
        /// <value>
        ///     The price style identifier.
        /// </value>
        public Guid PriceStyleId { get; set; }
    }
}