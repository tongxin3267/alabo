using Alabo.AutoConfigs;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Tool.Payment.CallBacks
{
    [NotMapped]
    /// <summary>
    /// 京东支付配置
    /// </summary>
    [ClassProperty(Name = "京东支付配置", Icon = "fa fa-puzzle-piece")]
    public class JdPayPaymentConfig : BaseViewModel, IAutoConfig
    {
        /// <summary>
        ///     是否启用
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "是否启用")]
        [HelpBlock("启用以后，同时保证商户收款账户、商户收款Key、商户收款秘钥、商户收款邮箱设置正确的情况下才可以京东支付")]
        public bool IsEnable { get; set; } = false;

        /// <summary>
        ///     商户收款账户
        ///     从京东支付等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [HelpBlock("商户收款账户，可京东支付等第三方平台获取")]
        [Display(Name = "商户收款账户")]
        [Required]
        public string Merchant { get; set; } = "";

        /// <summary>
        ///     商户收款Key
        ///     从京东支付等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox)]
        [HelpBlock("商户收款Key，可京东支付等第三方平台获取")]
        [Display(Name = "商户收款Key")]
        [Required]
        public string RsaPublicKey { get; set; } = "";

        /// <summary>
        ///     商户收款秘钥
        ///     从京东支付等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [HelpBlock("商户收款秘钥，可京东支付等第三方平台获取")]
        [Display(Name = "商户收款秘钥")]
        public string RsaPrivateKey { get; set; } = "";

        /// <summary>
        ///     商户收款邮箱
        ///     从京东支付等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox)]
        [HelpBlock("商户收款邮箱，可京东支付等第三方平台获取")]
        [Display(Name = "商户收款邮箱")]
        public string DesKey { get; set; } = "";

        public void SetDefault()
        {
        }
    }
}