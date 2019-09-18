using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Domain.Enums {

    [ClassProperty(Name = "用户关系类型")]
    public enum UserTypeRelationType {

        /// <summary>
        ///     锁定用户
        /// </summary>
        [Display(Name = "锁定用户")]
        [LabelCssClass(BadgeColorCalss.Danger)]
        LockUser = 1,

        /// <summary>
        ///     配送
        /// </summary>
        [Display(Name = "配送")]
        [LabelCssClass(BadgeColorCalss.Danger)]
        Distribution = 2
    }
}