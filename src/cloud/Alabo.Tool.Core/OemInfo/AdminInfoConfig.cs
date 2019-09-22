using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Admin.Domain.CallBacks {

    /// <summary>
    /// 公司信息
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "登陆页公司信息", Icon = IconFlaticon.computer, SortOrder = 1,
        SideBarType = SideBarType.ControlSideBar)]
    public class AdminInfoConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        /// 登陆页显示公司名称
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1)]
        [Display(Name = "公司名称")]
        public string Name { get; set; } = "广东中酷网络科技有限公司";

        /// <summary>
        /// 登陆页显示公司网址
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1)]
        [Display(Name = "公司网址")]
        public string Url { get; set; } = "5ug.com";

        /// <summary>
        /// 登陆页logo
        /// </summary>
        [Field(ControlsType = ControlsType.AlbumUploder, GroupTabId = 1)]
        [Display(Name = "登陆页Logo")]
        public string LoginLogo { get; set; } = "/wwwroot/static/images/logo.png";

        ///// <summary>
        ///// 首页logo
        ///// </summary>
        //[Field(ControlsType = ControlsType.ImagePreview, GroupTabId = 1)]
        //[Display(Name = "首页logo")]
        //[Field(ControlsType = ControlsType.AlbumUploder, GroupTabId = 1)]
        //public string AdminLogo { get; set; } = "/wwwroot/static/images/adminlogo.png";

        public void SetDefault() {
        }
    }
}