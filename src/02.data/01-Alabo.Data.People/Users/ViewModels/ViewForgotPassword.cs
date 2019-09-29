using Alabo.Regexs;
using Alabo.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Data.People.Users.ViewModels
{
    /// <summary>
    ///     Class ViewForgotPassword.
    /// </summary>
    public class ViewForgotPassword
    {
        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "6~16个字符，为了安全请使用字母加数字组合")]
        //[RegularExpression("^[@A-Za-z0-9!#$%^&*.~]{6,16}$",ErrorMessage = "6~16个字符，区分大小写,为了安全请使用大小写字母加数字组合")]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        [Display(Name = "邮箱")]
        [Remote("verify_email", HttpMethod = "POST", ErrorMessage = ErrorMessage.IsUserd)]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [RegularExpression(RegularExpressionHelper.Email, ErrorMessage = ErrorMessage.NotMatchFormat)]
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the mobile.
        /// </summary>
        [Display(Name = "手机")]
        [Remote("verify_mobile", HttpMethod = "POST", ErrorMessage = ErrorMessage.IsUserd)]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [RegularExpression(RegularExpressionHelper.ChinaMobile, ErrorMessage = ErrorMessage.NotMatchFormat)]
        public string Mobile { get; set; }

        /// <summary>
        ///     Gets or sets the mobile verifiy code.
        /// </summary>
        [Display(Name = "手机验证码")]
        public string MobileVerifiyCode { get; set; }
    }
}