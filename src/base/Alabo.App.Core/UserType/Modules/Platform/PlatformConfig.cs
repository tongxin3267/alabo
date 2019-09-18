using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Modules.Platform {

    /// <summary>
    ///     平台配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "平台配置", Icon = "fa fa-user-times",
        Description = "平台配置", PageType = ViewPageType.Edit, SortOrder = 12,
        SideBarType = SideBarType.PlatformSideBar)]
    public class PlatformConfig : IAutoConfig {

        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, Row = 15)]
        [Display(Name = "平台服务条款")]
        [HelpBlock("请输入平台服务条款，默认值为平台服务条款")]
        public string PlatformServiceAgreement { get; set; } = "平台服务条款";

        public void SetDefault() {
            //throw new NotImplementedException();
        }
    }
}