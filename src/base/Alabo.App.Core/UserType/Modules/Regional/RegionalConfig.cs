using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Modules.Regional {

    /// <summary>
    ///     大区配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "大区配置", Icon = "fa fa-user-times",
        Description = "大区配置", PageType = ViewPageType.Edit, SortOrder = 12,
        SideBarType = SideBarType.RegionalSideBar)]
    public class RegionalConfig : IAutoConfig {

        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, Row = 15)]
        [Display(Name = "大区服务条款")]
        [HelpBlock("请输入大区服务条款，默认值为大区服务条款")]
        public string RegionalServiceAgreement { get; set; } = "大区服务条款";

        public void SetDefault() {
            // throw new NotImplementedException();
        }
    }
}