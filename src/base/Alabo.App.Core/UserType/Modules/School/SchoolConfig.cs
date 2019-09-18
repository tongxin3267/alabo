using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Modules.School {

    /// <summary>
    ///     商学院配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "商学院配置", Icon = "fa fa-user-times",
        Description = "商学院配置", PageType = ViewPageType.Edit, SortOrder = 12,
        SideBarType = SideBarType.SchoolSideBar)]
    public class SchoolConfig : IAutoConfig {

        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, Row = 15)]
        [Display(Name = "商学院服务条款")]
        [HelpBlock("请输入商学院服务条款，默认值为商学院服务条款")]
        public string SchoolServiceAgreement { get; set; } = "商学院服务条款";

        public void SetDefault() {
        }
    }
}