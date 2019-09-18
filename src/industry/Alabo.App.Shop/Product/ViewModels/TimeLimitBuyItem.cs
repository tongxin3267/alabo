using System;

namespace Alabo.App.Shop.Product.ViewModels
{
    /// <summary>
    /// TimeLimitBuyItem
    /// </summary>
    public class TimeLimitBuyItem
    {
        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// bn
        /// </summary>
        public string Bn { get; set; }

        /// <summary>
        /// price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// thumbnail url
        /// </summary>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// start time
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// end time
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Stock
        /// </summary>
        public long Stock { get; set; }

        /// <summary>
        /// sold count
        /// </summary>
        public long SoldCount { get; set; }
    }
}