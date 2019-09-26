using Alabo.Web.Mvc.Attributes;

namespace Alabo.Cloud.Shop.Footprints.Domain.Enums {

    /// <summary>
    /// 足迹类型
    /// </summary>
    [ClassProperty(Name = "足迹类型")]
    public enum FootprintType {
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