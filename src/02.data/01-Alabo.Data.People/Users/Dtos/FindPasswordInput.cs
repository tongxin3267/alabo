using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Regexs;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.User.Domain.Dtos {

    /// <summary>
    ///     找回密码
    /// </summary>
    [ClassProperty(Name = "找回密码", Icon = "fa fa-puzzle-piece", Description = "找回密码", PostApi = "Api/User/FindPassword",
        SuccessReturn = "Api/User/GetLoginform")]
    public class FindPasswordInput : EntityDto {

        /// <summary>
        ///     用户名
        /// </summary>
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        //[Display(Name = "用户名")]
        //[Field(ControlsType = ControlsType.TextBox, Width = "80", SortOrder = 1)]
        public string UserName { get; set; }

        /// <summary>
        ///     新密码
        /// </summary>
        [Display(Name = "新密码", Description = "6-20个字符。")]
        [Field(ControlsType = ControlsType.Password, Width = "80", SortOrder = 4)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "密码长度不能少于6位")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        ///     确认新密码
        /// </summary>
        [Display(Name = "确认新密码")]
        [DataType(DataType.Password)]
        [Field(ControlsType = ControlsType.Password, Width = "80", SortOrder = 5)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Compare("Password", ErrorMessage = "密码与确认密码不相同")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "邮箱")]
        [Remote("verify_email", HttpMethod = "POST", ErrorMessage = ErrorMessage.IsUserd)]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        //[Field(ControlsType = ControlsType.TextBox, Width = "80", SortOrder = 1)]
        [RegularExpression(RegularExpressionHelper.Email, ErrorMessage = ErrorMessage.NotMatchFormat)]
        public string Email { get; set; }

        /// <summary>
        ///     手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        [Field(ControlsType = ControlsType.TextBox, Width = "80", SortOrder = 2)]
        [Remote("verify_mobile", HttpMethod = "POST", ErrorMessage = ErrorMessage.IsUserd)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [RegularExpression(RegularExpressionHelper.ChinaMobile, ErrorMessage = ErrorMessage.NotMatchFormat)]
        public string Mobile { get; set; }

        /// <summary>
        ///     手机验证码
        /// </summary>
        [Display(Name = "手机验证码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [MinLength(6, ErrorMessage = "验证码格式不正确")]
        [MaxLength(6, ErrorMessage = "验证码格式不正确")]
        [Field(ControlsType = ControlsType.PhoneVerifiy, GroupTabId = 2, Mark = "Mobile", EditShow = true)]
        public string MobileVerifiyCode { get; set; }
    }
}