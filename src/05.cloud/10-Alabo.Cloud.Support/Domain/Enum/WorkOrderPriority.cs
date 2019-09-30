using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Cloud.Support.Domain.Enum
{
    /// <summary>
    ///     工单优先级
    /// </summary>
    [ClassProperty(Name = "工单优先级")]
    public enum WorkOrderPriority
    {
        /// <summary>
        ///     低
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "低")]
        Low = 0,

        /// <summary>
        ///     中
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "中")]
        Middle = 1,

        /// <summary>
        ///     高
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "高")]
        Hight = 2,

        /// <summary>
        ///     紧急
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "紧急")]
        Exigency = 3
    }
}