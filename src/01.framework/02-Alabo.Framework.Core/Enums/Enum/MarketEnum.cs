using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum
{
    [ClassProperty(Name = "货品类型")]
    public enum MarketEnum
    {
        /// <summary>
        ///     女装男装
        /// </summary>
        [Display(Name = "女装男装", Description = "blue")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699000")]
        Nznz = 0,

        /// <summary>
        ///     鞋类箱包
        /// </summary>
        [Display(Name = "鞋类箱包", Description = "blue")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699001")]
        Xlxb = 1,

        /// <summary>
        ///     母婴用品
        /// </summary>
        [Display(Name = "母婴用品", Description = "blue")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699002")]
        Myyp = 2,

        /// <summary>
        ///     护肤彩妆
        /// </summary>
        [Display(Name = "护肤彩妆", Description = "blue")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699003")]
        Fhcz = 3,

        /// <summary>
        ///     汇吃美食
        /// </summary>
        [Display(Name = "汇吃美食", Description = "blue")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699004")]
        Hcms = 4,

        /// <summary>
        ///     家装建材
        /// </summary>
        [Display(Name = "家装建材", Description = "blue")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699005")]
        Jzjc = 5,

        /// <summary>
        ///     珠宝配饰
        /// </summary>
        [Display(Name = "珠宝配饰", Description = "blue")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699006")]
        Zbps = 6,

        /// <summary>
        ///     家居家纺
        /// </summary>
        [Display(Name = "家居家纺", Description = "blue")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699007")]
        Jjjf = 7,

        /// <summary>
        ///     百货市场
        /// </summary>
        [Display(Name = "百货市场", Description = "blue")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699008")]
        Bhsc = 8,

        /// <summary>
        ///     汽车用品
        /// </summary>
        [Display(Name = "汽车用品", Description = "blue")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699009")]
        Qcyp = 9,

        /// <summary>
        ///     手机数码
        /// </summary>
        [Display(Name = "手机数码", Description = "blue")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699010")]
        Sjsm = 10,

        /// <summary>
        ///     家电办公
        /// </summary>
        [Display(Name = "家电办公", Description = "blue")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699011")]
        Jdbg = 11,

        /// <summary>
        ///     更多服务
        /// </summary>
        [Display(Name = "更多服务", Description = "blue")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699012")]
        Gdff = 12,

        /// <summary>
        ///     生活服务
        /// </summary>
        [Display(Name = "生活服务", Description = "blue")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699013")]
        Shfw = 13,

        /// <summary>
        ///     运动户外
        /// </summary>
        [Display(Name = "运动户外", Description = "blue")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699014")]
        Ydhw = 14,

        /// <summary>
        ///     花鸟文娱
        /// </summary>
        [Display(Name = "花鸟文娱", Description = "blue")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699015")]
        Hnwy = 15,

        /// <summary>
        ///     农资采购
        /// </summary>
        [Display(Name = "农资采购", Description = "blue")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699016")]
        Nzcg = 16,

        /// <summary>
        ///     自定义
        ///     适用于不使用真实货币交易的系统
        /// </summary>
        [Display(Name = "自定义")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Custom = -1
    }
}