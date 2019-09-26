using System.ComponentModel.DataAnnotations;
using Alabo.Regexs;
using Alabo.Validations;

namespace Alabo.Data.People.Users.ViewModels
{
    /// <summary>
    ///     Class ViewChangMobile.
    /// </summary>
    public class ViewChangMobile
    {
        /// <summary>
        ///     Gets or sets the name of the 会员.
        /// </summary>
        [Display(Name = "用户名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the mobile.
        /// </summary>
        [Display(Name = "原手机号码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [RegularExpression(RegularExpressionHelper.ChinaMobile, ErrorMessage = ErrorMessage.NotMatchFormat)]
        public string Mobile { get; set; }

        /// <summary>
        ///     Gets or sets the new mobile.
        /// </summary>
        [Display(Name = "新手机号码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [RegularExpression(RegularExpressionHelper.ChinaMobile, ErrorMessage = ErrorMessage.NotMatchFormat)]
        public string NewMobile { get; set; }

        /// <summary>
        ///     Gets or sets the mobile verifiy code.
        /// </summary>
        [Display(Name = "手机验证码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string MobileVerifiyCode { get; set; }
    }
}