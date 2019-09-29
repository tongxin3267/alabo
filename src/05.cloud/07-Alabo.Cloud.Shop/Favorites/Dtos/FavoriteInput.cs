using Alabo.Cloud.Shop.Favorites.Domain.Enums;

namespace Alabo.Cloud.Shop.Favorites.Dtos
{
    /// <summary>
    ///     添加收藏模型
    /// </summary>
    public class FavoriteInput
    {
        /// <summary>
        ///     用户ID
        /// </summary>
        public long LoginUserId { get; set; }

        /// <summary>
        ///     实体Id
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        ///     收藏类型
        /// </summary>
        public FavoriteType Type { get; set; } = FavoriteType.Product;
    }
}