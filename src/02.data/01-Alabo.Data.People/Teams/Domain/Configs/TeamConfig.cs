using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.AutoConfigs;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.User.Domain.Callbacks {

    [NotMapped]
    [ClassProperty(Name = "团队配置", Icon = "flaticon-map",
        Description = "团队配置", PageType = ViewPageType.Edit, SortOrder = 12, SideBarType = SideBarType.ControlSideBar)]
    public class TeamConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        ///     团队数量定义
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = false, EditShow = true, SortOrder = 1)]
        [Display(Name = "团队有效层数", Description = "团队数量定义")]
        [HelpBlock("团队有效层数，计算数内的有效会员")]
        public long TeamLevel { get; set; } = 3;

        /// <summary>
        ///     支付密码
        /// </summary>
        [Field(ControlsType = ControlsType.Password, ListShow = false, EditShow = false, SortOrder = 1)]
        [Display(Name = "支付密码", Description = "团队数量定义")]
        public string SafePassword { get; set; }

        public void SetDefault() {
        }
    }
}