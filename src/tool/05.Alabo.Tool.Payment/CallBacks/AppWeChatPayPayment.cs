using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.ApiStore.CallBacks {

    [NotMapped]
    /// <summary>
    /// APP微信支付配置
    /// </summary>
    [ClassProperty(Name = "APP微信支付配置", Icon = "fa fa-puzzle-piece",
        SideBarType = SideBarType.ApiStoreSideBar,
        SortOrder = 2, Description = "APP微信支付配置")]
    internal class AppWeChatPaymentConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        ///     是否启用
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "是否启用")]
        [HelpBlock("启用以后，同时保证微信开放平台AppId、微信开放平台AppSercet、微信支付商户号、商户号API秘钥设置正确的情况下才可以APP微信支付")]
        public bool IsEnable { get; set; } = false;

        /// <summary>
        ///     微信支付商户号
        ///     从微信支付等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [HelpBlock("微信支付商户号，即微信开放平台申请下来的微信支付商户号号码，可微信支付等第三方平台获取")]
        [Display(Name = "微信支付商户号")]
        [Required]
        public string MchId { get; set; } = "";

        /// <summary>
        ///     微信开放平台AppId
        ///     从微信开放平台等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox)]
        [HelpBlock("微信开放平台AppId，可微信开放平台等第三方平台获取")]
        [Display(Name = "微信开放平台AppId")]
        [Required]
        public string AppId { get; set; } = "";

        /// <summary>
        ///     微信开放平台AppSercet
        ///     从微信开放平台等第三方平台获取
        [Field(ControlsType = ControlsType.TextBox)]
        [HelpBlock("微信开放平台AppSercet（应用秘钥），可微信开放平台等第三方平台获取")]
        [Display(Name = "微信开放平台AppSercet")]
        public string AppSecret { get; set; } = "";

        /// <summary>
        ///     商户号API秘钥
        ///     从微信支付等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [HelpBlock("商户号API秘钥，可微信支付等第三方平台获取")]
        [Display(Name = "商户号API秘钥")]
        public string APISecretKey { get; set; } = "";

        public void SetDefault() {
        }
    }
}