using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Data.People.Users.Dtos
{
    /// <summary>
    ///     登录模块
    /// </summary>
    [ClassProperty(Name = "会员登陆",  SuccessReturn = "pages/index")]
    public class LoginInput : EntityDto
    {
        /// <summary>
        ///     用户名和手机号
        /// </summary>
        [Display(Name = "用户名/手机")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, SortOrder = 1)]
        public string UserName { get; set; }

        /// <summary>
        ///     密码
        /// </summary>
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, SortOrder = 2)]
        public string Password { get; set; }

        /// <summary>
        ///     手机验证码
        /// </summary>
        [Display(Name = "验证码")]
        public string VerifyCode { get; set; }

        /// <summary>
        ///     微信登录的OpenId
        /// </summary>
        public string OpenId { get; set; }
    }
}