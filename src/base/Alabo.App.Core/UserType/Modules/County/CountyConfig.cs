using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Modules.County {

    /// <summary>
    ///     县代理配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "县代理配置", Icon = "fa fa-user-times",
        Description = "县代理配置", PageType = ViewPageType.Edit, SortOrder = 12,
        SideBarType = SideBarType.CountySideBar)]
    public class CountyConfig : IAutoConfig {

        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, Row = 15)]
        [Display(Name = "县代理服务条款")]
        [HelpBlock("请输入县代理服务条款，默认值为县代理服务条款")]
        public string CountyServiceAgreement { get; set; } = "县代理服务条款";

        public void SetDefault() {
            //  throw new NotImplementedException();
        }
    }
}