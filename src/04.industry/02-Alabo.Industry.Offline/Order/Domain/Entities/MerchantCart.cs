using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Offline.Order.Domain.Entities
{
    /// <summary>
    /// merchant cart
    /// </summary>
    [Table("Offline_MerchantCart")]
    [BsonIgnoreExtraElements]
    [ClassProperty(Name = "商品", PageType = ViewPageType.List)]
    public class MerchantCart : AggregateMongodbUserRoot<MerchantCart>
    {
        /// <summary>
        /// 店铺id
        /// </summary>
        [Display(Name = "店铺id")]
        public string MerchantStoreId { get; set; }

        /// <summary>
        /// 商品id
        /// </summary>
        [Display(Name = "商品id")]
        public string MerchantProductId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [Display(Name = "商品名称")]
        public string ProductName { get; set; }

        /// <summary>
        /// SkuId
        /// </summary>
        [Display(Name = "SkuId")]
        public string SkuId { get; set; }

        /// <summary>
        /// SkuName
        /// </summary>
        [Display(Name = "SkuName")]
        public string SkuName { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        [Display(Name = "销售价")]
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Display(Name = "数量")]
        public long Count { get; set; }

        /// <summary>
        /// 状态
        /// 购物车不删除
        /// </summary>
        public Status Status { get; set; } = Status.Normal;
    }
}