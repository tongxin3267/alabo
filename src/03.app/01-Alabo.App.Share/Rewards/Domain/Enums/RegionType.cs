using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Share.Rewards.Domain.Enums
{
    [ClassProperty(Name = "地址类型")]
    public enum RegionType
    {
        /// <summary>
        ///     订单收货地址
        /// </summary>
        [Display(Name = "区县理管理费")]
        [LabelCssClass(BadgeColorCalss.Success)]
        County = 1,

        /// <summary>
        ///     用户信息地址
        /// </summary>
        [Display(Name = "市代理管理费")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        City = 2,

        /// <summary>
        ///     用户信息地址
        /// </summary>
        [Display(Name = "省代理管理费")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        Province = 3
    }
}