using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.Cloud.People.UserTree.Domain.Configs;
using Alabo.Cloud.People.UserTree.Domain.Service;
using Alabo.Data.People.Users.Domain.Repositories;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Maps;
using Alabo.UI;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Validations;

namespace Alabo.Cloud.People.UserTree.Domain.UI
{
    /// <summary>
    ///     转移团队关系
    /// </summary>
    [ClassProperty(Name = "转移团队关系", Description = "将转移用户的所有团队，转移到接受用户的旗下。", Icon = IconFontawesome.trademark)]
    public class TransferRelationshipForm : UIBase, IAutoForm
    {
        /// <summary>
        ///     当前登录id
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden)]
        public long UserId { get; set; }

        /// <summary>
        ///     需转移的用户名
        /// </summary>
        [Display(Name = "需转移用户名")]
        [Required(ErrorMessage = "需转移的用户名")]
        [HelpBlock("需转移的用户名，具有一定的风险，修改后需确保组织架构图正确，否则会影响分润的正确性<br/><code>确定后会将该用户所有的团队人转移到接受人旗下</code>")]
        [Field(EditShow = true, SortOrder = 1, ControlsType = ControlsType.TextBox, ValidType = ValidType.UserName)]
        public string UserName { get; set; }

        /// <summary>
        ///     上级用户名(推荐人)
        /// </summary>
        [Display(Name = "接受用户名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("接受用户名，用户状态需正常<br/><code>确定后该用户,将接受转移用户名的所有团队</code>")]
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
            var view = new TransferRelationshipForm();
            return ToAutoForm(view);
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            var view = model.MapTo<TransferRelationshipForm>();
            if (autoModel != null) view.UserId = autoModel.BasicUser.Id;
            var user = Resolve<IUserService>().GetSingle(r => r.UserName == view.UserName);
            if (user == null) return ServiceResult.FailedWithMessage("用户名不存在");

            var parentUser = Resolve<IUserService>().GetSingle(view.ParentUserName);
            if (parentUser == null) return ServiceResult.FailedWithMessage("推荐人不存在");

            if (parentUser.Status != Status.Normal) return ServiceResult.FailedWithMessage("推荐人状态不正常");

            var currentUserPay = Resolve<IUserDetailService>().GetSingle(r => r.Id == view.UserId);
            if (view.PayPassword.ToMd5HashString() != currentUserPay.PayPassword)
                return ServiceResult.FailedWithMessage("支付密码错误！");

            var result = UpdateParentUserAfterUserDelete(user.Id, parentUser.Id);

            if (!result.Succeeded) return ServiceResult.FailedWithMessage("推荐人修改失败");

            return ServiceResult.Success;
        }

        public ServiceResult UpdateParentUserAfterUserDelete(long userId, long parentId)
        {
            var userTreeConfig = Resolve<IAutoConfigService>().GetValue<UserTreeConfig>();
            // 不修改
            if (!userTreeConfig.AfterDeleteUserUpdateParentMap) return ServiceResult.Success;

            //var user = Resolve<IUserService>().GetSingle(r => r.Id == userId);
            //if (user != null) {
            //    return ServiceResult.FailedWithMessage("该用户没有物理删除会员");
            //}
            var parentUser = Resolve<IUserService>().GetSingle(r => r.Id == parentId);
            if (parentUser == null || parentUser.Status != Status.Normal)
                return ServiceResult.FailedWithMessage("用户已删除,其推荐人不存在或状态不正常");

            var childUsers = Resolve<IUserService>().GetList(r => r.ParentId == userId);
            var childUserIds = childUsers.Select(r => r.Id).ToList();
            var result = Repository<IUserRepository>().UpdateRecommend(childUserIds, parentId);

            if (result)
            {
                // 修改直推会员数量
                var recomonedUserCount = Resolve<IUserService>().Count(r => r.ParentId == parentUser.Id);
                var userMap = Resolve<IUserMapService>().GetSingle(r => r.UserId == parentUser.Id);
                userMap.LevelNumber = recomonedUserCount;
                Resolve<IUserMapService>().Update(userMap);
                Resolve<ITreeUpdateService>().ParentMapTaskQueue();
                return ServiceResult.Success;
            }

            return ServiceResult.FailedWithMessage("修改推荐人失败");
        }
    }
}