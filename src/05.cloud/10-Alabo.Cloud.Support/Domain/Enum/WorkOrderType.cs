using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Cloud.Support.Domain.Enum {

    /// <summary>
    ///     工单类型
    /// </summary>
    public enum WorkOrderType {

        /// <summary>
        ///     问题
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "问题")]
        Problem = 0,

        /// <summary>
        ///     事务
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "事务")]
        Transaction = 1,

        /// <summary>
        ///     故障
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "故障")]
        Fault = 2,

        /// <summary>
        ///     任务
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "任务")]
        Task = 3
    }
}