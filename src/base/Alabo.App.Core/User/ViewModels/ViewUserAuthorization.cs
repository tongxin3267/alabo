using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Core.User.ViewModels {

    public class ViewUserAuthorization {

        [Display(Name = "用户名")]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string UserName { get; set; }

        [Display(Name = "用户名")]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string UserName2 { get; set; }

        [Display(Name = "密码")]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty), DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "手机号码")] public string Mobile { get; set; }

        public string RedirectUrl { get; set; }

        public string OpenId { get; set; }

        public bool IsBoundUser { get; set; } = true;
    }
}