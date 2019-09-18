using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Product.Domain.Enums {

    /// <summary>
    ///     商品排序
    /// </summary>
    [ClassProperty(Name = "商品排序")]
    public enum ProductSortOrder {

        /// <summary>
        ///     默认排序，综合排序
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "默认排序")]
        Defualt = 0,

        /// <summary>
        ///     价格
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "价格")]
        Price = 1,

        /// <summary>
        ///     商品添加时间
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "更新时间")]
        ModifiedTime = 2,

        /// <summary>
        ///     查看次数
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "查看次数")]
        ViewCount = 3,

        /// <summary>
        ///     销售数量
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "销售数量")]
        SoldCount = 4,

        /// <summary>
        ///     喜欢数量
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "喜欢数量")]
        LikeCount = 5,

        /// <summary>
        ///     收藏数量
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "收藏数量")]
        FavoriteCount = 6
    }
}