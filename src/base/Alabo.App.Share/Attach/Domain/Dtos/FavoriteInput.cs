using Alabo.App.Open.Attach.Domain.Enums;

namespace Alabo.App.Open.Attach.Domain.Dtos {

    /// <summary>
    /// 添加收藏模型
    /// </summary>
    public class FavoriteInput {

        /// <summary>
        /// 用户ID
        /// </summary>
        public long LoginUserId { get; set; }

        /// <summary>
        /// 实体Id
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// 收藏类型
        /// </summary>
        public FavoriteType Type { get; set; } = FavoriteType.Product;
    }
}