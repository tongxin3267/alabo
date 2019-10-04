using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Tasks.Schedules.Domain.Enums {

    /// <summary>
    ///     分润结果类型
    ///     分润执行类型
    /// </summary>
    [ClassProperty(Name = "分润执行类型")]
    public enum FenRunResultType {

        /// <summary>
        ///     价格分润
        ///     如果是分润方式时候，会显示分润比例
        ///     分润方式
        ///     价格分润表示会自动产生佣金，在分润规则设置中会显示分润比例，支持多个分润比例， 大部分的分润维度都是价格分润，比如裂变分佣
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "价格分润")]
        Price = 1,

        /// <summary>
        ///     队列方式执行
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "队列方式执行")]
        Queue = 2,

        /// <summary>
        ///     更新业绩
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "更新业绩")]
        Sales = 3
    }
}