using Alabo.App.Core.User.Domain.Entities;
using Alabo.Domains.Entities.Extensions;
using Alabo.Users.Entities;

/// <summary>
/// The Extension namespace.
/// </summary>
namespace Alabo.App.Shop.Activitys.Domain.Entities.Extension {

    /// <summary>
    ///     活动记录
    /// </summary>
    public class ActivityRecordExtension : EntityExtension {

        /// <summary>
        ///     Gets or sets the order.
        /// </summary>
        public Order.Domain.Entities.Order Order { get; set; }

        /// <summary>
        ///     Gets or sets the 会员.
        /// </summary>
        public User User { get; set; }
    }
}