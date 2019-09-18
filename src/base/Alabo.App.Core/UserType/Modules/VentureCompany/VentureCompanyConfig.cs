using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Modules.VentureCompany {

    /// <summary>
    ///     合资公司配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "合资公司配置", Icon = "fa fa-user-times",
        Description = "合资公司配置", PageType = ViewPageType.Edit, SortOrder = 12)]
    public class VentureCompanyConfig {

        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, Row = 15)]
        [Display(Name = "合资公司服务条款")]
        [HelpBlock("请输入合资公司服务条款，默认值为合资公司服务条款")]
        public string VentureCompanyServiceAgreement { get; set; } = "合资公司服务条款";
    }
}