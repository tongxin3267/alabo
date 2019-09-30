using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Cloud.Support.Domain.Enum
{
    /// <summary>
    ///     工单状态
    /// </summary>
    [ClassProperty(Name = "工单状态")]
    public enum WorkOrderState
    {
        /// <summary>
        ///     受理中
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "等待回复")]
        Accept = 0,

        /// <summary>
        ///     已解决
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "已解决")]
        Resolved = 1
    }
}