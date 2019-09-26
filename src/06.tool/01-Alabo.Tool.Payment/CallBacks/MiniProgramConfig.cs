using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.AutoConfigs;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

/// <summary>
/// </summary>
namespace Alabo.Tool.Payment.CallBacks {

    [NotMapped]
    /// <summary>
    /// 微信小程序配置
    /// </summary>
    [ClassProperty(Name = "微信小程序配置", Icon = "fa fa-puzzle-piece",
        SideBarType = SideBarType.ApiStoreSideBar,
        SortOrder = 2, Description = "微信小程序配置")]
    public class MiniProgramConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        ///     是否启用
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "是否启用")]
        [HelpBlock("启用以后，同时保证小程序的小程序AppID、小程序密钥设置正确的情况下才可以微信小程序")]
        public bool IsEnable { get; set; } = false;

        /// <summary>
        ///     Gets or sets the application identifier.
        /// </summary>
        /// <value>The application identifier.</value>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1)]
        [Display(Name = "小程序AppID")]
        [HelpBlock("属性小程序AppId")]
        public string AppID { get; set; } = "wxacb704e4280d1fe9";

        /// <summary>
        ///     Gets or sets the application secret.
        /// </summary>
        /// <value>The application secret.</value>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1)]
        [Display(Name = "AppSecret(小程序密钥)")]
        [HelpBlock("请输入AppSecret(小程序密钥)")]
        public string AppSecret { get; set; } = "ac63b1e9b8f626d150d482bf2daf7226";

        /// <summary>
        ///     Sets the default.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SetDefault() {
        }
    }
}