using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.Activitys.Domain.Enum
{
    /// <summary>
    ///     活动记录类型
    /// </summary>
    [ClassProperty(Name = "活动记录类型")]
    public enum ActivityRecordType
    {
        /// <summary>
        ///     行为，比如发起拼团，但是未必会产生了订单
        ///     OrderId=0
        /// </summary>
        Behavior = 1,

        /// <summary>
        ///     订单记录，和Order管理
        /// </summary>
        Order = 2
    }
}