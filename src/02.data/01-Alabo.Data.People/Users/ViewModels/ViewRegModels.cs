using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Randoms;
using Alabo.Regexs;
using Alabo.UI;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Random = Alabo.Randoms.Random;

namespace Alabo.App.Core.User.ViewModels {

    /// <summary>
    ///     Class ViewRegModels.
    /// </summary>
    [ClassProperty(Name = "用户注册", Icon = "fa fa-puzzle-piece", Description = "用户注册")]
    public class ViewRegModels : UIBase, IAutoForm {

        /// <summary>
        ///     Gets or sets the name of the 会员.
        /// </summary>
        [Display(Name = "用户名")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "用户名大小需要在4-15个字符之间")]
        [Remote("ExistsUserName", "Verify", HttpMethod = "POST", ErrorMessage = "用户名已经存在，请重新输入")]
        [Required(ErrorMessage = "请正确填写用户名")]
        [Field(ControlsType = ControlsType.TextBox, IsMain = true, EditShow = true, GroupTabId = 1, Width = "110", ListShow = true,
            SortOrder = 1)]
        [RegularExpression(RegularExpressionHelper.UserName, ErrorMessage = ErrorMessage.NotMatchFormat)]
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [DataType(DataType.Password)]
        [Field(ControlsType = ControlsType.TextBox, IsMain = true, EditShow = true, GroupTabId = 1, Width = "110", ListShow = true,
            SortOrder = 2)]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "6~16个字符，为了安全请使用字母加数字组合")]
        //[RegularExpression("^[@A-Za-z0-9!#$%^&*.~]{6,16}$",ErrorMessage = "6~16个字符，区分大小写,为了安全请使用大小写字母加数字组合")]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets the confirm password.
        /// </summary>
        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        [Field(ControlsType = ControlsType.TextBox, IsMain = true, EditShow = true, GroupTabId = 1, Width = "110", ListShow = true,
            SortOrder = 3)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Compare("Password", ErrorMessage = "密码与确认密码不相同")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        ///     Gets or sets the pay password.
        /// </summary>
        [Display(Name = "支付密码")]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string PayPassword { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        [Display(Name = "邮箱")]
        [Field(ControlsType = ControlsType.Email, IsMain = true, EditShow = true, GroupTabId = 1, Width = "110", ListShow = true,
            SortOrder = 4)]
        [Remote("verify_email", HttpMethod = "POST", ErrorMessage = ErrorMessage.IsUserd)]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [RegularExpression(RegularExpressionHelper.Email, ErrorMessage = ErrorMessage.NotMatchFormat)]
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the mobile.
        /// </summary>
        [Display(Name = "手机")]
        [Field(ControlsType = ControlsType.Phone, IsMain = true, EditShow = true, GroupTabId = 1, Width = "110", ListShow = true,
            SortOrder = 5)]
        [Remote("verify_mobile", HttpMethod = "POST", ErrorMessage = ErrorMessage.IsUserd)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [RegularExpression(RegularExpressionHelper.ChinaMobile, ErrorMessage = ErrorMessage.NotMatchFormat)]
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
        [Field(ControlsType = ControlsType.TextBox, IsMain = true, EditShow = true, GroupTabId = 1, Width = "110", ListShow = true,
            SortOrder = 6)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "姓名长度应为2~15个字符")]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the name of the record.
        /// </summary>
        [Display(Name = "推荐人")]
        [Field(ControlsType = ControlsType.TextBox, IsMain = true, EditShow = true, GroupTabId = 1, Width = "110", ListShow = true,
            SortOrder = 7)]
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
        public Guid GradeId { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.RadioButtonMultipl, IsMain = true, EditShow = true, GroupTabId = 1, Width = "110", ListShow = true,
            SortOrder = 8)]
        public Status Status { get; set; } = Status.Normal;

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var view = new ViewRegModels {
                GradeId = Resolve<IGradeService>().DefaultUserGrade.Id,
                Status = Status.Normal
            };
            var userConfig = Resolve<IAutoConfigService>().GetValue<UserConfig>();
            if (userConfig.IsFixedData) {
                view.Mobile = RandomHelper.Mobile();
                view.Email = RandomHelper.Email();
                var sex = RandomHelper.GetSex();
                view.Name = RandomHelper.Name(sex);
                view.UserName = RandomHelper.RandomString(1).ToUpper() + new Random().Next(100, 999);
            }

            return null;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            //var user = new Domain.Entities.User
            //{
            //    Map = new UserMap(),
            //    UserName = view.UserName.Trim(),
            //    Name = view.Name,
            //    Mobile = view.Mobile,
            //    Email = view.Email,
            //    Status = view.Status,
            //    GradeId = view.GradeId
            //};
            //if (!view.RecName.IsNullOrEmpty())
            //{
            //   // autoModel
            //    var parentUser = Resolve<IUserService>().GetSingle(view.RecName.Trim());
            //    if (parentUser == null)
            //    {
            //        ModelState.AddModelError("RecName", "推荐人不存在,请重新输入");
            //        return View(view);
            //    }

            //    if (parentUser.Status != Status.Normal)
            //    {
            //    }

            //    user.ParentId = parentUser.Id;
            //}

            //user.Detail = new UserDetail
            //{
            //    //Password = RandomHelper.PassWord(), //登录密码

            //    //  PayPassword = RandomHelper.PassWord() //支付密码
            //    Password = "111111".ToMd5HashString(), //登录密码

            //    PayPassword = "222222".ToMd5HashString() //支付密码
            //};
            //var result = await _userManager.RegisterAsync(user, false);
            //if (!result.Succeeded)
            //{
            //    ModelState.AddModelError(string.Empty, result.ToString());
            //    return await Task.FromResult(View(view));
            //}

            ////发送登录密码和，支付密码到手机
            //_messageManager.Keep();
            //Resolve<IUserService>().Log($"后台管理员成功添加会员，用户名{view.UserName},姓名{view.Name},手机{view.Mobile}，推荐人{view.RecName}"
            //  );
            //return SuccessMessageWithDefaultBack("会员添加成功",
            //    MessageLink.CreateRouteLink("编辑资料", new { controller = "AdminUser", action = "Edit", user.Id }),
            //    MessageLink.CreateRouteLink("会员管理", new { controller = "AdminUser", action = "Index" }),
            //    MessageLink.CreateRouteLink("首页", new { controller = "/Admin/Index" })
            //);
            throw new NotImplementedException();
        }
    }
}