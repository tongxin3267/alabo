using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.ApiStore.CallBacks {

    [NotMapped]
    /// <summary>
    /// 支付宝配置
    /// </summary>
    [ClassProperty(Name = "支付宝配置", Icon = "fa fa-puzzle-piece",
        SideBarType = SideBarType.ApiStoreSideBar,
        SortOrder = 2, Description = "支付宝配置,支持PC电脑端、手机移动端、苹果手机、安卓手机")]
    public class AlipayPaymentConfig : BaseViewModel, IAutoConfig {//internal

        /// <summary>
        ///     是否启用
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "是否启用")]
        [HelpBlock("启用以后，同时保证合作伙伴（开发平台）身份（APPID）、收款账户Key（应用私钥），支付宝公钥、商户收款MD5秘钥、商户收款邮箱设置正确的情况下才可以支付宝支付<br/><code><a href='http://ui.5ug.com/static/jc/alipay.pdf' target='_blank'>支付宝配置教程</a></code>")]
        public bool IsEnable { get; set; } = true;

        /// <summary>
        ///     商户收款账户
        ///     从支付宝等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [HelpBlock("商户收款账户，即合作伙伴身份（开放平台APPID），可支付宝等第三方平台获取，<br/><code><a href='https://www.alipay.com/' target='_blank'>支付宝首页</a></code>")]
        [Display(Name = "商户收款账户")]
        [Required]
        public string AppId { get; set; }

        /// <summary>
        ///     支付宝公钥
        ///     从支付宝等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextArea, Row = 5)]
        [HelpBlock("支付宝公钥，确定是支付宝公钥,而不是应用公钥;该公钥用于解密支付宝回调(回调签名用的是支付宝私钥=>无法查看的)，<br/><code><a href='https://docs.open.alipay.com/200/105310' target='_blank'>公钥与私钥说明</a></code>")]
        [Display(Name = "支付宝公钥")]
        [Required]
        public string RsaAlipayPublicKey { get; set; }

        /// <summary>
        ///     收款账户Key
        ///     支付宝私钥
        ///     从支付宝等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextArea, Row = 12)]
        [HelpBlock("支付宝应用私钥，与上方商户收款账户填写信息一致")]
        [Display(Name = "支付宝应用私钥")]
        [Required]
        public string RsaPrivateKey { get; set; }

        /// <summary>
        ///     商户收款MD5秘钥
        ///     从支付宝等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = false, EditShow = false)]
        [HelpBlock("商户收款MD5秘钥，可支付宝等第三方平台获取")]
        [Display(Name = "商户收款MD5秘钥")]
        public string MD5SecretKey { get; set; } = "";

        /// <summary>
        ///     商户收款邮箱
        ///     从支付宝等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, EditShow = false)]
        [HelpBlock("商户收款邮箱，可支付宝等第三方平台获取")]
        [Display(Name = "商户收款邮箱")]
        public string PIDEmail { get; set; } = "";

        [Display(Name = "设置默认值")]
        public void SetDefault() {
        }
    }
}

//{
//  "Alipay": {
//    "AppId": "xxx",
//    "RsaPublicKey": "xxx",
//    "RsaPrivateKey": "xxx"
//  },
//  "WeChatPay": {
//    "AppId": "xxx",
//    "AppSecret": "xxx",
//    "MchId": "xxx",
//    "Key": "xxx",
//    "Certificate": "xxx",
//    "RsaPublicKey": "xxx",
//  },
//  "QPay": {
//    "MchId": "xxx",
//    "Key": "xxx",
//    "Certificate": "xxx",
//  },
//  "JdPay": {
//    "Merchant": "xxx",
//    "RsaPublicKey": "xxx",
//    "RsaPrivateKey": "xxx",
//    "DesKey": "xxx"
//  }
//}