using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Core.Enums.Enum
{
    /// <summary>
    ///     终端类型
    /// </summary>
    [ClassProperty(Name = "终端类型")]
    public enum ClientType
    {
        /// <summary>
        ///     电脑PC端
        /// </summary>
        [Display(Name = "电脑PC端")] PcWeb = 1,

        /// <summary>
        ///     手机Wap H5 端
        /// </summary>
        [Display(Name = "手机H5")] WapH5 = 2,

        /// <summary>
        ///     苹果App
        /// </summary>
        [Display(Name = "苹果App")] IOS = 3,

        /// <summary>
        ///     安卓App
        /// </summary>
        [Display(Name = "安卓App")] Android = 4,

        /// <summary>
        ///     微信公众号
        /// </summary>
        [Display(Name = "微信公众号")] WeChat = 5,

        /// <summary>
        ///     微信小程序
        /// </summary>
        [Display(Name = "微信小程序")] WeChatLite = 6
    }
}