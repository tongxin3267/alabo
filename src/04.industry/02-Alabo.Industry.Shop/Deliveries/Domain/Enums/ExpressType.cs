using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.Deliveries.Domain.Enums {

    /// <summary>
    ///     快递来源
    ///     https://www.kuaidi100.com/all/index.shtml?from=newindex
    /// </summary>
    [ClassProperty(Name = "快递来源")]
    public enum ExpressType {

        /// <summary>
        ///     顺丰快递
        /// </summary>
        [Display(Name = "顺丰快递")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E0000000-1479-49BD-BFC7-E00000000000")]
        SF = 1,

        /// <summary>
        ///     申通快速
        /// </summary>
        [Display(Name = "申通快速")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E0000000-1479-49BD-BFC7-E00000000001")]
        Sto = 2,

        /// <summary>
        ///     圆通快递
        /// </summary>
        [Display(Name = "圆通快递")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E0000000-1479-49BD-BFC7-E00000000002")]
        Yto = 3,

        /// <summary>
        ///     中通快速
        /// </summary>
        [Display(Name = "中通快速")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E0000000-1479-49BD-BFC7-E00000000003")]
        Zto = 4,

        /// <summary>
        ///     韵达快速
        /// </summary>
        [Display(Name = "韵达快速")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E0000000-1479-49BD-BFC7-E00000000004")]
        Yd = 5,

        /// <summary>
        ///     中国邮政        ///
        /// </summary>
        [Display(Name = "中国邮政")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E0000000-1479-49BD-BFC7-E00000000005")]
        ChinaPost = 6,

        /// <summary>
        ///     EMS        ///
        /// </summary>
        [Display(Name = "EMS")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E0000000-1479-49BD-BFC7-E00000000006")]
        Ems = 7,

        /// <summary>
        ///     宅急送
        /// </summary>
        [Display(Name = "宅急送")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E0000000-1479-49BD-BFC7-E00000000007")]
        ZJs = 8,

        [Display(Name = "天天快速")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E0000000-1479-49BD-BFC7-E00000000008")]
        Tt = 250,

        [Display(Name = "德邦物流")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E0000000-1479-49BD-BFC7-E00000000009")]
        DeBang = 9,

        [Display(Name = "汇通快递")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E0000000-1479-49BD-BFC7-E00000000010")]
        Ht = 10,

        [Display(Name = "百世快递")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E0000000-1479-49BD-BFC7-E00000000011")]
        BeSt = 11,

        [Display(Name = "全峰快递")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E0000000-1479-49BD-BFC7-E00000000012")]
        Qf = 12,

        [Display(Name = "如风达快递")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E0000000-1479-49BD-BFC7-E00000000013")]
        Rfd = 13,

        [Display(Name = "城市100")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E0000000-1479-49BD-BFC7-E00000000014")]
        Cs = 14,

        [Display(Name = "品俊快递")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E0000000-1479-49BD-BFC7-E00000000015")]
        Pj = 15,

        [Display(Name = "宝凯物流")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E0000000-1479-49BD-BFC7-E00000000016")]
        Bk = 16,

        [Display(Name = "德中快递")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E0000000-1479-49BD-BFC7-E00000000017")]
        Dz = 17,

        [Display(Name = "苏宁快递")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E0000000-1479-49BD-BFC7-E000000000018")]
        Sn = 18,

        [Display(Name = "先锋快递")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E0000000-1479-49BD-BFC7-E00000000019")]
        Xf = 19,

        [Display(Name = "万通快递")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E0000000-1479-49BD-BFC7-E00000000020")]
        Wt = 20,

        [Display(Name = "AOL澳通速递")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E0000000-1479-49BD-BFC7-E00000000021")]
        At = 21,

        [Display(Name = "东方快递")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = false, GuidId = "E0000000-1479-49BD-BFC7-E00000000022")]
        Df = 22,

        [Display(Name = "上门自提")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E0000000-1479-49BD-BFC7-E00000000100")]
        Door = 100
    }
}