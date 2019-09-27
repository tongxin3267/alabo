using System.Collections.Generic;
using Alabo.Industry.Shop.Products.ViewModels;
using Alabo.Users.Entities;

namespace Alabo.Industry.Shop.Deliveries.Dtos
{
    /// <summary>
    ///     店铺信息
    /// </summary>
    public class StoreInfoOutput
    {
        /// <summary>
        ///     店铺信息
        /// </summary>
        public Store Store { get; set; }

        /// <summary>
        ///     用户信息
        /// </summary>
        public User User { get; set; }

        /// <summary>
        ///     店铺等级名称
        /// </summary>
        public string StoreGradeName { get; set; }

        /// <summary>
        ///     热销商品
        /// </summary>
        public IList<ProductItem> ProductItems { get; set; }
    }
}