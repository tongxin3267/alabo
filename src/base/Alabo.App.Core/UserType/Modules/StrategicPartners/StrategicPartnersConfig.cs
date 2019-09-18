using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Modules.StrategicPartners {

    /// <summary>
    ///     合作伙伴配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "合作伙伴配置", Icon = "fa fa-user-times",
        Description = "合作伙伴配置", PageType = ViewPageType.Edit, SortOrder = 12,
        SideBarType = SideBarType.VentureCompanySideBar)]
    public class StrategicPartnersConfig : IAutoConfig {

        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, Row = 15)]
        [Display(Name = "合作伙伴服务条款")]
        [HelpBlock("请输入合作伙伴服务条款，默认值为合作伙伴服务条款")]
        public string StrategicPartnersServiceAgreement { get; set; } = "合作伙伴服务条款";

        public void SetDefault() {
            // throw new NotImplementedException();
        }
    }
}