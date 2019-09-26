using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Core.Enums.Enum
{
    /// <summary>
    ///     会员状态
    ///     激活状态
    /// </summary>
    [ClassProperty(Name = "会员激活状态")]
    public enum ActivateState
    {
        /// <summary>
        ///     未激活
        /// </summary>
        [Display(Name = "未激活")] [LabelCssClass(BadgeColorCalss.Info)]
        NotActivated = 1,

        /// <summary>
        ///     正常
        /// </summary>
        [Display(Name = "正常")] [LabelCssClass(BadgeColorCalss.Success)]
        Normal = 0,

        /// <summary>
        ///     已冻结
        /// </summary>
        [Display(Name = "已冻结")] [LabelCssClass(BadgeColorCalss.Danger)]
        Locked = 2
    }
}