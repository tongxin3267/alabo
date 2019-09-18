using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Modules.ChuangKe {

    /// <summary>
    ///     创客配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "创客配置", Icon = "fa fa-user-times",
        Description = "创客配置", PageType = ViewPageType.Edit, SortOrder = 12,
        SideBarType = SideBarType.ChuangKeSideBar)]
    public class ChuangKeConfig : IAutoConfig {

        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, Row = 15)]
        [Display(Name = "创客服务条款")]
        [HelpBlock("请输入创客服务条款，默认值为创客服务条款")]
        public string ChuangKeServiceAgreement { get; set; } = "创客服务条款";

        public void SetDefault() {
        }
    }
}