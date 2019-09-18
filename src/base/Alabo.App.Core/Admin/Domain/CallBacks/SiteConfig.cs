using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Admin.Domain.CallBacks {

    /// <summary>
    /// 站点配置信息
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "站点配置信息", Icon = IconFlaticon.computer, SortOrder = 1,
        SideBarType = SideBarType.ControlSideBar)]
    public class SiteConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        /// 站点编号
        /// </summary>
        [Display(Name = "项目编号")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, SortOrder = 1)]
        public string ProjectNum { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public string Tenant { get; set; }

        /// <summary>
        /// 用户ID,平台的站点用户
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     站点名称
        /// </summary>
        [Display(Name = "公司名称")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, Link = "/Admin/Site/Info?id=[[Id]]", Width = "200", ListShow = true,
            SortOrder = 2)]
        public string CompanyName { get; set; }

        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, Width = "100", ListShow = true,
            SortOrder = 4)]
        [Display(Name = "手机号码")]
        public string Phone { get; set; }

        public string ClientHost { get; set; }

        /// <summary>
        ///     前台预览地址，移动端预览地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// PC端后台预览地址
        /// </summary>
        public string AdminUrl { get; set; }

        public void SetDefault() {
        }
    }
}