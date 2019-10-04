using Alabo.Web.Mvc.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum {

    /// <summary>
    ///     语言
    /// </summary>
    [ClassProperty(Name = "语言")]
    public enum Languages {

        /// <summary>
        ///     中文
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "Chinese")]
        [LanguageCode("zh-CN")]
        Chinese,

        /// <summary>
        ///     英语
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "English")]
        [LanguageCode("en-US")]
        English,

        /// <summary>
        ///     法语
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "French")]
        [LanguageCode("fr-FR")]
        French,

        /// <summary>
        ///     德语
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "German")]
        [LanguageCode("de-DE")]
        German,

        /// <summary>
        ///     日语
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "Japanese")]
        [LanguageCode("ja-JP")]
        Japanese,

        /// <summary>
        ///     韩语
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "Korean")]
        [LanguageCode("ko-KR")]
        Korean,

        /// <summary>
        ///     西班牙语
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "Spanish")]
        [LanguageCode("es-ES")]
        Spanish,

        /// <summary>
        ///     泰语
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "Thai")]
        [LanguageCode("th-TH")]
        Thai,

        /// <summary>
        ///     繁体中文
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "TraditionalChinese")]
        [LanguageCode("zh-TW")]
        TraditionalChinese,

        /// <summary>
        ///     俄语
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "Russian")]
        [LanguageCode("ru-RU")]
        Russian,

        /// <summary>
        ///     意大利语
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "Italian")]
        [LanguageCode("it-IT")]
        Italian,

        /// <summary>
        ///     希腊语
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "Greek")]
        [LanguageCode("el-GR")]
        Greek,

        /// <summary>
        ///     阿拉伯语
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "Arabic")]
        [LanguageCode("ar-DZ")]
        Arabic,

        /// <summary>
        ///     波兰语
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "Polish")]
        [LanguageCode("pl-PL")]
        Polish,

        /// <summary>
        ///     捷克语
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "Czech")]
        [LanguageCode("cs-CZ")]
        Czech
    }

    /// <summary>
    ///     语言代码的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class LanguageCodeAttribute : Attribute {

        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="code"></param>
        public LanguageCodeAttribute(string code) {
            Code = code;
        }

        /// <summary>
        ///     语言代码
        /// </summary>
        public string Code { get; set; }
    }
}