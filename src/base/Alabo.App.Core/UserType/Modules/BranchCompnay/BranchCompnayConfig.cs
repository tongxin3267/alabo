using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Modules.BranchCompnay {

    /// <summary>
    ///     商学院，分公司配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "分公司配置", Icon = "fa fa-user-times",
        Description = "分公司配置", PageType = ViewPageType.Edit, SortOrder = 12,
        SideBarType = SideBarType.BranchCompnaySideBar)]
    public class BranchCompnayConfig : IAutoConfig {

        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, Row = 15)]
        [Display(Name = "分公司服务条款")]
        [HelpBlock("请输入分公司服务条款，默认值为分公司服务条款")]
        public string BranchCompnayServiceAgreement { get; set; } = "分公司服务条款";

        public void SetDefault() {
        }
    }
}