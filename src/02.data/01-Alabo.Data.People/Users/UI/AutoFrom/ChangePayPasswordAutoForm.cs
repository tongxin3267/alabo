using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Data.People.Users.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.UI.Design.AutoForms;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Validations;

namespace Alabo.Data.People.Users.UI.AutoFrom
{
    [ClassProperty(Name = "修改支付密码", Description = "根据原始登录密码，修改支付密码")]
    public class ChangePayPasswordAutoForm : UIBase, IAutoForm
    {
        /// <summary>
        ///     用户id
        /// </summary>
        [Display(Name = "用户id")]
        [Field(EditShow = false, ControlsType = ControlsType.Hidden)]
        public long UserId { get; set; }

        /// <summary>
        ///     原密码
        /// </summary>
        [Display(Name = "原支付密码", Description = "6位数字。")]
        [Field(EditShow = true, ControlsType = ControlsType.Password, GroupTabId = 1, SortOrder = 1)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        //[StringLength(6, MinimumLength = 6)]
        [HelpBlock("请您务必输入【原支付密码】")]
        [DataType(DataType.Password)]
        public string LastPassword { get; set; }

        /// <summary>
        ///     新密码
        /// </summary>
        [Display(Name = "新支付密码", Description = "6位数字。(否则不能支付)")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(6, MinimumLength = 6)]
        [Field(EditShow = true, ControlsType = ControlsType.Password, GroupTabId = 1, SortOrder = 2)]
        [HelpBlock("请您务必输入【新支付密码】(6位数字,否则不能支付）")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        ///     确认新密码
        /// </summary>
        [Display(Name = "确认新支付密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.Password, EditShow = true, GroupTabId = 1, SortOrder = 3,
            ValidType = ValidType.Customer)]
        [StringLength(6, MinimumLength = 6)]
        [HelpBlock("请您务必重复输入【新支付密码】")]
        //[Compare("Password", ErrorMessage = "密码与确认密码不相同")]
        public string ConfirmPassword { get; set; }

        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            var result = new AutoForm();
            result = ToAutoForm(new ChangePayPasswordAutoForm());
            result.AlertText = "【修改支付密码】为了更好的保护你的帐号安全，避免您受到损失，建议您修改默认支付密码";

            result.ButtomHelpText = new List<string>
            {
                "建议确保支付密码与登录密码不同！",
                "支付密码为6位数字,否则无法支付!"
            };
            return result;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            var parameter = (ChangePayPasswordAutoForm) model;

            var view = AutoMapping.SetValue<PasswordInput>(parameter);
            view.Type = PasswordType.PayPassword;
            var result = Resolve<IUserDetailService>().ChangePassword(view);
            if (result.Succeeded) return ServiceResult.Success;
            return ServiceResult.FailedWithMessage(result.ErrorMessages.Join());
        }
    }
}