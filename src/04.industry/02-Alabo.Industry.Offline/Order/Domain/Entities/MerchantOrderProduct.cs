using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Industry.Offline.Order.Domain.Entities.Extensions;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alabo.Industry.Offline.Order.Domain.Entities
{

    /// <summary>
    /// 订单商品
    /// </summary>
    [ClassProperty(Name = "订单商品")]
    public class MerchantOrderProduct : AggregateDefaultRoot<MerchantOrderProduct>
    {
        /// <summary>
        /// 店铺id
        /// </summary>
        [Display(Name = "店铺id")]
        public string MerchantStoreId { get; set; }

        /// <summary>
        /// 订单id
        /// </summary>
        [Display(Name = "订单id")]
        public long OrderId { get; set; }

        /// <summary>
        /// 商品id
        /// </summary>
        [Display(Name = "商品id")]
        public string MerchantProductId { get; set; }

        /// <summary>
        /// SkuId
        /// </summary>
        [Display(Name = "SkuId")]
        public string SkuId { get; set; }

        /// <summary>
        ///     商品数量
        /// </summary>
        [Display(Name = "商品数量")]
        public long Count { get; set; }

        /// <summary>
        ///     商品单价
        /// </summary>
        [Display(Name = "商品单价")]
        public decimal Amount { get; set; }

        /// <summary>
        ///     单个商品分润价格,根据这个字段来分润
        /// </summary>
        [Display(Name = "单个商品分润价格")]
        public decimal FenRunAmount { get; set; }

        /// <summary>
        ///     订单实际支付的金额
        /// </summary>
        [Display(Name = "订单实际支付的金额")]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        /// Extension
        /// </summary>
        [Display(Name = "扩展信息")]
        public string Extension { get; set; }

        /// <summary>
        /// extension
        /// </summary>
        public MerchantOrderProductExtension MerchantOrderProductExtension { get; set; }
    }

    public class OrderProductTableMap : MsSqlAggregateRootMap<MerchantOrderProduct>
    {

        protected override void MapTable(EntityTypeBuilder<MerchantOrderProduct> builder)
        {
            builder.ToTable("Offline_MerchantOrderProduct");
        }

        protected override void MapProperties(EntityTypeBuilder<MerchantOrderProduct> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.Version);
            builder.Ignore(e => e.MerchantOrderProductExtension);
        }
    }
}