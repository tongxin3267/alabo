using Alabo.Domains.Entities.Extensions;
using Alabo.Industry.Shop.Orders.Domain.Entities;
using Alabo.Users.Entities;

/// <summary>
/// The Extension namespace.
/// </summary>
namespace Alabo.Industry.Shop.Activitys.Domain.Entities.Extension
{
    /// <summary>
    ///     活动记录
    /// </summary>
    public class ActivityRecordExtension : EntityExtension
    {
        /// <summary>
        ///     Gets or sets the order.
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        ///     Gets or sets the 会员.
        /// </summary>
        public User User { get; set; }
    }
}