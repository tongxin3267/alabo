using Alabo.Web.Mvc.Attributes;

namespace Alabo.Security.Identity.Results {

    /// <summary>
    ///     登录状态
    /// </summary>
    [ClassProperty(Name = "登陆状态")]
    public enum SignInState {

        /// <summary>
        ///     登录成功
        /// </summary>
        Succeeded,

        /// <summary>
        ///     失败
        /// </summary>
        Failed,

        /// <summary>
        ///     需要两阶段认证
        /// </summary>
        TwoFactor
    }
}