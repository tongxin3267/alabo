using System;

namespace Alabo.Industry.Shop.Activitys.ViewModels
{
    /// <summary>
    ///     ProductGradePrice
    /// </summary>
    public class ProductGradePrice
    {
        /// <summary>
        ///     product id
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        ///     product sku id
        /// </summary>
        public long ProductSkuId { get; set; }

        /// <summary>
        ///     member grade id
        /// </summary>
        public Guid GradeId { get; set; }

        /// <summary>
        ///     member price
        /// </summary>
        public decimal MemberPrice { get; set; }
    }
}