using System.ComponentModel.DataAnnotations;
using Alabo.AutoConfigs;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.Core.WebApis.Configs {

    [ClassProperty(Name = "Api接口安全", Icon = "fa	fa-exclamation-circle", SortOrder = 1,
        SideBarType = SideBarType.ControlSideBar)]
    public class ApiSecurityConfig : BaseViewModel, IAutoConfig {

        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "开启Api接口安全")]
        [HelpBlock("关闭后，Api接口没有经过任何的安全，数据会有入侵的可能。关闭模式仅用于程序调试，在正式环境下一定要开启Api接口安全设置")]
        public bool IsOpen { get; set; } = false;

        /// <summary>
        ///     Api私钥
        /// </summary>
        [Field(ControlsType = ControlsType.TextArea, Row = 12)]
        [HelpBlock("Api私钥需要有前台代码配置相同，设置Api秘钥后前台代码需与后台配置相同，为空时不验证，请妥善保管")]
        [Display(Name = "自定义Api私钥")]
        public string PrivateKey { get; set; }

        public void SetDefault() {
        }
    }
}