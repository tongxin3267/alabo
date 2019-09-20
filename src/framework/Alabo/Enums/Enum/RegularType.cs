using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Core.Enums.Enum
{
    /// <summary>
    ///     正则方式
    /// </summary>
    [ClassProperty(Name = "正则方式")]
    public enum RegularType
    {
        /// <summary>
        ///     数字类型
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Number")]
        Number = 0,

        /// <summary>
        ///     网址
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Url")]
        Url = 1,

        /// <summary>
        ///     手机号码
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Mobile")]
        Mobile = 2

        //[a-zA-Z]
    }
}