using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Core.User.ViewModels;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Datas.Queries.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Regexs;
using Alabo.UI;
using Alabo.Users.Entities;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.User.UI.AutoFrom {

    [Display(Name = "用户注册")]
    [ClassProperty(Name = "用户注册", Description = "用户注册")]
    public class RegUserAutoForm : UIBase, IAutoForm {
        #region

        /// <summary>
        ///     Gets or sets the name of the 会员.
        /// </summary>
        [Display(Name = "用户名")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "用户名大小需要在4-15个字符之间")]
        [Required(ErrorMessage = "请正确填写用户名")]
        [RegularExpression(RegularExpressionHelper.UserName, ErrorMessage = ErrorMessage.NotMatchFormat)]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true, IsMain = true, Operator = Operator.Contains,
            ListShow = true, EditShow = true, GroupTabId = 1, SortOrder = 2)]
        [HelpBlock("请填写用户姓名")]
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        [DataType(DataType.Password)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "6~16个字符，为了安全请使用字母加数字组合")]
        [Field(ControlsType = ControlsType.Password, IsMain = true, Operator = Operator.Contains,
              EditShow = true, GroupTabId = 1, SortOrder = 8)]
        //[RegularExpression("^[@A-Za-z0-9!#$%^&*.~]{6,16}$",ErrorMessage = "6~16个字符，区分大小写,为了安全请使用大小写字母加数字组合")]
        [Display(Name = "密码")]
        [HelpBlock("为了安全请使用字母加数字组合")]
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets the confirm password.
        /// </summary>
        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "6~16个字符，为了安全请使用字母加数字组合")]
        [Compare("Password", ErrorMessage = "密码与确认密码不相同")]
        [Field(ControlsType = ControlsType.Password, IsMain = true, Operator = Operator.Contains,
              EditShow = true, GroupTabId = 1, SortOrder = 8)]
        public string ConfirmPassword { get; set; }

        /// <summary>
        ///     Gets or sets the pay password.
        /// </summary>
        [Display(Name = "支付密码")]
        [Field(ControlsType = ControlsType.Password, IsMain = true, Operator = Operator.Contains,
              EditShow = true, GroupTabId = 1, SortOrder = 9)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(6, MinimumLength = 6)]
        [HelpBlock("支付密码为6位数的数字,用于支付")]
        public string PayPassword { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        [Display(Name = "邮箱")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true, IsMain = true, Operator = Operator.Contains,
            ListShow = true, EditShow = true, GroupTabId = 1, SortOrder = 6)]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [RegularExpression(RegularExpressionHelper.Email, ErrorMessage = ErrorMessage.NotMatchFormat)]
        [HelpBlock("请填写正确的邮箱,否则无法注册")]
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the mobile.
        /// </summary>
        [Display(Name = "手机")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [RegularExpression(RegularExpressionHelper.ChinaMobile, ErrorMessage = ErrorMessage.NotMatchFormat)]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true, IsMain = true, Operator = Operator.Contains,
            ListShow = true, EditShow = true, GroupTabId = 1, SortOrder = 4)]
        [HelpBlock("请填写正确的手机号码,新用户的一些重要信息将通过该手机号码发送")]
        public string Mobile { get; set; }

        /// <summary>
        ///     Gets or sets the mobile verifiy code.
        /// </summary>
        [Display(Name = "手机验证码")]
        public string MobileVerifiyCode { get; set; }

        /// <summary>
        ///     Gets or sets the verify code.
        /// </summary>
        [Display(Name = "验证码")]
        public string VerifyCode { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        [Display(Name = "姓名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "姓名长度应为2~15个字符")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true, IsMain = true, Operator = Operator.Contains,
            ListShow = true, EditShow = true, GroupTabId = 1, SortOrder = 3)]
        [HelpBlock("请填写用户的姓名")]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the name of the record.
        /// </summary>
        [Display(Name = "推荐人")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true, IsMain = true, Operator = Operator.Contains,
            ListShow = true, GroupTabId = 1, SortOrder = 5)]
        [HelpBlock("请输入该用户的推荐人")]
        public string RecName { get; set; }

        /// <summary>
        ///     Gets or sets the verifycode.
        /// </summary>
        public string verifycode { get; set; }

        /// <summary>
        ///     Gets or sets the parent identifier.
        /// </summary>
        public long ParentId { get; set; } = 0;

        /// <summary>
        ///     Gets or sets the grade identifier.
        /// </summary>
        [Display(Name = "会员等级")]
        [Field(ControlsType = ControlsType.RadioButton, ApiDataSource = "Api/Common/GetKeyValuesByAutoConfig?type=UserGradeConfig", EditShow = true, GroupTabId = 1, Width = "110", ListShow = true,
         SortOrder = 1)]
        [HelpBlock("请选择用户的会员等级")]
        public Guid GradeId { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.RadioButton, DataSource = "Alabo.Domains.Enums.Status", IsMain = true,
            ListShow = true, EditShow = true, GroupTabId = 1, SortOrder = 7)]
        [HelpBlock("正常:用户可正常登陆,分润,下单等操作.冻结和删除不能进行操作.删除是软删除,不是真正的删除")]
        public Status Status { get; set; } = Status.Normal;

        #endregion

        /// <summary>

        /// 获取视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var res = Ioc.Resolve<IUserService>().GetSingle(s => s.Id == id.ConvertToLong(-1));
            if (res != null) {
                return ToAutoForm(res);
            }

            return ToAutoForm(new RegUserAutoForm());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var view = (RegUserAutoForm)model;

            if (view.Password.Length < 6) {
                return ServiceResult.FailedWithMessage("请您务必输入【密码】大于6位！");
            }

            if (view.ConfirmPassword.Length < 6) {
                return ServiceResult.FailedWithMessage("请您务必输入【支付密码】大于6位！");
            }

            if (view.PayPassword.Length < 6) {
                return ServiceResult.FailedWithMessage("请您务必输入【支付密码】6位,否则不能支付！");
            }

            var user = new Users.Entities.User {
                Map = new UserMap(),
                UserName = view.UserName.Trim(),
                Name = view.Name,
                Mobile = view.Mobile,
                Email = view.Email,
                Status = view.Status,
                GradeId = view.GradeId
            };
            if (!view.RecName.IsNullOrEmpty()) {
                var parentUser = Ioc.Resolve<IUserService>().GetSingle(s => s.Mobile == view.RecName.Trim() || s.UserName == view.RecName.Trim());
                if (parentUser == null) {
                    return ServiceResult.FailedMessage("推荐人不存在,请重新输入");
                }
                if (parentUser.Status != Status.Normal) {
                    return ServiceResult.FailedMessage("推荐人状态不正常，不能作为推荐人");
                }
                user.ParentId = parentUser.Id;
            }

            user.Detail = new UserDetail {
                //Password = RandomHelper.PassWord(), //登录密码

                //  PayPassword = RandomHelper.PassWord() //支付密码
                Password = view.Password, //登录密码

                PayPassword = view.PayPassword.ToMd5HashString() //支付密码
            };
            //处理异步
            var result = Task.Run(async () => {
                //  return await Ioc.Resolve<UserManager>().RegisterAsync(user, false);
            });

            //if (result.Result.Succeeded)
            //{
            //    //Resolve<IUserService>().Log($"后台管理员成功添加会员，用户名{view.UserName},姓名{view.Name},手机{view.Mobile}，推荐人{view.RecName}");
            //    return ServiceResult.Success;

            //return ServiceResult.FailedMessage($"注册失败!{result.Result.ErrorMessages.Join()}");
            return null;
        }
    }
}