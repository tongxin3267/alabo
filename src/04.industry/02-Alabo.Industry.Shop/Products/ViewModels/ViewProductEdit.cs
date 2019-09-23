using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Shop.Product.Domain.CallBacks;
using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.App.Shop.Product.Domain.Enums;
using Alabo.App.Shop.Store.Domain.Entities.Extensions;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Shop.Product.ViewModels {

    /// <summary>
    ///     Class ViewProductEdit.
    /// </summary>
    /// <seealso cref="BaseViewModel" />
    public class ViewProductEdit : BaseViewModel {

        /// <summary>
        ///     商品信息
        /// </summary>
        /// <value>The product.</value>
        public Domain.Entities.Product Product { get; set; }

        /// <summary>
        ///     商品详情
        /// </summary>
        /// <value>The product detail.</value>
        public ProductDetail ProductDetail { get; set; }

        /// <summary>
        ///     是否为管理员添加商品
        ///     如果为true则为管理员添加商品，如果为false则为供应商添加商品
        /// </summary>
        /// <value><c>true</c> if this instance is admin add product; otherwise, <c>false</c>.</value>
        public bool IsAdminAddProduct { get; set; } = true;

        /// <summary>
        ///     品牌列表
        /// </summary>
        /// <value>The brand items.</value>
        public IEnumerable<SelectListItem> BrandItems { get; set; }

        /// <summary>
        ///     类目列表
        /// </summary>
        /// <value>The category items.</value>
        public IEnumerable<SelectListItem> CategoryItems { get; set; }

        /// <summary>
        ///     是否为商品复制
        /// </summary>
        /// <value>The is copy.</value>
        public int IsCopy { get; set; } = 0;

        /// <summary>
        ///     Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public Category.Domain.Entities.Category Category { get; set; }

        /// <summary>
        ///     商品类目
        /// </summary>
        /// <value>The category identifier.</value>
        [Display(Name = "商品类目")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public Guid CategoryId { get; set; }

        /// <summary>
        ///     所属商城
        /// </summary>
        /// <value>The price style identifier.</value>
        [Display(Name = "所属商城")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public Guid PriceStyleId { get; set; }

        /// <summary>
        ///     商品状态
        /// </summary>
        /// <value>The product status.</value>
        [Display(Name = "商品状态")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public ProductStatus ProductStatus { get; set; }

        /// <summary>
        ///     Gets or sets the product configuration.
        /// </summary>
        /// <value>The product configuration.</value>
        public ProductConfig ProductConfig { get; set; }

        /// <summary>
        ///     Gets or sets the delivery template items.
        /// </summary>
        /// <value>The delivery template items.</value>
        public IEnumerable<SelectListItem> DeliveryTemplateItems { get; set; }

        /// <summary>
        ///     Gets or sets the store.
        /// </summary>
        /// <value>The store.</value>
        public Store.Domain.Entities.Store Store { get; set; }

        /// <summary>
        ///     Gets or sets the price style items.
        /// </summary>
        /// <value>The price style items.</value>
        public IList<PriceStyleConfig> PriceStyleItems { get; set; }

        /// <summary>
        ///     商品sku列表
        /// </summary>
        /// <value>The product skus.</value>
        public List<ProductSku> ProductSkus { get; set; }

        /// <summary>
        ///     商品分类
        /// </summary>
        /// <value>The classes.</value>
        public string Classes { get; set; }

        /// <summary>
        ///     店铺商品分类
        /// </summary>
        /// <value>The classes store.</value>
        public string ClassesStore { get; set; }

        /// <summary>
        ///     商品标签
        /// </summary>
        /// <value>The tags.</value>
        public string Tags { get; set; }

        /// <summary>
        ///     SKU数据
        /// </summary>
        /// <value>The sku json.</value>
        [Display(Name = "商品SKU")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        //[Field("拼团商品价格设置", ControlsType.Json, PlaceHolder = "拼团价支持多模式,如:拼团价为100元，商城模式为50%现金+50%积分，则可用50元+50积分购买", ListShow = false, EditShow = true, JsonCanAddOrDelete = false, ExtensionJson = "SkuProducts")]
        public string SkuJson { get; set; }

        /// <summary>
        ///     当前商品的价格模型（商城模型）
        /// </summary>
        /// <value>The price style configuration.</value>
        public PriceStyleConfig PriceStyleConfig { get; set; }

        /// <summary>
        ///     当前商品的货币类型
        /// </summary>
        /// <value>The money type configuration.</value>
        public MoneyTypeConfig MoneyTypeConfig { get; set; }

        /// <summary>
        ///     手机版电脑详情
        /// </summary>
        /// <value>The intro.</value>
        [Display(Name = "电脑端描述")]
        public string Intro { get; set; }

        /// <summary>
        ///     Gets or sets the mobile intro.
        /// </summary>
        /// <value>The mobile intro.</value>
        [Display(Name = "手机端描述")]
        public string MobileIntro { get; set; }

        /// <summary>
        ///     SEO标题
        /// </summary>
        /// <value>The meta title.</value>
        [Display(Name = "SEO标题")]
        [StringLength(200, ErrorMessage = ErrorMessage.MaxStringLength)]
        public string MetaTitle { get; set; }

        /// <summary>
        ///     SEO关键字
        /// </summary>
        /// <value>The meta keywords.</value>
        [Display(Name = "SEO关键字")]
        [StringLength(300, ErrorMessage = ErrorMessage.MaxStringLength)]
        public string MetaKeywords { get; set; }

        /// <summary>
        ///     SEO描述
        /// </summary>
        /// <value>The meta description.</value>
        [Display(Name = "SEO描述")]
        [StringLength(400, ErrorMessage = ErrorMessage.MaxStringLength)]
        public string MetaDescription { get; set; }

        /// <summary>
        ///     Gets or sets the images.
        /// </summary>
        /// <value>The images.</value>
        [Display(Name = "商品图片")]
        public string Images { get; set; }

        /// <summary>
        ///     所在区县
        /// </summary>
        /// <value>The region identifier.</value>
        [Display(Name = "所在区县")]
        public long RegionId { get; set; }

        /// <summary>
        ///     供应商Id
        /// </summary>
        public long StoreId { get; set; }
    }
}