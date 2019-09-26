using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Core.Enums.Enum
{
    [ClassProperty(Name = "货币类型")]
    public enum Currency
    {
        /// <summary>
        ///     人民币
        ///     Selection 表示颜色
        /// </summary>
        [Display(Name = "现金账户", Description = "blue")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699000", Mark = "元", SortOrder = 1,
            Selection = "Red")]
        Cny = 0,

        /// <summary>
        ///     自定义
        ///     适用于不使用真实货币交易的系统
        /// </summary>
        [Display(Name = "自定义")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699000", Mark = "元")]
        Custom = -1,

        /// <summary>
        ///     积分
        /// </summary>
        [Display(Name = "积分账户")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699002", Mark = "积分", Selection = "Blue")]
        Point = 2,

        /// <summary>
        ///     虚拟币
        /// </summary>
        [Display(Name = "虚拟账户")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699003", Mark = "币", Selection = "Purple")]
        Virtual = 3,

        /// <summary>
        ///     股权
        /// </summary>
        [Display(Name = "股权账户")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699004", Mark = "股权", Selection = "Green")]
        Equity = 4,

        /// <summary>
        ///     授信
        /// </summary>
        [Display(Name = "授信账户")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699005", Mark = "授信")]
        Credit = 5,

        /// <summary>
        ///     会员升级使用
        /// </summary>
        [Display(Name = "升级点")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699006", Mark = "升级点", Selection = "Red")]
        UpgradePoints = 6,

        /// <summary>
        ///     慈善账户
        /// </summary>
        [Display(Name = "慈善账户")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699007", Mark = "慈善", Selection = "Green")]
        Charity = 7,

        /// <summary>
        ///     红包
        /// </summary>
        [Display(Name = "红包")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699008", Mark = "红包")]
        RedPacket = 8,

        /// <summary>
        ///     抵用券
        /// </summary>
        [Display(Name = "抵用券")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699009", Mark = "抵用券")]
        Voucher = 9,

        /// <summary>
        ///     美元
        /// </summary>
        [Display(Name = "美元")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699840", Mark = "美元")]
        Usd = 840,

        /// <summary>
        ///     欧元
        /// </summary>
        [Display(Name = "欧元")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699978", Mark = "欧元")]
        Eur = 978,

        /// <summary>
        ///     日元
        /// </summary>
        [Display(Name = "日元")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699392", Mark = "日元")]
        Jpy = 392,

        /// <summary>
        ///     英镑
        /// </summary>
        [Display(Name = "英镑")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699826", Mark = "英镑")]
        Gbp = 826,

        /// <summary>
        ///     港元
        /// </summary>
        [Display(Name = "港元")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699344", Mark = "港元")]
        Hkd = 344,

        /// <summary>
        ///     新台币
        /// </summary>
        [Display(Name = "数字资产")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699901", Mark = "资产", Selection = "Dark")]
        DigitalAssets = 901,

        /// <summary>
        ///     奖金账户
        /// </summary>
        [Display(Name = "奖金账户")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699410", Mark = "元")]
        Bonus = 410,

        /// <summary>
        ///     充值账户
        /// </summary>
        [Display(Name = "充值账户")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699643", Mark = "元")]
        Recharge = 643,

        /// <summary>
        ///     提现账户
        /// </summary>
        [Display(Name = "提现账户")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699756", Mark = "元", Selection = "Purple")]
        Withdrawal = 756,

        /// <summary>
        ///     基金账户
        /// </summary>
        [Display(Name = "基金账户")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-40BD-BFC7-E73A5D699301", Mark = "基金", Selection = "Dark")]
        Fund = 301,

        /// <summary>
        ///     增值账户
        /// </summary>
        [Display(Name = "增值账户")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E97CCD1E-1478-40BD-BFC7-E73A5D699302", Mark = "增值")]
        Increment = 302,

        /// <summary>
        ///     共享账户
        /// </summary>
        [Display(Name = "共享账户")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E97CCD1E-1478-40BD-BFC7-E73A5D699302", Mark = "值")]
        SharedAccount = 305,

        /// <summary>
        ///     白条
        /// </summary>
        [Display(Name = "白条账户")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E97CCD1E-1478-40BD-BFC7-E73A5D699300", Mark = "白条")]
        BaiTiao = 300
    }
}