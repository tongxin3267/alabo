using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Modules.Employees {

    /// <summary>
    ///     员工配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "员工配置", Icon = "fa fa-user-times",
        Description = "员工配置", PageType = ViewPageType.Edit, SortOrder = 12,
        SideBarType = SideBarType.EmployeesSideBar)]
    public class EmployeesConfig : IAutoConfig {

        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, Row = 15)]
        [Display(Name = "员工服务条款")]
        [HelpBlock("请输入员工服务条款，默认值为员工服务条款")]
        public string EmployeesServiceAgreement { get; set; } = "员工服务条款";

        public void SetDefault() {
        }
    }
}