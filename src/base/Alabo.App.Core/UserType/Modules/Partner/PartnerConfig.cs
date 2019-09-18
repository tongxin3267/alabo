using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.UserType.Modules.Partner {

    /// <summary>
    ///     Partner配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "配置", Icon = "fa fa-user-times", Description = "配置", PageType = ViewPageType.Edit,
        SortOrder = 12,
        SideBarType = SideBarType.PartnerSideBar)]
    public class PartnerConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        ///     设置服务中心为自己
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "会员注册自动添加")]
        [HelpBlock("新会员注册时，自动添加合伙人，自动关联内部合伙人")]
        public bool AfterUserRegAddPanter { get; set; } = false;

        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, Row = 15)]
        [Display(Name = "服务条款")]
        [HelpBlock("请输入服务条款，默认值为服务条款")]
        public string PartnerServiceAgreement { get; set; } = "服务条款";

        public void SetDefault() {
            // throw new NotImplementedException();
        }
    }
}