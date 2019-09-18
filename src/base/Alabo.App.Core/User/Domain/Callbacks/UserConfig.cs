using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.User.Domain.Callbacks {

    [NotMapped]
    /// <summary>
    /// 会员注册选项
    /// </summary>
    [ClassProperty(Name = "用户选项 ", Icon = "fa fa-user", Description = "会员选项", SortOrder = 10,
        SideBarType = SideBarType.UserSideBar)]
    public class UserConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        ///     Gets or sets the recommend redirect URL.
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1)]
        [Display(Name = "新会员默认支付密码")]
        [HelpBlock("新注册会员默认支付密码,如果为空时默认密码为登陆密码，长度不能低于6位")]
        public string DefaultPassword { get; set; }

        /// <summary>
        /// 默认时支付密码和登录密码一样,设置新会员默认支付密码时不生效
        /// </summary>
        [Display(Name = "第一次使用支付密码时设置为支付密码")]
        [HelpBlock("第一次使用支付密码时设置为支付密码，注册的时候注册密码为空")]
        public bool TheFirstUserSetPayPassword { get; set; } = true;

        /// <summary>
        ///     Gets or sets the recommend redirect URL.
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1)]
        [Display(Name = "推荐注册后跳转网址")]
        [HelpBlock("推荐注册后跳转到的网址")]
        public string RecommendRedirectUrl { get; set; } = "/Index";

        /// <summary>
        ///    注册自动填充的邮箱后缀
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1)]
        [Display(Name = "注册自动填充的邮箱后缀")]
        [HelpBlock("注册自动填充的邮箱后缀")]
        public string RegEmailPostfix { get; set; } = "@qnn.com";

        /// <summary>
        ///     Gets or sets the period of validity.
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1)]
        [Display(Name = "推广链接有效期")]
        [HelpBlock("推广链接有效期,超过这个有效期，推广链接将失效")]
        public long PeriodOfValidity { get; set; } = 30;

        /// <summary>
        ///     Gets or sets the login expiration time.
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1)]
        [Display(Name = "登录有效期")]
        [HelpBlock("用户登录后超出这个时间，登录将失效，单位分钟，默认720分钟（1个月）")]
        public long LoginExpirationTime { get; set; } = 720 * 60;

        /// <summary>
        ///     Gets or sets a value indicating whether [reg after login].
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "注册后自动登录")]
        [HelpBlock("")]
        public bool RegAfterLogin { get; set; } = true;

        /// <summary>
        ///     Gets or sets a value indicating whether [enable phone editing].
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1, EditShow = false)]
        [Display(Name = "会员中心启用手机号修改")]
        public bool EnablePhoneEditing { get; set; } = false;

        /// <summary>
        ///     Gets or sets a value indicating whether [edit shipping address].
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1, EditShow = false)]
        [Display(Name = "收货地址启动手机号修改")]
        public bool EditShippingAddress { get; set; } = false;

        /// <summary>
        ///     Gets or sets the after login URL.
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1)]
        [Display(Name = "登录成功以后跳转")]
        [HelpBlock("登录成功以后跳转")]
        public string AfterLoginUrl { get; set; } = "/User/Index";

        /// <summary>
        ///     会员中心显示层数
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1, EditShow = false)]
        [Display(Name = "会员中心显示层数")]
        public long ShowLevel { get; set; } = 3;

        /// <summary>
        ///     注册时需选择门店
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1, EditShow = true)]
        [Display(Name = "注册时需选择门店")]
        public bool NeedSelectServiceCenter { get; set; } = false;

        /// <summary>
        ///     Gets or sets a value indicating whether [need select parent 会员].
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1, EditShow = true)]
        [Display(Name = "注册时需选择推荐人")]
        public bool NeedSelectParentUser { get; set; } = false;

        /// <summary>
        ///     Gets or sets a value indicating whether [need phone vierfy code].
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1, EditShow = true)]
        [Display(Name = "注册时需手机验证码")]
        public bool NeedPhoneVierfyCode { get; set; } = true;

        /// <summary>
        /// 后台添加会员数据自动填充
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1, EditShow = true)]
        [Display(Name = "后台添加会员数据自动填充")]
        [HelpBlock("后台添加会员（Admi/User/Add)数据自动填充,可以加快测试速度")]
        public bool IsFixedData { get; set; } = false;

        /// <summary>
        ///     Sets the default.
        /// </summary>
        public void SetDefault() {
        }
    }
}