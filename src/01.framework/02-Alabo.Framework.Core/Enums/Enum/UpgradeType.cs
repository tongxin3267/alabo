using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ZKCloud.App.Core.Tasks.Domain.Enums {

    [ClassProperty(Name = "升级类型")]
    public enum UpgradeType {

        /// <summary>
        ///     后台修改
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "后台修改")]
        [Field(Icon = "la la-check-circle-o")]
        AdminChange = 1,

        /// <summary>
        ///     升级点触发
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "升级点触发")]
        [Field(Icon = "la la-lock")]
        UpgradePoint = 2,

        /// <summary>
        ///     团队等级触发
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "团队等级触发")]
        [Field(Icon = "fa fa-trash-o")]
        TeamUserGradeChange = 3,

        /// <summary>
        ///     团队等级触发
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "前台购买")]
        [Field(Icon = "fa fa-trash-o")]
        Buy = 4,
    }
}