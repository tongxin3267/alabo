using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.AutoConfigs;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.ApiStore.CallBacks {

    [NotMapped]
    /// <summary>
    /// Paypal支付配置
    /// </summary>
    [ClassProperty(Name = "Paypal支付配置", Icon = "fa fa-puzzle-piece",
        SideBarType = SideBarType.ApiStoreSideBar,
        SortOrder = 2, Description = "Paypal支付配置")]
    public class PaypalPayPaymentConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        ///     是否启用
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "是否启用")]
        [HelpBlock("启用以后，同时保证商户收款账户、商户收款Key、商户收款秘钥、商户收款邮箱设置正确的情况下才可以Paypal支付")]
        public bool IsEnable { get; set; } = false;

        /// <summary>
        ///     商户收款账户
        ///     从Paypal等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [HelpBlock("商户收款账户，可Paypal等第三方平台获取")]
        [Display(Name = "商户收款账户")]
        [Required]
        public string PaypalId { get; set; } = "";

        /// <summary>
        ///     商户收款Key
        ///     从Paypal等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox)]
        [HelpBlock("商户收款Key，可Paypal等第三方平台获取")]
        [Display(Name = "商户收款Key")]
        [Required]
        public string PaypalKey { get; set; } = "";

        /// <summary>
        ///     商户收款秘钥
        ///     从Paypal等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [HelpBlock("商户收款秘钥，可Paypal等第三方平台获取")]
        [Display(Name = "商户收款秘钥")]
        public string PaypalSecretKey { get; set; } = "";

        /// <summary>
        ///     商户收款邮箱
        ///     从Paypal等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox)]
        [HelpBlock("商户收款邮箱，可Paypal等第三方平台获取")]
        [Display(Name = "商户收款邮箱")]
        public string PaypalEmail { get; set; } = "";

        public void SetDefault() {
        }
    }
}