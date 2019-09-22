using System;

namespace Alabo.Web.Mvc.Attributes
{
    /// <summary>
    ///     显示为标签文本时使用的css类
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class LabelCssClassAttribute : Attribute
    {
        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="cssClass">The CSS class.</param>
        public LabelCssClassAttribute(string cssClass)
        {
            CssClass = cssClass;
        }

        /// <summary>
        ///     css类
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is data field.
        /// </summary>
        public bool IsDataField { get; set; } = false;
    }

    /// <summary>
    ///     Class BadgeColorCalss.
    /// </summary>
    public class BadgeColorCalss
    {
        /// <summary>
        ///     The default
        /// </summary>
        public const string Default = "m-badge--default";

        /// <summary>
        ///     The success
        /// </summary>
        public const string Success = "m-badge--success";

        /// <summary>
        ///     The information
        /// </summary>
        public const string Info = "m-badge--info";

        /// <summary>
        ///     The danger
        /// </summary>
        public const string Danger = "m-badge--danger";

        /// <summary>
        ///     The warning
        /// </summary>
        public const string Warning = "m-badge--warning";

        /// <summary>
        ///     The brand
        /// </summary>
        public const string Brand = "m-badge--brand";

        /// <summary>
        ///     The focus
        /// </summary>
        public const string Focus = "m-badge--focus";

        /// <summary>
        ///     The accent
        /// </summary>
        public const string Accent = "m-badge--accent";

        /// <summary>
        ///     The metal
        /// </summary>
        public const string Metal = "m-badge--metal";

        /// <summary>
        ///     The primary
        /// </summary>
        public const string Primary = "m-badge--primary";
    }
}