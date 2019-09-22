using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Shop.Product.Domain.Enums;

namespace Alabo.App.Shop.Product.Domain.Dtos
{
    /// <summary>
    /// 审核输入
    /// </summary>
    public class AuditProductInput
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public ProductStatus State { get; set; }
        /// <summary>
        /// 审核消息
        /// </summary>
        public string AuditMessage { get; set; }

        /// <summary>
        /// 店铺分类
        /// </summary>
        public List<long> StoreClass { get; set; } = new List<long>();

        /// <summary>
        /// 商品标签
        /// </summary>
        public List<long> Tags { get; set; } = new List<long>();

    }
}
