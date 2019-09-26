using System;
using System.Collections.Generic;

namespace Alabo.Industry.Shop.Deliveries.Domain.Entities.Extensions
{
    /// <summary>
    ///     店铺分类
    /// </summary>
    public class StoreCategory
    {
        /// <summary>
        ///     id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     子分类
        /// </summary>
        public List<StoreCategory> Children { get; set; }
    }
}