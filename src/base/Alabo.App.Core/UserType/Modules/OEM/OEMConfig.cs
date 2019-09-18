using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Modules.OEM {

    /// <summary>
    ///     OEM配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "OEM配置", Icon = "fa fa-user-times",
        Description = "OEM配置", PageType = ViewPageType.Edit, SortOrder = 12,
        SideBarType = SideBarType.OemSideBar)]
    public class OEMConfig : IAutoConfig {

        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, Row = 15)]
        [Display(Name = "OEM服务条款")]
        [HelpBlock("请输入OEM服务条款，默认值为OEM服务条款")]
        public string OEMServiceAgreement { get; set; } = "OEM服务条款";

        public void SetDefault() {
            // throw new NotImplementedException();
        }
    }
}