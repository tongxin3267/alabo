using System.ComponentModel.DataAnnotations;

namespace Alabo.Cloud.People.Visitors.Domain.Enums
{
    public enum VisitorType
    {
        /// <summary>
        ///     微信公众号
        /// </summary>
        [Display(Name = "微信公众号")] WeChat = 1,

        /// <summary>
        ///     微信小程序
        /// </summary>
        [Display(Name = "微信小程序")] WeChatLite = 2
    }
}