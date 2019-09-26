using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Tool.Payment
{
    /// <summary>
    ///     支付方式
    /// </summary>
    [ClassProperty(Name = "支付方式")]
    public enum PayType
    {
        /// <summary>
        ///     余额支付
        ///     会员在系统内的余额
        /// </summary>
        [Display(Name = "余额支付")]
        [ClientType(AllowClient = "PcWeb,WapH5,IOS,Android,WeChat,WeChatLite", Icon = "zk-amount",
            Intro = "使用余额支付")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = true, GuidId = "E0000000-1481-49BD-BFC7-E00000000000")]
        BalancePayment = 1,

        /// <summary>
        ///     支付宝 PC端网页支付
        /// </summary>
        [Display(Name = "支付宝电脑支付")]
        [ClientType(AllowClient = "PcWeb", Icon = "zk-alipay", Intro = "使用支付宝PC端网页支付")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = true, GuidId = "E0000000-1481-49BD-BFC7-E00000000001")]
        AlipayWeb = 2,

        /// <summary>
        ///     支付宝 手机端网页支付
        /// </summary>
        [Display(Name = "支付宝手机支付")]
        [ClientType(AllowClient = "WapH5,Recharge", Icon = "zk-alipay", Intro = "使用支付宝手机端网页支付")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = true, GuidId = "E0000000-1481-49BD-BFC7-E00000000003")]
        AlipayWap = 3,

        /// <summary>
        ///     支付宝 手机app支付
        /// </summary>
        [Display(Name = "支付宝App支付")]
        [ClientType(AllowClient = "Android,IOS", Icon = "zk-alipay", Intro = "使用支付宝App支付")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = true, GuidId = "E0000000-1481-49BD-BFC7-E00000000004")]
        AlipayApp = 4,

        /// <summary>
        ///     支付宝 扫码支付
        /// </summary>
        [Display(Name = "支付宝扫码支付")]
        [ClientType(AllowClient = "PcWeb", Icon = "zk-alipay", Intro = "使用支付宝扫码支付")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = false, GuidId = "E0000000-1481-49BD-BFC7-E00000000005")]
        AliPayQrCode = 5,

        /// <summary>
        ///     支付宝 条码支付
        /// </summary>
        [Display(Name = "支付宝条码支付")]
        [ClientType(AllowClient = "PcWeb", Icon = "zk-alipay", Intro = "使用支付宝条码支付")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = false, GuidId = "E0000000-1481-49BD-BFC7-E00000000006")]
        AliPayBar = 6,

        /// <summary>
        ///     微信 公众号 支付
        /// </summary>
        [Display(Name = "微信支付")]
        [ClientType(AllowClient = "WeChat,Recharge", Icon = "zk-wechatpay", Intro = "使用微信公众号支付")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = true, GuidId = "E0000000-1481-49BD-BFC7-E00000000007")]
        WeChatPayPublic = 7,

        /// <summary>
        ///     微信 APP 支付
        /// </summary>
        [Display(Name = "微信APP支付")]
        [ClientType(AllowClient = "Android,IOS", Icon = "zk-wechatpay", Intro = "使用微信APP支付")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = true, GuidId = "E0000000-1481-49BD-BFC7-E00000000008")]
        WeChatPayApp = 8,

        /// <summary>
        ///     微信 刷卡支付，与支付宝的条码支付对应
        /// </summary>
        [Display(Name = "微信条码支付")]
        [ClientType(AllowClient = "PcWeb", Icon = "zk-wechatpay", Intro = "使用微信条码支付")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = false, GuidId = "E0000000-1481-49BD-BFC7-E00000000009")]
        WeChatPayBar = 9,

        /// <summary>
        ///     微信 扫码支付  (可以使用app的帐号，也可以用公众的帐号完成)
        /// </summary>
        [Display(Name = "微信扫码支付")]
        [ClientType(AllowClient = "PcWeb", Icon = "zk-wechatpay", Intro = "使用微信扫码支付")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = false, GuidId = "E0000000-1481-49BD-BFC7-E00000000010")]
        WeChatPayQrCode = 10,

        /// <summary>
        /// 微信wap支付，针对特定用户
        /// 微信H5支付
        /// </summary>
        //[Display(Name = "微信H5支付")]
        //[ClientType(AllowClient = "WapH5", Icon = "zk-wechatpay", Intro = "使用微信H5支付")]
        //[LabelCssClass("m-badge--info")]
        //[Field(IsDefault = false, GuidId = "E0000000-1481-49BD-BFC7-E00000000011")]
        //WeChatPayWap = 11,

        /// <summary>
        ///     微信小程序支付
        /// </summary>
        [Display(Name = "微信小程序支付")]
        [ClientType(AllowClient = "WeChatLite", Icon = "zk-wechatpay", Intro = "使用微信小程序支付")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = true, GuidId = "E0000000-1481-49BD-BFC7-E00000000012")]
        WeChatPayLite = 12,

        ///// <summary>
        ///// QQ钱包扫描支付
        ///// </summary>
        //[Display(Name = "QQ钱包支付")]
        //[ClientType(AllowClient = "PcWeb,WapH5", Icon = "zk-qpay", Intro = "使用QQ钱包支付")]
        //[LabelCssClass("m-badge--info")]
        //[Field(IsDefault = false, GuidId = "E0000000-1481-49BD-BFC7-E00000000013")]
        //QPayBar = 13,

        /// <summary>
        ///     京东钱包支付
        /// </summary>
        [Display(Name = "京东钱包支付")]
        [ClientType(AllowClient = "IOS,Android", Icon = "zk-jdpay", Intro = "使用京东钱包支付")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = false, GuidId = "E0000000-1481-49BD-BFC7-E00000000014")]
        JdPayBar = 14,

        /// <summary>
        ///     易宝支付
        /// </summary>
        [Display(Name = "易宝支付")]
        [ClientType(AllowClient = "PcWeb", Icon = "zk-yeepay", Intro = "使用易宝支付")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = false, GuidId = "E0000000-1481-49BD-BFC7-E00000000015")]
        YeePayBar = 15,

        /// <summary>
        ///     网银支付
        /// </summary>
        [Display(Name = "网银支付")]
        [ClientType(AllowClient = "PcWeb", Icon = "zk-unionpay", Intro = "使用网银支付")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = false, GuidId = "E0000000-1481-49BD-BFC7-E00000000016")]
        EPayBar = 16,

        /// <summary>
        ///     Paypal PC端支付
        /// </summary>
        [Display(Name = "Paypal电脑支付")]
        [ClientType(AllowClient = "Android", Icon = "zk-paypla", Intro = "使用Paypal PC端支付")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = false, GuidId = "E0000000-1481-49BD-BFC7-E000000000017")]
        PaypalWeb = 17,

        /// <summary>
        ///     Paypal 手机端支付
        /// </summary>
        [Display(Name = "Paypal手机端支付")]
        [ClientType(AllowClient = "WapH5", Icon = "zk-paypal", Intro = "使用Paypal 手机端支付")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = false, GuidId = "E0000000-1481-49BD-BFC7-E00000000018")]
        PaypalWap = 18,

        /// <summary>
        ///     管理员代付
        /// </summary>
        [Display(Name = "管理员代付")]
        [ClientType(AllowClient = "", Icon = "zk-paypal", Intro = "管理员代付")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = true, GuidId = "E0000000-1481-49BD-BFC7-E00000000099")]
        AdminPay = 99,

        /// <summary>
        ///     线下转账给卖家
        /// </summary>
        [Display(Name = "线下转账给卖家")]
        [ClientType(AllowClient = "", Icon = "zk-paypal", Intro = "线下转账给卖家")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = true, GuidId = "E0000000-1481-49BD-BFC7-E00000000109")]
        OffineToSeller = 109
    }
}