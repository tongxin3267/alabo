using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Users.Enum {

    /// <summary>
    ///     Enum LastLoginIp
    /// </summary>
    [ClassProperty(Name = "认证状态")]
    public enum IdentityStatus {

        /// <summary>
        ///     未认证
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Default)]
        [Display(Name = "未认证")]
        IsNoPost = 1,

        /// <summary>
        ///     未审核
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Info)]
        [Display(Name = "未审核")]
        IsPost = 2,

        /// <summary>
        ///     已认证
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "已认证")]
        Succeed = 3,

        /// <summary>
        ///     认证失败
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "认证失败")]
        Failed = 4
    }
}