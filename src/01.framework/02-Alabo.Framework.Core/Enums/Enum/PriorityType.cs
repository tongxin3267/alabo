using Alabo.Web.Mvc.Attributes;

namespace Alabo.Core.Enums.Enum
{
    [ClassProperty(Name = "优先级")]
    public enum PriorityType : byte
    {
        /// <summary>
        ///     允许优先
        /// </summary>
        AllowedFirst = 1,

        /// <summary>
        ///     拒绝优先
        /// </summary>
        RefusedFirst,

        /// <summary>
        ///     最大值优先
        /// </summary>
        MaximumFirst,

        /// <summary>
        ///     最小值优先
        /// </summary>
        MinimumFirst
    }
}