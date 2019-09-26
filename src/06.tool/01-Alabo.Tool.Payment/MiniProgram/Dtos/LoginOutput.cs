namespace Alabo.Tool.Payment.MiniProgram.Dtos {

    /// <summary>
    ///     小程序登录以后返回的状态
    /// </summary>
    public class LoginOutput {

        /// <summary>
        ///     用户是否注册
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is reg; otherwise, <c>false</c>.
        /// </value>
        public bool IsReg { get; set; } = false;

        /// <summary>
        ///     微信登录后返回的信息
        /// </summary>
        /// <value>The session.</value>
        public SessionOutput Session { get; set; }

        /// <summary>
        ///     登录用户
        /// </summary>
        /// <value>The user.</value>
     //   public UserOutput User { get; set; }
    }
}