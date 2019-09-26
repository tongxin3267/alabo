using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum
{
    /// <summary>
    ///     支付状态
    /// </summary>
    [ClassProperty(Name = "支付状态")]
    public enum PayStatus
    {
        /// <summary>
        ///     等待处理
        /// </summary>
        [Display(Name = "等待处理")]
        [LabelCssClass("m-badge--metal")]
        WaiPay = 1,

        /// <summary>
        ///     处理成功
        /// </summary>
        [Display(Name = "处理成功")]
        [LabelCssClass("m-badge--success")]
        Success = 2,

        /// <summary>
        ///     处理失败
        /// </summary>
        [Display(Name = "处理失败")]
        [LabelCssClass("m-badge--metal")]
        Failured = 3
    }
}