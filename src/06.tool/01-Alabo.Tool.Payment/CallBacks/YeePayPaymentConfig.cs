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
    /// 易宝支付配置
    /// </summary>
    [ClassProperty(Name = "易宝支付配置", Icon = "fa fa-puzzle-piece")]
    public class YeePayPaymentConfig : BaseViewModel, IAutoConfig
    {
        /// <summary>
        ///     是否启用
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "是否启用")]
        [HelpBlock("启用以后，同时保证商户收款账户、商户收款Key、商户收款秘钥、商户收款邮箱设置正确的情况下才可以易宝支付")]
        public bool IsEnable { get; set; } = false;

        /// <summary>
        ///     商户收款账户
        ///     从易宝等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [HelpBlock("商户收款账户，可易宝等第三方平台获取")]
        [Display(Name = "商户收款账户")]
        [Required]
        public string YeeId { get; set; } = "";

        /// <summary>
        ///     商户收款Key
        ///     从易宝等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox)]
        [HelpBlock("商户收款Key，可易宝等第三方平台获取")]
        [Display(Name = "商户收款Key")]
        [Required]
        public string YeeKey { get; set; } = "";

        /// <summary>
        ///     商户收款秘钥
        ///     从易宝等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [HelpBlock("商户收款秘钥，可易宝等第三方平台获取")]
        [Display(Name = "商户收款秘钥")]
        public string YeeSecretKey { get; set; } = "";

        /// <summary>
        ///     商户收款邮箱
        ///     从易宝等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox)]
        [HelpBlock("商户收款邮箱，可易宝等第三方平台获取")]
        [Display(Name = "商户收款邮箱")]
        public string YeeEmail { get; set; } = "";

        public void SetDefault()
        {
        }
    }
}