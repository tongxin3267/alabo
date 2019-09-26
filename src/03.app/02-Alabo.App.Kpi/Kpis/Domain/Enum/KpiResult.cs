using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Kpis.Kpis.Domain.Enum {

    /// <summary>
    /// 考核结果
    /// </summary>
    [ClassProperty(Name = "考核结果")]
    public enum KpiResult {

        /// <summary>
        /// 保持
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Default)]
        [Display(Name = "保持")]
        [Field(Icon = "la la-check-circle-o")]
        NoChange = 1,

        /// <summary>
        /// 晋升
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "晋升")]
        [Field(Icon = "la la-check-circle-o")]
        Promote = 2,

        /// <summary>
        /// 降职
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "降职")]
        [Field(Icon = "la la-check-circle-o")]
        Demotion = 3,
    }
}