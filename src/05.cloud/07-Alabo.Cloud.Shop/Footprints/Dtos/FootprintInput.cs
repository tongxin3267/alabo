using Alabo.Cloud.Shop.Footprints.Domain.Enums;

namespace Alabo.Cloud.Shop.Footprints.Dtos
{
    /// <summary>
    ///     足迹模型
    /// </summary>
    public class FootprintInput
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
        public FootprintType Type { get; set; }
    }
}