using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Core.WebUis.Design.AutoForms;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.User.UI.AutoFrom {

    [ClassProperty(Name = "修改登录密码", Description = "根据原始登录密码，修改登录密码")]
    public class ChangeLoginPasswordAutoForm : UIBase, IAutoForm {

        /// <summary>
        /// 用户id
        /// </summary>
        [Display(Name = "用户id")]
        [Field(EditShow = false, ControlsType = ControlsType.Hidden)]
        public long UserId { get; set; }

        /// <summary>
        ///     原密码
        /// </summary>
        [Display(Name = "原密码", Description = "6-20个字符。")]
        [Field(EditShow = true, ControlsType = ControlsType.Password, GroupTabId = 1, SortOrder = 1)]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请您务必输入【原密码】")]
        [DataType(DataType.Password)]
        public string LastPassword { get; set; }

        /// <summary>
        ///     新密码
        /// </summary>
        [Display(Name = "新密码", Description = "6-20个字符。")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(EditShow = true, ControlsType = ControlsType.Password, GroupTabId = 1, SortOrder = 2)]
        [StringLength(255, MinimumLength = 6, ErrorMessage = ErrorMessage.StringLength)]
        [HelpBlock("请您务必输入【新密码】(6-16位，区分大小写，使用字母、数字、特殊字母）")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        ///     确认新密码
        /// </summary>
        [Display(Name = "确认新密码")]
        [DataType(DataType.Password)]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.Password, EditShow = true, GroupTabId = 1,
            SortOrder = 3)]
        [HelpBlock("请您务必重复输入【新密码】")]
        //[Compare("Password", ErrorMessage = "密码与确认密码不相同")]
        public string ConfirmPassword { get; set; }

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var result = new AutoForm();
            result = ToAutoForm(new ChangeLoginPasswordAutoForm());
            result.AlertText = "【修改登录密码】为了更好的保护你的帐号安全，避免您受到损失，建议您修改默认密码";

            result.ButtomHelpText = new List<string> {
                "建议确保登录密码与支付密码不同！",
                "建议密码采用字母和数字混合，并且不短于6位。",
            };
            return result;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var parameter = (ChangeLoginPasswordAutoForm)model;

            var view = AutoMapping.SetValue<PasswordInput>(parameter);
            view.Type = PasswordType.LoginPassword;
            var result = Resolve<IUserDetailService>().ChangePassword(view, true);
            if (result.Succeeded) {
                return ServiceResult.Success;
            }

            return ServiceResult.FailedWithMessage(result.ErrorMessages.Join());
        }
    }
}