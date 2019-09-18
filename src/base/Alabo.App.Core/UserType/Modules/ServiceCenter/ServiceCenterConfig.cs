using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.UserType.Modules.ServiceCenter {

    /// <summary>
    ///     门店配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "配置", Icon = "fa fa-user-times",
        Description = "配置", PageType = ViewPageType.Edit, SortOrder = 12,
        SideBarType = SideBarType.ServiceCenterSideBar)]
    public class ServiceCenterConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        ///     设置服务中心为自己
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "设置服务中心为自己")]
        [HelpBlock("添加自己为服务中心时，会员本身的服务中心会从其他服务中心修改为自己")]
        public bool IsSetUserSelf { get; set; } = true;

        /// <summary>
        ///     推荐人类型限制
        /// </summary>

        [Field(ControlsType = ControlsType.CheckBoxMultipl, SortOrder = 2,
            DataSource = "Alabo.App.Core.User.Domain.CallBacks.UserTypeConfig")]
        [Display(Name = "推荐人类型")]
        [HelpBlock(
            "请选择符合条件的用户类型，只有被选择的用户类型才可以成为推荐人.<a href='/Admin/AutoConfig/List?key=Alabo.App.Core.User.Domain.CallBacks.UserTypeConfig'>用户类型</a>")]
        public string ParentUserTypes { get; set; } = "71be65e6-3a64-414d-972e-1a3d4a365000";

        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, Row = 15)]
        [Display(Name = "服务条款")]
        [HelpBlock("请输入服务条款，默认值为服务条款")]
        public string ServiceCenterServiceAgreement { get; set; } = "服务条款请到此处补充";

        public void SetDefault() {
            //  throw new NotImplementedException();
        }
    }
}