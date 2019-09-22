using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.UI.AutoForms;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Market.UserCard.UI {

    /// <summary>
    /// 开卡
    /// </summary>
    [ClassProperty(Name = "会员升级")]
    public class UserCardOpen : UIBase, IAutoForm {

        /// <summary>
        ///     用户要等级Id
        /// </summary>
        [Field(ControlsType = ControlsType.RadioButton, ApiDataSource = "Api/User/GetUserGrade",
            EditShow = true, ListShow = true, SortOrder = 1)]
        [Display(Name = "会员卡等级")]
        [HelpBlock("请您选择会员卡等级")]
        public Guid GradeId { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, PlaceHolder = "请您输入开卡用户名",
            EditShow = true, ListShow = true, SortOrder = 2)]
        [Display(Name = "用户名")]
        [HelpBlock("请您务必输入开卡用户名")]
        public string UserName { get; set; }

        public AutoForm GetView(object id, AutoBaseModel autoModel) {

            var autoForm = ToAutoForm(new UserCardOpen());

            autoForm.AlertText = "【会员升级】用户可通过多种途径获取优惠券，优惠券可用于优惠商城消费";
            autoForm.ButtomHelpText = new List<string>
            {
                "会员卡等级：根据会员消费实际情况，设置相应会员等级",
                "用户名：主要填写店铺存的会员用户名",
    
            };


            return autoForm;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var find = AutoMapping.SetValue<UserCardOpen>(model);
            if (find.GradeId.IsGuidNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("会员卡等级不能为空");
            }
            var userGrade = Resolve<IGradeService>().GetGrade(find.GradeId);
            if (userGrade == null) {
                return ServiceResult.FailedWithMessage("会员等级不存在");
            }
            var user = Resolve<IUserService>().GetSingle(r => r.UserName == find.UserName);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户不存在");
            }
            user.GradeId = userGrade.Id;
            if (!Resolve<IUserService>().UpdateUser(user)) {
                return ServiceResult.FailedWithMessage("开卡失败");
            }
            return ServiceResult.Success;
        }
    }
}