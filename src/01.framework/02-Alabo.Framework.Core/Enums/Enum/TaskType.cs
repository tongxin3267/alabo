using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Core.Enums.Enum
{
    /// <summary>
    ///     任务类型
    /// </summary>
    [ClassProperty(Name = "任务类型")]
    public enum TaskType
    {
        /// <summary>
        ///     缴费升级
        /// </summary>
        [Display(Name = "缴费升级")] [LabelCssClass(BadgeColorCalss.Success)]
        PayToUpgrade = 0,

        /// <summary>
        ///     推荐会员注册
        ///     也叫接点会员
        /// </summary>
        [Display(Name = "推荐二级会员注册")] [LabelCssClass(BadgeColorCalss.Success)] [Field(IsDefault = true)]
        RecommendedMember = 1,

        /// <summary>
        ///     二级推荐会员升级
        /// </summary>
        [Display(Name = "推荐二级会员注册")] [LabelCssClass(BadgeColorCalss.Success)] [Field(IsDefault = true)]
        RecommendedSecondMember = 2,

        /// <summary>
        ///     组织架构图下会员总数
        /// </summary>
        [Display(Name = "组织架构图下会员总数")] [LabelCssClass(BadgeColorCalss.Success)] [Field(IsDefault = true)]
        RecommendedAllMember = 3,

        /// <summary>
        ///     签到
        /// </summary>
        [Display(Name = "签到")] [LabelCssClass(BadgeColorCalss.Success)] [Field(IsDefault = true)]
        SignIn = 4,

        /// <summary>
        ///     会员注册时候，赠送升级点
        /// </summary>
        [Display(Name = "会员注册")] [LabelCssClass(BadgeColorCalss.Success)]
        UserReg = 5,

        /// <summary>
        ///     会员注册时候，赠送升级点
        /// </summary>
        [Display(Name = "实名认证")] [LabelCssClass(BadgeColorCalss.Success)]
        Identity = 6
    }
}