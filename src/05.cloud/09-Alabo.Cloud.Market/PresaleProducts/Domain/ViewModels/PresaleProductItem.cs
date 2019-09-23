using System;
using System.Collections.Generic;
using System.Text;

namespace Alabo.App.Market.PresaleProducts.Domain.ViewModels
{
    public class PresaleProductItem
    {
        /// <summary>
        /// 预售商品Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 产品id
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// 商品规格
        /// </summary>
        public string SkuName { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        public string ProductTypeName { get; set; }

        /// <summary>
        /// 成本价格
        /// </summary>
        public decimal CostPrice { get; set; }

        /// <summary>
        /// 虚拟货币价格
        /// </summary>
        public decimal VirtualPrice { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 已售
        /// </summary>
        public int QuantitySold { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
