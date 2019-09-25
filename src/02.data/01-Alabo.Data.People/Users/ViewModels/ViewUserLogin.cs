using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Validations;

namespace Alabo.App.Core.User.ViewModels {

    public class ViewUserLogin {

        [Display(Name = "用户名/手机")]
        [Required(ErrorMessage = "用户名/手机")]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public long ParentId { get; set; }

        [Display(Name = "验证码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string VerifyCode { get; set; }
    }
}