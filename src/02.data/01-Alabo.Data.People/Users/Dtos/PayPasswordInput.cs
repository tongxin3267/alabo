using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Mapping;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Data.People.Users.Dtos {

    /// <summary>
    /// 支付密码修改传输层
    /// </summary>
    [ClassProperty(Name = "支付密码修改", Icon = "fa fa-puzzle-piece", Description = "支付密码修改",
        PostApi = "Api/User/ChangePayPassword", SuccessReturn = "pages/user/index")]
    public class PayPasswordInput : UIBase, IAutoForm//EntityDto
    {
        /// <summary>
        ///     用户Id不能为空
        /// </summary>
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long UserId { get; set; }

        /// <summary>
        ///     用户登录名不能为空
        /// </summary>
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string UserName { get; set; }

        /// <summary>
        ///     指定前台是修改登陆密码还是支付密码
        ///     Type=1: 表示修改登录密码
        ///     Type=2: 表示修改支付密码
        /// </summary>
        //[Required(ErrorMessage = "必须传入密码修改类型")]
        public PasswordType Type { get; set; }

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

        [Display(Name = "手机验证码")]
        public string MobileVerifiyCode { get; set; }

        //public IObjectCache ObjectCache => throw new System.NotImplementedException();

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var result = new AutoForm();
            result = ToAutoForm(new PasswordInput());
            result.AlertText = "【修改支付密码】为了更好的保护你的帐号安全，避免您和您的好友受到损失，建议您设置密码";

            result.ButtomHelpText = new List<string> {
                "建议确保登录密码与支付密码不同！",
                "建议密码采用字母和数字混合，并且不短于6位。",
            };
            return result;
        }

        //public TRepository Repository<TRepository>() where TRepository : IRepository
        //{
        //    throw new System.NotImplementedException();
        //}

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var parameter = (PayPasswordInput)model;
            var view = AutoMapping.SetValue<PasswordInput>(parameter);
            view.Type = PasswordType.PayPassword;
            var result = Resolve<IUserDetailService>().ChangePassword(view, true);
            if (result.Succeeded) {
                return ServiceResult.Success;
            }

            return ServiceResult.FailedWithMessage(result.ReturnMessage);
        }
    }
}