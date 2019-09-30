using Alabo.AutoConfigs;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Tool.Payment.CallBacks
{
    /// <summary>
    ///     公众号微信支付配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "公众号微信支付配置", Icon = "fa fa-puzzle-piece",
        SideBarType = SideBarType.ApiStoreSideBar,
        SortOrder = 2, Description = "公众号微信支付配置")]
    public class WeChatPaymentConfig : BaseViewModel, IAutoConfig
    {
        /// <summary>
        ///     是否启用
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "是否启用")]
        [HelpBlock(
            "启用以后，同时保证公众号AppId、公众号AppSercet、微信支付商户号、商户号API秘钥设置正确的情况下才可以公众号微信支付<br/><code><a href='http://ui.5ug.com/static/jc/weixin.pdf' target='_blank'>具体操作教程</a></code>")]
        public bool IsEnable { get; set; } = false;

        /// <summary>
        ///     获取头像新
        /// </summary>
        /// <summary>
        ///     是否启用
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "用户手动授权")]
        [HelpBlock("用户手动授权：可获取到头像、地址、性别等公开信息.如果是动态。如果是静默授权则不行")]
        public bool IsBaseUserInfo { get; set; } = true;

        /// <summary>
        ///     微信支付商户号
        ///     从微信支付等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [HelpBlock(
            "微信支付商户号，<br/><code><a href='https://mp.weixin.qq.com/' target='_blank'>微信公众号平台</a></code><code><a href='https://pay.weixin.qq.com' target='_blank'>微信支付</a></code> 中查看<br/><code><a href='http://ui.5ug.com/static/jc/weixin.pdf' target='_blank'>具体操作教程</a></code>")]
        [Display(Name = "微信支付商户号")]
        [Required]
        public string MchId { get; set; } = "1502045441";

        /// <summary>
        ///     公众号AppId
        ///     从微信公众平台等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox)]
        [HelpBlock(
            "公众号AppId，在<code><a href='https://mp.weixin.qq.com/' target='_blank'>微信公众号平台</a></code>中获取<br/><code><a href='http://ui.5ug.com/static/jc/weixin.pdf' target='_blank'>具体操作教程</a></code>")]
        [Display(Name = "公众号AppId")]
        [Required]
        public string AppId { get; set; } = "";

        /// <summary>
        ///     公众号AppSecret
        ///     从微信公众平台等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox)]
        [HelpBlock(
            "公众号AppSecret（应用秘钥），可以在微信公众号中重置.<code><a href='https://mp.weixin.qq.com/' target='_blank'>微信公众号平台</a></code><br/><code><a href='http://ui.5ug.com/static/jc/weixin.pdf' target='_blank'>具体操作教程</a></code>")]
        [Display(Name = "公众号AppSecret(微信公众号中重置)")]
        public string AppSecret { get; set; } = "";

        /// <summary>
        ///     商户号API秘钥
        ///     从微信支付等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [HelpBlock(
            "商户号API秘钥，需在微信支付中自行设置，长度32位.<code><a href='https://pay.weixin.qq.com' target='_blank'>微信支付</a></code> ")]
        [Display(Name = "商户号API秘钥(微信支付中自行设置)")]
        public string APISecretKey { get; set; } = "";

        /// <summary>
        ///     授权网址
        ///     从微信支付等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [HelpBlock(
            "授权网址，在微信当中授权的网址，必须以http或https开头，需要在微信公众号、和微信支付当中授权，具体操作请查看详细教程<br/><code><a href='http://ui.5ug.com/static/jc/weixin.pdf' target='_blank'>具体操作教程</a></code>")]
        [Display(Name = "授权网址（前台网址) ")]
        public string ReturnUrl { get; set; } = "http://www.5ug.com";

        /// <summary>
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [HelpBlock("回调地址，微信支付的后台地址，及后台网址")]
        [Display(Name = "回调地址（后台网址)")]
        public string CallBackUrl { get; set; } = "http://admin.5ug.com";

        public void SetDefault()
        {
        }
    }
}