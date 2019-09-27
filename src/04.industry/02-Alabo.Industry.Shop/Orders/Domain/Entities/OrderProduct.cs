using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Industry.Shop.Orders.Domain.Entities.Extensions;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alabo.Industry.Shop.Orders.Domain.Entities
{
    /// <summary>
    ///     订单商品表
    /// </summary>
    [ClassProperty(Name = "发货记录表")]
    public class OrderProduct : AggregateDefaultRoot<OrderProduct>
    {
        /// <summary>
        ///     订单号ID
        /// </summary>
        [Display(Name = "订单号ID")]
        public long OrderId { get; set; }

        /// <summary>
        ///     商品 Id
        /// </summary>

        [Display(Name = "商品")]
        public long ProductId { get; set; }

        /// <summary>
        ///     SkuId
        /// </summary>
        [Display(Name = "最小存货")]
        public long SkuId { get; set; }

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
        ///     商品总价格
        ///     商品单价乘以数量的总金额
        ///     商品总价格(TotalAmount)=商品单价(Amount)*商品数量(Count)
        /// </summary>
        [Display(Name = "商品总价格")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     订单实际支付的金额
        /// </summary>
        [Display(Name = "订单实际支付的金额")]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        ///     Gets or sets the extensions.
        ///     保存订单商品扩展数据
        /// </summary>
        /// <value>
        ///     The extensions.
        /// </value>
        [Field(ExtensionJson = "OrderProductExtension")]
        [Display(Name = "保存订单商品扩展数据")]
        public string Extension { get; set; }

        [Display(Name = "订单产品扩展")] public OrderProductExtension OrderProductExtension { get; set; }
    }

    public class OrderProductTableMap : MsSqlAggregateRootMap<OrderProduct>
    {
        protected override void MapTable(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.ToTable("Shop_OrderProduct");
        }

        protected override void MapProperties(EntityTypeBuilder<OrderProduct> builder)
        {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FenRunAmount).IsRequired();
            builder.Ignore(e => e.OrderProductExtension);
         
            if (TenantContext.IsTenant)
            {
                // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
            }
        }
    }
}