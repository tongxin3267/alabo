using Alabo.Domains.Entities.Extensions;

namespace Alabo.Industry.Shop.Activitys.Domain.Entities.Extension {

    /// <summary>
    ///     活动关联的商品
    /// </summary>
    public class ActivityProduct : EntityExtension {

        /// <summary>
        ///     活动ID
        /// </summary>
        public long ActivityId { get; set; }

        /// <summary>
        ///     商品ID
        /// </summary>
        public long ProductId { get; set; }
    }
}