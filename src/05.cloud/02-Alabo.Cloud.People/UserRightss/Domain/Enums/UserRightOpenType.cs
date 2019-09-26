using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Cloud.People.UserRightss.Domain.Enums
{
    /// <summary>
    ///     会员权益开通方式
    /// </summary>
    [ClassProperty(Name = "会员权益开通方式")]
    public enum UserRightOpenType
    {
        /// <summary>
        ///     不能开通（针对部分禁用或未开放的用户等级）
        /// </summary>
        [Display(Name = "不能开通")] None = 0,

        /// <summary>
        ///     立即开通
        /// </summary>
        [Display(Name = "自己开通")] OpenSelf = 1,

        /// <summary>
        ///     使用名额帮朋友开通
        /// </summary>
        [Display(Name = "使用名额帮朋友开通")] OpenToOtherByRight = 2,

        /// <summary>
        ///     升级
        /// </summary>
        [Display(Name = "自身升级")] Upgrade = 3,

        /// <summary>
        ///     支付现金帮朋友开通
        /// </summary>
        [Display(Name = "支付现金帮朋友开通")] OpenToOtherByPay = 4,


        /// <summary>
        ///     管理员开通高等级商家
        ///     比如管理员开通准营销中心，营销中心
        /// </summary>
        [Display(Name = "管理员开通高等级商家")] AdminOpenHightGrade = 5
    }
}