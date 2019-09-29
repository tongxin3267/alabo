using Alabo.Domains.Entities.Extensions;
using Alabo.Users.Entities;

namespace Alabo.Industry.Shop.OrderActions.Domain.Entities.Extensions
{
    /// <summary>
    ///     订单操作记录扩展
    /// </summary>
    public class OrderActionExtension : EntityExtension
    {
        /// <summary>
        ///     操作时备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        ///     操作员信息
        /// </summary>
        public User AdminUser { get; set; }
    }
}