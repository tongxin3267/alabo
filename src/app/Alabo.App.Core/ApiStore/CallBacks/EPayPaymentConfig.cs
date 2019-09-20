using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.ApiStore.CallBacks {

    [NotMapped]
    /// <summary>
    /// Paypal支付配置
    /// </summary>
    [ClassProperty(Name = "网银支付配置", Icon = "fa fa-puzzle-piece",
        SideBarType = SideBarType.ApiStoreSideBar,
        SortOrder = 2, Description = "网银支付配置")]
    internal class EPayPaymentConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        ///     是否启用
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "是否启用")]
        [HelpBlock("启用以后，同时保证商户收款账户、商户收款Key、商户收款秘钥、商户收款邮箱设置正确的情况下才可以网银支付")]
        public bool IsEnable { get; set; } = false;

        /// <summary>
        ///     商户收款账户
        ///     从网银对接等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [HelpBlock("商户收款账户，可网银对接等第三方平台获取")]
        [Display(Name = "商户收款账户")]
        [Required]
        public string PayID { get; set; } = "";

        /// <summary>
        ///     商户收款Key
        ///     从网银对接等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox)]
        [HelpBlock("商户收款Key，可网银对接等第三方平台获取")]
        [Display(Name = "商户收款Key")]
        [Required]
        public string PayKey { get; set; } = "";

        /// <summary>
        ///     商户收款秘钥
        ///     从网银对接等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [HelpBlock("商户收款秘钥，可网银对接等第三方平台获取")]
        [Display(Name = "商户收款秘钥")]
        public string PaySecretKey { get; set; } = "";

        /// <summary>
        ///     商户收款邮箱
        ///     从网银对接等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox)]
        [HelpBlock("商户收款邮箱，可网银对接等第三方平台获取")]
        [Display(Name = "商户收款邮箱")]
        public string PayEmail { get; set; } = "";

        public void SetDefault() {
        }
    }
}