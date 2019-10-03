using Alabo.Cloud.People.UserTree.Domain.Service;
using Alabo.Data.People.Users.Domain.Repositories;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Maps;
using Alabo.UI;
using Alabo.UI.Design.AutoForms;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Cloud.People.UserTree.Domain.UI
{
    /// <summary>
    ///     修改推荐关系
    /// </summary>
    [ClassProperty(Name = "修改推荐关系")]
    public class UpdateParentForm : UIBase, IAutoForm
    {
        /// <summary>
        ///     当前登录id
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden)]
        public long UserId { get; set; }

        /// <summary>
        ///     要更改的用户名
        /// </summary>
        [Display(Name = "要更改的用户名")]
        [Required(ErrorMessage = "请输入要更改的用户名")]
        [HelpBlock("修改推荐人，具有一定的风险，修改后需确保组织架构图正确，否则会影响分润的正确性")]
        [Field(EditShow = true, SortOrder = 1, ControlsType = ControlsType.TextBox, ValidType = ValidType.UserName)]
        public string UserName { get; set; }

        /// <summary>
        ///     上级用户名(推荐人)
        /// </summary>
        [Display(Name = "上级用户名(推荐人)")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("新推荐人用户名，用户状态需正常")]
        [Field(EditShow = true, SortOrder = 2, ControlsType = ControlsType.TextBox, ValidType = ValidType.UserName)]
        public string ParentUserName { get; set; }

        /// <summary>
        ///     支付密码
        /// </summary>
        [Display(Name = "支付密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(EditShow = true, SortOrder = 3, ControlsType = ControlsType.Password)]
        [HelpBlock("输入当前用户的支付密码")]
        [DataType(DataType.Password)]
        public string PayPassword { get; set; }

        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            var view = new UpdateParentForm();
            return ToAutoForm(view);
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            var view = model.MapTo<UpdateParentForm>();
            if (autoModel != null) {
                view.UserId = autoModel.BasicUser.Id;
            }

            var user = Resolve<IUserService>().GetSingle(r => r.UserName == view.UserName);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户名不存在");
            }

            var parentUser = Resolve<IUserService>().GetSingle(view.ParentUserName);
            if (parentUser == null) {
                return ServiceResult.FailedWithMessage("推荐人不存在");
            }

            if (parentUser.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("推荐人状态不正常");
            }

            var currentUserPay = Resolve<IUserDetailService>().GetSingle(r => r.Id == view.UserId);
            if (view.PayPassword.ToMd5HashString() != currentUserPay.PayPassword) {
                return ServiceResult.FailedWithMessage("支付密码错误！");
            }

            var result = ServiceResult.Success;
            var context = Ioc.Resolve<IUserRepository>().RepositoryContext;
            try
            {
                context.BeginTransaction();
                user.ParentId = parentUser.Id;
                Resolve<IUserService>().Update(user);

                Resolve<ITreeUpdateService>().ParentMapTaskQueue();
                context.SaveChanges();
                context.CommitTransaction();
            }
            catch (Exception ex)
            {
                context.RollbackTransaction();
                result = ServiceResult.FailedWithMessage(ex.Message);
            }
            finally
            {
                context.DisposeTransaction();
            }

            if (!result.Succeeded) {
                return ServiceResult.FailedWithMessage("推荐人修改失败");
            }

            return result;
        }
    }
}