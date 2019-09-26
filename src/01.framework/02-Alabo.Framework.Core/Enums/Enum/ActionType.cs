using Alabo.Web.Mvc.Attributes;

namespace Alabo.Core.Enums.Enum {

    [ClassProperty(Name = "用户操作类型")]
    public enum UserActionType {

        /// <summary>
        ///     商品收藏
        /// </summary>
        ProductFavorite = 1,

        /// <summary>
        ///     购物车
        /// </summary>
        ProductCart = 2,

        /// <summary>
        ///     店铺收藏
        /// </summary>
        StoreFavorite = 3,

        /// <summary>
        ///     用户收藏
        /// </summary>
        UserFavorite = 4,

        /// <summary>
        ///     足迹,商品浏览历史
        /// </summary>
        Footprint = 5
    }
}