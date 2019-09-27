using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Data.People.UserTypes.Enums
{
    /// <summary>
    ///     用户类型状态
    /// </summary>
    [ClassProperty(Name = "用户类型状态")]
    public enum UserTypeStatus
    {
        /// <summary>
        ///     待审核
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Warning)]
        [Display(Name = "待审核")]
        [Field(Icon = "la la-check-circle-o")]
        Pending = 1,

        /// <summary>
        ///     审核通过
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "审核通过")]
        [Field(Icon = "la la-check-circle-o")]
        Success = 2,

        /// <summary>
        ///     审核未通过
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "审核未通过")]
        [Field(Icon = "la la-check-circle-o")]
        Failured = 3,

        /// <summary>
        ///     锁定冻结
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "冻结")]
        [Field(Icon = "la la-lock")]
        Freeze = 4
    }
}