using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Industry.Offline.Product.Domain.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Industry.Offline.Product.Domain.Entities
{
    /// <summary>
    ///     商品
    /// </summary>
    [Table("Offline_MerchantProduct")]
    [BsonIgnoreExtraElements]
    [ClassProperty(Name = "商品", PageType = ViewPageType.List)]
    [AutoDelete(IsAuto = true)]
    public class MerchantProduct : AggregateMongodbRoot<MerchantProduct>
    {
        /// <summary>
        ///     店铺id
        /// </summary>
        [Display(Name = "店铺id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string MerchantStoreId { get; set; }

        /// <summary>
        ///     商品名称
        /// </summary>
        [Display(Name = "商品名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Name { get; set; }

        /// <summary>
        ///     商品分类
        /// </summary>
        [Display(Name = "商品分类")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 99999999, ErrorMessage = ErrorMessage.NameNotInRang)]
        public long ClassId { get; set; }

        /// <summary>
        ///     商品类型
        /// </summary>
        [Display(Name = "商品类型")]
        public ProductTypeEnum Type { get; set; } = ProductTypeEnum.Single;

        /// <summary>
        ///     商品子菜（套餐可用）
        /// </summary>
        [Display(Name = "商品子菜")]
        public List<ObjectId> ProductIds { get; set; } = new List<ObjectId>();

        /// <summary>
        ///     缩略图的URL,通过主图生成
        /// </summary>
        [Display(Name = "缩略图的URL")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string ThumbnailUrl { get; set; }

        /// <summary>
        ///     商品主图
        /// </summary>
        [Display(Name = "商品主图")]
        public List<string> Images { get; set; } = new List<string>();

        /// <summary>
        ///     商品属性
        /// </summary>
        [Display(Name = "商品属性")]
        public List<MerchantProductProperty> Properties { get; set; } = new List<MerchantProductProperty>();

        /// <summary>
        ///     商品Sku
        /// </summary>
        [Display(Name = "商品Sku")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public List<MerchantProductSku> Skus { get; set; } = new List<MerchantProductSku>();

        /// <summary>
        ///     商品总库存
        /// </summary>
        [Display(Name = "商品总库存")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long Stock { get; set; }

        /// <summary>
        ///     商品单位
        /// </summary>
        [Display(Name = "商品单位")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Unit { get; set; }

        /// <summary>
        ///     商品描述
        /// </summary>
        [Display(Name = "商品描述")]
        public string Description { get; set; }

        /// <summary>
        ///     销售数量
        /// </summary>
        [Display(Name = "销售数量")]
        public long SoldCount { get; set; }
    }

    /// <summary>
    ///     product property
    /// </summary>
    public class MerchantProductProperty
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }

    /// <summary>
    ///     product sku
    /// </summary>
    public class MerchantProductSku
    {
        /// <summary>
        ///     sku id
        /// </summary>
        public string SkuId { get; set; } = ObjectId.GenerateNewId().ToString();

        /// <summary>
        ///     名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Name { get; set; }

        /// <summary>
        ///     销售价
        /// </summary>
        [Display(Name = "销售价")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(0, 99999999, ErrorMessage = ErrorMessage.NameNotInRang)]
        public decimal Price { get; set; }

        /// <summary>
        ///     显示价格
        /// </summary>
        [Display(Name = "显示价格")]
        public decimal DisplayPrice { get; set; }

        /// <summary>
        ///     分润价格
        /// </summary>
        [Display(Name = "分润价格")]
        public decimal FenRunPrice { get; set; } = 0m;

        /// <summary>
        ///     附加价格
        /// </summary>
        [Display(Name = "附加价格")]
        public decimal AttachPrice { get; set; } = 0m;

        /// <summary>
        ///     商品库存
        /// </summary>
        [Display(Name = "商品库存")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(0, 99999999, ErrorMessage = ErrorMessage.NameNotInRang)]
        public long Stock { get; set; }

        /// <summary>
        ///     规格属性说明
        /// </summary>
        [Display(Name = "规格属性说明")]
        public string PropertyValueDesc { get; set; }

        /// <summary>
        ///     是否默认
        /// </summary>
        public bool IsDefault { get; set; }
    }
}