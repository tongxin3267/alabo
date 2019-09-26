using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Shop.Store.Domain.Dtos
{

    public class ProductEditOuput {

        /// <summary>
        /// 商品
        /// </summary>
        [Display(Name = "商品")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public Product.Domain.Entities.Product Product { get; set; } = new Product.Domain.Entities.Product();

        /// <summary>
        ///     商品sku列表
        /// </summary>

        [Display(Name = "商品Sku")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public List<ProductSku> ProductSkus { get; set; } = new List<ProductSku>();

        /// <summary>
        ///     商品详情
        /// </summary>
        /// <value>The product detail.</value>
        [Display(Name = "商品详情")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public ProductDetail ProductDetail { get; set; } = new ProductDetail();

        /// <summary>
        /// 图片
        /// </summary>
        public List<string> Images { get; set; } = new List<string>();

        /// <summary>
        /// 类目属性
        /// </summary>
        [Display(Name = "商品类目")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public Category.Domain.Entities.Category Category { get; set; }

        /// <summary>
        /// 设置信息
        /// </summary>
        public ProductViewEditSetting Setting { get; set; } = new ProductViewEditSetting();

        /// <summary>
        /// 店铺
        /// </summary>
        [Display(Name = "店铺")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public Entities.Store Store { get; set; }

        /// <summary>
        /// 店铺通用级联
        /// </summary>
        public StoreRelation Relation { get; set; } = new StoreRelation();

        /// <summary>
        /// 是否上架
        /// </summary>
        public bool OnSale { get; set; } = true;
    }

    public class ProductViewEditSetting {

        /// <summary>
        /// 是否为添加商品
        /// </summary>
        public bool IsAdd { get; set; } = true;

        /// <summary>
        /// 页面标题
        /// </summary>
        public string Title { get; set; } = "商品添加";
    }

    /// <summary>
    /// 店铺级联数据
    /// </summary>
    public class StoreRelation {

        /// <summary>
        /// 店铺标签
        /// </summary>
        public List<KeyValue> Tags { get; set; } = new List<KeyValue>();

        /// <summary>
        /// 店铺分类
        /// </summary>
        public List<RelationApiOutput> Classes { get; set; } = new List<RelationApiOutput>();

        /// <summary>
        /// 运费模板
        /// </summary>
        public List<KeyValue> DeliveryTemplates { get; set; } = new List<KeyValue>();
    }
}