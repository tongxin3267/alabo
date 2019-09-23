using System;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Datas.Ef.SqlServer;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Alabo.Domains.Enums;
using Alabo.Tenants;

namespace Alabo.App.Market.PresaleProducts.Domain.Entities {

    /// <summary>
    /// 预售产品
    /// </summary>
    [ClassProperty(Name = "预售产品", Description = "预售产品")]
    [Table("Market_PresaleProduct")]
    public class PresaleProduct : AggregateMongodbRoot<PresaleProduct> {

        /// <summary>
        /// 供应商 Id，0 表示平台商品
        /// </summary>
        [Display(Name = "供应商")]
        public long StoreId { get; set; }

        /// <summary>
        /// 产品id
        /// </summary>
        [Display(Name = "产品id")]
        public long ProductId { get; set; }

        /// <summary>
        /// 产品SkuId
        /// </summary>
        [Display(Name = "产品SkuId")]
        public long SkuId { get; set; }

        /// <summary>
        /// 商品价格模式的配置Id 与PriceStyleConfig 对应
        /// </summary>
        [Display(Name = "商品价格模式的配置Id")]
        public Guid PriceStyleId { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        [Display(Name = "商品价格")]
        public decimal Price { get; set; }

        /// <summary>
        /// 虚拟货币价格
        /// </summary>
        [Display(Name = "虚拟货币价格")]
        public decimal VirtualPrice { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        [Display(Name = "库存")]
        public long Stock { get; set; }

        /// <summary>
        /// 已售
        /// </summary>
        [Display(Name = "已售")]
        public long QuantitySold { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public int Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        public long Sort { get; set; }
    }
}