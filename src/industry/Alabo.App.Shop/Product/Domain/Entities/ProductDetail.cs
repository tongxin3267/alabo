using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Shop.Product.Domain.Entities.Extensions;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Product.Domain.Entities
{

    /// <summary>
    ///     商品统计数据
    /// </summary>
    [ClassProperty(Name = "商品统计数据")]
    public class ProductDetail : AggregateDefaultRoot<ProductDetail>
    {
        public long ProductId { get; set; }

        /// <summary>
        ///     库存警报数
        /// </summary>
        [Display(Name = "库存警报数")]
        [Range(0, 99999999, ErrorMessage = "商品库存必须为大于等于0的整数")]
        public long StockAlarm { get; set; }

        /// <summary>
        ///     商品详细介绍
        /// </summary>
        [Display(Name = "商品详细介绍")]
        public string Intro { get; set; }

        /// <summary>
        ///     手机版、App、移动端商品详情页面
        /// </summary>

        [Display(Name = "手机版详情")]
        public string MobileIntro { get; set; }

        /// <summary>
        ///     价格计量单位
        /// </summary>
        [Display(Name = "价格计量单位")]
        public string PriceUnit { get; set; } = "件";

        /// <summary>
        ///     SEO标题
        /// </summary>
        [Display(Name = "SEO标题")]
        [StringLength(200, ErrorMessage = "Seo标题长度不能超过200个字符")]
        public string MetaTitle { get; set; }

        /// <summary>
        ///     SEO关键字
        /// </summary>
        [Display(Name = "SEO关键字")]
        [StringLength(300, ErrorMessage = "SEO关键字长度不能超过300个字符")]
        public string MetaKeywords { get; set; }

        /// <summary>
        ///     SEO描述
        /// </summary>
        [Display(Name = "SEO描述")]
        [StringLength(400, ErrorMessage = "SEO描述长度不能超过400个字符")]
        public string MetaDescription { get; set; }

        /// <summary>
        ///     商品缩略图Json格式
        ///     List
        ///     <ProductThum>
        ///         对象
        ///         多张图片
        /// </summary>
        [Display(Name = "商品缩略图")]
        public string ImageJson { get; set; }

        /// <summary>
        ///     商品属性对象，使用Json格式存储
        ///     List<ProductProperty>Json对象
        /// </summary>
        [Display(Name = "商品属性对象")]
        public string PropertyJson { get; set; }

        /// <summary>
        ///     扩展属性
        /// </summary>
        [Field(ExtensionJson = "ProductDetailExtension")]
        [Display(Name = "扩展属性")]
        public string Extension { get; set; }
        /// <summary>
        ///     扩展属性
        /// </summary>
        [Display(Name = "产品细节扩展")]
        public ProductDetailExtension ProductDetailExtension { get; set; } = new ProductDetailExtension();

        public class ProductDetailTableMap : MsSqlAggregateRootMap<ProductDetail>
        {

            protected override void MapTable(EntityTypeBuilder<ProductDetail> builder)
            {
                builder.ToTable("ZKShop_ProductDetail");
            }

            protected override void MapProperties(EntityTypeBuilder<ProductDetail> builder)
            {
                //应用程序编号
                builder.HasKey(e => e.Id);
                builder.Ignore(e => e.ProductDetailExtension);
                builder.Ignore(e => e.Version);
                if (TenantContext.IsTenant)
                {
                    // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
                }
            }
        }
    }
}