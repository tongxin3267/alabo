using System;

namespace Alabo.Industry.Shop.Products.ViewModels {

    /// <summary>
    ///     存入 OrderProduct 的商品活动信息
    /// </summary>
    public class ProductActivityInfo {

        /// <summary>
        ///     获取或设置活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置活动的类型
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///     总共优惠掉的金额
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        ///     获取或设置活动的 Id
        /// </summary>
        public long ActivityId { get; set; }

        /// <summary>
        ///     SKUId
        /// </summary>
        public Guid SkuId { get; set; }

        /// <summary>
        ///     商品 Id
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        ///     附加JSON
        /// </summary>
        public string ModulesJson { get; set; }

        /// <summary>
        ///     数量
        /// </summary>
        public int Count { get; set; }
    }
}