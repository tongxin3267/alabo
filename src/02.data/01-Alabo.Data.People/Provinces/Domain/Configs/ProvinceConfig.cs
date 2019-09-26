using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.AutoConfigs;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Data.People.Provinces.Domain.Configs {

    /// <summary>
    ///     省代理配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "省代理配置", Icon = "fa fa-user-times",
        Description = "省代理配置", PageType = ViewPageType.Edit, SortOrder = 12,
        SideBarType = SideBarType.PartnerSideBar)]
    public class ProvinceConfig : IAutoConfig {

        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, Row = 15)]
        [Display(Name = "省代理服务条款")]
        [HelpBlock("请输入省代理服务条款，默认值为省代理服务条款")]
        public string ProvinceServiceAgreement { get; set; } = "省代理服务条款";

        public void SetDefault() {
            // throw new NotImplementedException();
        }
    }
}