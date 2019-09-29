using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Tasks.Queues.Domain.Enums
{
    [ClassProperty(Name = "处理状态")]
    public enum QueueStatus
    {
        /// <summary>
        ///     待处理
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)] [Display(Name = "待处理")] [Field(Icon = "la la-lock")]
        Pending = 1,

        /// <summary>
        ///     处理中，循环中
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)] [Display(Name = "处理中")] [Field(Icon = "la la-lock")]
        Processing = 2,

        /// <summary>
        ///     执行完毕
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "已处理")] [Field(Icon = "la la-check-circle-o")]
        Handled = 3,

        /// <summary>
        ///     发生错误
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)] [Display(Name = "发生错误")] [Field(Icon = "la la-lock")]
        Error = 4,

        /// <summary>
        ///     已取消
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)] [Display(Name = "已取消")] [Field(Icon = "la la-lock")]
        Canceld = 5
    }
}