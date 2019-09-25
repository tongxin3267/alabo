using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Core.Extensions;
using Alabo.Core.Regex;
using Alabo.Datas.Queries.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Randoms;
using Alabo.UI;
using Alabo.UI.AutoForms;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.User.UI.AutoFrom {

    [Display(Name = "用户注册")]
    [ClassProperty(Name = "用户注册", Description = "用户注册")]
    public class AdminUserAddAutoForm : UIBase, IAutoForm {
        #region

        /// <summary>
        ///     Gets or sets the grade identifier.
        /// </summary>
        [Display(Name = "会员等级")]
        [Field(ControlsType = ControlsType.RadioButton, ApiDataSource = "Api/Common/GetKeyValuesByAutoConfig?type=UserGradeConfig", EditShow = true, GroupTabId = 1, Width = "110", ListShow = true,
         SortOrder = 1)]
        [HelpBlock("请选择用户的会员等级")]
        public Guid GradeId { get; set; }

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
        ///     Gets or sets the name of the record.
        /// </summary>
        [Display(Name = "推荐人")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true, IsMain = true, Operator = Operator.Contains,
            ListShow = true, GroupTabId = 1, SortOrder = 5)]
        [HelpBlock("请输入该用户的推荐人")]
        public string RecName { get; set; }

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
            var view = new AdminUserAddAutoForm {
                GradeId = Resolve<IGradeService>().DefaultUserGrade.Id,
                Status = Status.Normal
            };
            var userConfig = Resolve<IAutoConfigService>().GetValue<UserConfig>();
            if (userConfig.IsFixedData) {
                view.Mobile = RandomHelper.Mobile();
                view.Email = RandomHelper.Email();
                var sex = RandomHelper.GetSex();
                view.Name = RandomHelper.Name(sex);
                view.UserName = RandomHelper.RandomString(1).ToUpper() + new System.Random().Next(100, 999);
            }

            return ToAutoForm(view);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var view = (AdminUserAddAutoForm)model;

            var user = new Domain.Entities.User {
                Map = new UserMap(),
                UserName = view.UserName.Trim(),
                Name = view.Name,
                Mobile = view.Mobile,
                Email = view.Email,
                Status = view.Status,
                GradeId = view.GradeId
            };
            if (!view.RecName.IsNullOrEmpty()) {
                var parentUser = Resolve<IUserService>().GetSingle(view.RecName.Trim());
                if (parentUser == null) {
                    return ServiceResult.FailedMessage("推荐人不存在,请重新输入");
                }

                if (parentUser.Status != Status.Normal) {
                    return ServiceResult.FailedMessage("推荐人状态不正常，不能作为推荐人");
                }

                user.ParentId = parentUser.Id;
            }

            user.Detail = new UserDetail {
                Password = "111111".ToMd5HashString(), //登录密码
                PayPassword = "222222".ToMd5HashString() //支付密码
            };

            //var result = Ioc.Resolve<IUserBaseService>().Reg(user, false);

            //if (result.Result.Succeeded)
            //{
            //    return ServiceResult.Success;
            //}
            //return ServiceResult.FailedMessage($"添加失败!{result.Result.ErrorMessages.Join()}");
            return ServiceResult.Success;
        }
    }
}