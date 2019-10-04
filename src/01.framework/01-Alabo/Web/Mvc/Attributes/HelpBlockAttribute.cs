using System;

namespace Alabo.Web.Mvc.Attributes {

    /// <summary>
    ///     文本框的帮助属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class HelpBlockAttribute : Attribute {

        /// <summary>
        ///     初始化
        /// </summary>
        public HelpBlockAttribute(string helpText) {
            HelpText = helpText;
        }

        /// <summary>
        ///     帮助文档
        /// </summary>
        public string HelpText { get; set; }
    }
}