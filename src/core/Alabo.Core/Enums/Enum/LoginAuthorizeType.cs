using Alabo.Web.Mvc.Attributes;

namespace Alabo.Core.Enums.Enum
{
    /// <summary>
    ///     登录激活方式
    /// </summary>
    [ClassProperty(Name = "登陆激活方式")]
    public enum LoginAuthorizeType
    {
        /// <summary>
        ///     通过登录激活
        /// </summary>
        Login,

        /// <summary>
        ///     通过微信激活
        /// </summary>
        WeChatAuthorize
    }
}