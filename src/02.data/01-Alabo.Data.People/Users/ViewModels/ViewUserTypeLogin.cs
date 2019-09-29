using Alabo.Validations;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Data.People.Users.ViewModels
{
    public class ViewUserTypeLogin
    {
        [Display(Name = "用户名/邮箱/手机")]
        [Required(ErrorMessage = "用户名/邮箱/手机不能为空")]
        [RegularExpression(
            @"([a-zA-z][a-zA-Z0-9_]{2,15})|(1[3|4|5|7|8]\d{9})|(([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2}))",
            ErrorMessage = "请输入用户名-邮箱或手机号")]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public long ParentId { get; set; }

        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [DataType(DataType.Password)]
        public string Verifycode { get; set; }
    }
}