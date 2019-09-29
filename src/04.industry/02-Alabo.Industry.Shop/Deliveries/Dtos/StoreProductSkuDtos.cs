using System.Collections.Generic;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Deliveries.Dtos
{
    /// <summary>
    ///     Class StoreProductSkuDtos.
    /// </summary>
    public class StoreProductSkuDtos
    {
        /// <summary>
        ///     Gets or sets the store ids.
        /// </summary>
        public IEnumerable<ObjectId> StoreIds { get; set; }

        /// <summary>
        ///     Gets or sets the product sku ids.
        /// </summary>
        public IEnumerable<long> ProductSkuIds { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is API image.
        /// </summary>
        public bool IsApiImage { get; set; } = true;

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is group buy.
        /// </summary>
        public bool IsGroupBuy { get; set; } = false;

        /// <summary>
        ///     Gets or sets the product identifier.
        /// </summary>
        public long ProductId { get; set; }
    }
}