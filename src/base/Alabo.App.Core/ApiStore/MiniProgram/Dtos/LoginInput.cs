using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Query.Dto;

/// <summary>
/// </summary>
namespace Alabo.App.Core.ApiStore.MiniProgram.Dtos {

    /// <summary>
    ///     小程序登录Input
    /// </summary>
    public class LoginInput : EntityDto {

        /// <summary>
        ///     登录时获取的code
        /// </summary>
        /// <value>The js code.</value>
        [Required(ErrorMessage = "登录时获取的code,不能为空")]
        public string JsCode { get; set; }

        /// <summary>
        ///     登录时获取的 code
        /// </summary>
        /// <value>The type of the grant.</value>
        public string GrantType { get; set; } = "authorization_code";

        /// <summary>
        ///     手机号码
        /// </summary>
        /// <value>The mobile.</value>
        public string Mobile { get; set; }
    }
}