using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Offline.Order.ViewModels
{
    /// <summary>
    /// MerchantCartViewModel
    /// </summary>
    public class MerchantCartViewModel
    {
        /// <summary>
        /// 购物车id
        /// </summary>
        [Display(Name = "购物车id")]
        public string Id { get; set; }

        /// <summary>
        /// 缩略图的URL,通过主图生成
        /// </summary>
        [Display(Name = "缩略图")]
        public string ThumbnailUrl { get; set; }

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
        /// 商品总价
        /// </summary>
        [Display(Name = "商品总价")]
        public decimal ProductAmount { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Display(Name = "数量")]
        public long Count { get; set; }
    }
}