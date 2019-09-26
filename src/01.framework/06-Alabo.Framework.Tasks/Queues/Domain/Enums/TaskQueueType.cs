using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Tasks.Queues.Domain.Enums {

    [ClassProperty(Name = "任务执行周期")]
    public enum TaskQueueType {

        /// <summary>
        ///     单次执行
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Default)]
        [Display(Name = "单次执行")]
        [Field(Icon = "la la-check-circle-o")]
        Once = 1,

        /// <summary>
        ///     每日执行一次（以设定的执行时间为准）
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Default)]
        [Display(Name = "每日执行一次")]
        [Field(Icon = "la la-check-circle-o")]
        Days,

        /// <summary>
        ///     每周执行一次（以设定的日期所在星期以及对应时间为准）
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Default)]
        [Display(Name = "每周执行一次")]
        [Field(Icon = "la la-check-circle-o")]
        Weeks,

        /// <summary>
        ///     每月执行一次（以设定日期所在月日期，如超过，则月最后一天执行）
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Default)]
        [Display(Name = "每月执行一次")]
        [Field(Icon = "la la-check-circle-o")]
        Months,

        /// <summary>
        ///     每年执行一次（以设定日期和时间为标准，如遇到2.29日，则在非闰年的2.28日执行）
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Default)]
        [Display(Name = "每年执行一次")]
        [Field(Icon = "la la-check-circle-o")]
        Years,
    }
}