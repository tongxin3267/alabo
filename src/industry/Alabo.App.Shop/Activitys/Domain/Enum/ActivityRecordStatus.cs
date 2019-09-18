using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Activitys.Domain.Enum {

    /// <summary>
    ///     Enum ActivityRecordStatus
    ///     活动记录状态
    /// </summary>
    [ClassProperty(Name = "活动记录状态")]
    public enum ActivityRecordStatus {

        /// <summary>
        ///     进行中
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "进行中")]
        Processing = 1,

        /// <summary>
        ///     已支付
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "已支付")]
        IsPay = 2,

        /// <summary>
        ///     已成功
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "已成功")]
        Success = 5,

        /// <summary>
        ///     已失败
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "已失败")]
        Failed = 10
    }
}