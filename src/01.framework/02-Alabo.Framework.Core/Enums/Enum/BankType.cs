using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Core.Enums.Enum
{
    /// <summary>
    ///     银行类型
    /// </summary>
    [ClassProperty(Name = "银行类型")]
    public enum BankType
    {
        /// <summary>
        ///     中国银行
        /// </summary>
        [Display(Name = "中国银行")] [LabelCssClass(BadgeColorCalss.Danger)]
        BankOfChina = 1,

        /// <summary>
        ///     工商银行
        /// </summary>
        [Display(Name = "工商银行")] [LabelCssClass(BadgeColorCalss.Danger)]
        Icbc = 2,

        /// <summary>
        ///     农业银行
        /// </summary>
        [Display(Name = "农业银行")] [LabelCssClass(BadgeColorCalss.Danger)]
        AgriculturalBank = 3,

        /// <summary>
        ///     建设银行
        /// </summary>
        [Display(Name = "建设银行")] [LabelCssClass(BadgeColorCalss.Danger)]
        Ccb = 4,

        /// <summary>
        ///     交通银行
        /// </summary>
        [Display(Name = "交通银行")] [LabelCssClass(BadgeColorCalss.Danger)]
        BankOfCommunications = 5,

        /// <summary>
        ///     招商银行
        /// </summary>
        [Display(Name = "民生银行")] [LabelCssClass(BadgeColorCalss.Danger)]
        Cmbc = 6,

        /// <summary>
        ///     其他银行
        /// </summary>
        [Display(Name = "其他银行")] [LabelCssClass(BadgeColorCalss.Danger)]
        OtherBank = 999
    }
}