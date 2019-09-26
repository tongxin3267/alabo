using Alabo.Web.Mvc.Attributes;

namespace Alabo.Cloud.Shop.Favorites.Domain.Enums {

    /// <summary>
    /// 收藏类型
    /// </summary>
    [ClassProperty(Name = "收藏类型")]
    public enum FavoriteType {

        /// <summary>
        /// 商品收藏
        /// </summary>
        Product = 1,

        /// <summary>
        /// 店铺收藏
        /// </summary>
        Store = 2,

        /// <summary>
        /// 用户收藏
        /// </summary>
        User = 3,

        /// <summary>
        /// 文章收藏
        /// </summary>
        Article = 4,
    }
}