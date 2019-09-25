using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.AutoConfigs;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Common.Domain.CallBacks {

    [NotMapped]
    /// <summary>
    /// 管理控制台
    /// </summary>
    [ClassProperty(Name = "管理控制台", Icon = IconFlaticon.menu, SortOrder = 1,
        SideBarType = SideBarType.ControlSideBar)]
    public class AdminCenterConfig : BaseViewModel, IAutoConfig {

        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "不开启PC网站")]
        public bool IsAllowOpenPc { get; set; } = true;

        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "是否允许会员登录")]
        public bool IsAllowUserLogin { get; set; } = true;

        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "是否允许会员注册")]
        public bool IsAllowUserReg { get; set; } = true;

        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "是否允许会员激活")]
        public bool IsAllowUserActivate { get; set; } = true;

        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "是否允许激活或升级")]
        public bool IsAllowUserUpgrade { get; set; } = true;

        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "是否允许会员提现")]
        public bool IsAllowUserWitraw { get; set; } = true;

        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "是否允许会员充值")]
        public bool IsAllowUserRecharge { get; set; } = true;

        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "是否允许会员转账")]
        public bool IsAllowUserTransfer { get; set; } = true;

        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "允许会员修改会员资料")]
        [HelpBlock("是否允许会员资料，包括用户资料、密码、个人信息等资料")]
        public bool IsAllowUserChangeInfo { get; set; } = true;

        /// <summary>
        ///     开启分润
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "开启分润与升级")]
        [HelpBlock("开启分润与升级")]
        public bool StartFenrun { get; set; } = true;

        public void SetDefault() {
        }
    }
}