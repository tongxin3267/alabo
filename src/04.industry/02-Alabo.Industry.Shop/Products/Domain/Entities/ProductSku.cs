using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Shop.Product.Domain.Enums;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Product.Domain.Entities
{

    /// <summary>
    ///     Class ProductSku.
    /// </summary>
    [ClassProperty(Name = "产品最小库存")]
    public class ProductSku : AggregateDefaultRoot<ProductSku>
    {

        /// <summary>
        ///     商品Guid
        /// </summary>
        [Display(Name = "商品Guid")]
        public long ProductId { get; set; }

        /// <summary>
        ///     商品货号
        /// </summary>
        [Display(Name = "商品货号")]
        public string Bn { get; set; }

        /// <summary>
        ///     商品条形码
        /// </summary>
        [Display(Name = "商品条形码")]
        public string BarCode { get; set; }

        /// <summary>
        ///     商品进货价，指针对卖家的进货价格 该价格通常用于与卖家的货款的结算 比如货号为X002的衣服从供货商进货价位100元
        /// </summary>
        [Display(Name = "商品进货价")]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        ///     商品成本价，指卖家的成本价格，该价格统称 成本价=进货价+实际的成本（比如员工工资，库存、损耗）
        /// </summary>
        [Display(Name = "商品成本价")]
        public decimal CostPrice { get; set; }

        /// <summary>
        ///     市场价 通常指商品商品的吊牌价格，一般用于显示
        /// </summary>
        [Display(Name = "市场价")]
        public decimal MarketPrice { get; set; } = 0;

        /// <summary>
        ///     销售价 用户购买的价格，和订单相关，生成订单的时候，使用这个价格
        /// </summary>
        [Display(Name = "销售价")]
        public decimal Price { get; set; }

        /// <summary>
        ///     虚拟资产，最高可使用虚拟资产支付的价格
        ///     Gets or sets the maximum pay price.
        /// </summary>
        [Display(Name = "最高可使用虚拟资产支付的价格")]
        public decimal MaxPayPrice { get; set; }

        /// <summary>
        ///     Gets or sets the minimum pay cash.
        ///     现金的最低支付额度
        /// </summary>
        [Display(Name = "现金的最低支付额度")]
        public decimal MinPayCash { get; set; }

        /// <summary>
        ///     价格显示方式，最终页面上的显示价格 页面上显示价格
        /// </summary>

        [Display(Name = "显示价格")]
        public string DisplayPrice { get; set; }

        /// <summary>
        ///     分润价格
        /// </summary>
        [Display(Name = "分润价格")]
        public decimal FenRunPrice { get; set; } = 0;

        /// <summary>
        ///     商品的重量，用于按重量计费的运费模板。注意：单位为kg。只能传入数值类型（包含小数），不能带单位，单位默认为kg。
        /// </summary>
        [Display(Name = "商品的重量")]
        public decimal Weight { get; set; }

        /// <summary>
        ///     表示商品的体积，如果需要使用按体积计费的运费模板，一定要设置这个值。该值的单位为立方米（m3），如果是其他单位，请转换成成立方米
        /// </summary>
        [Display(Name = "表示商品的体积")]
        public decimal Size { get; set; }

        /// <summary>
        ///     商品库存,用商品所有的SKU库存 相加的总数
        /// </summary>
        [Display(Name = "商品库存")]
        public long Stock { get; set; }

        /// <summary>
        ///     库存存放地
        /// </summary>
        [Display(Name = "库存存放地")]
        public string StorePlace { get; set; }

        /// <summary>
        ///     Gets or sets the property json.
        /// </summary>
        [Display(Name = "财产")]
        public string PropertyJson { get; set; }

        /// <summary>
        ///     规格属性说明,属性、规格的文字说明 比如：绿色 XL
        /// </summary>
        [Display(Name = "规格属性说明")]
        public string PropertyValueDesc { get; set; }

        /// <summary>
        ///     sku是否上架
        /// </summary>
        [Display(Name = "sku是否上架")]
        public ProductStatus ProductStatus { get; set; }

        /// <summary>
        ///     最后更新时间,格式:yyyy-MM-dd HH:mm:ss
        /// </summary>
        [Display(Name = "最后更新时间")]
        public DateTime Modified { get; set; } = DateTime.Now;

        /// <summary>
        ///     规格sn，由propertyId拼接而成,页面传递用,格式"guid1|guid2|guid3|"
        /// </summary>
        [Display(Name = "规格sn")]
        public string SpecSn { get; set; }

        /// <summary>
        /// 会员等级价 List<SkuGradePriceItem> 的json数据
        /// </summary>
        public string GradePrice { get; set; }

        /// <summary>
        /// 会员等级
        /// 前端显示
        /// </summary>
        public List<SkuGradePriceItem> GradePriceList { get; set; }
    }

    /// <summary>
    /// 会员等级价折扣
    /// </summary>

    public class SkuGradePriceItem
    {
        /// <summary>
        /// 会员等级
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 等级名称
        /// </summary>
        [Display(Name = "等级名称")]
        public string Name { get; set; }

        /// <summary>
        ///     销售价 用户购买的价格，和订单相关，生成订单的时候，使用这个价格
        /// </summary>
        [Display(Name = "销售价")]
        public decimal Price { get; set; }

        ///// <summary>
        /////     分润价格
        ///// </summary>
        //[Display(Name = "分润价格")]
        //public decimal FenRunPrice { get; set; } = 0;

        ///// <summary>
        /////     价格显示方式，最终页面上的显示价格 页面上显示价格
        ///// </summary>

        //[Display(Name = "显示价格")]
        //public string DisplayPrice { get; set; }

        ///// <summary>
        /////     虚拟资产，最高可使用虚拟资产支付的价格
        /////     Gets or sets the maximum pay price.
        ///// </summary>
        //[Display(Name = "最高可使用虚拟资产支付的价格")]
        //public decimal MaxPayPrice { get; set; }

        ///// <summary>
        /////     Gets or sets the minimum pay cash.
        /////     现金的最低支付额度
        ///// </summary>
        //[Display(Name = "现金的最低支付额度")]
        //public decimal MinPayCash { get; set; }
    }

    public class ProductSkuTableMap : MsSqlAggregateRootMap<ProductSku>
    {

        protected override void MapTable(EntityTypeBuilder<ProductSku> builder)
        {
            builder.ToTable("Shop_ProductSku");
        }

        protected override void MapProperties(EntityTypeBuilder<ProductSku> builder)
        {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.Version);
            builder.Ignore(e => e.GradePriceList);
            if (TenantContext.IsTenant) {
                // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
            }
        }
    }
}