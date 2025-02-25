﻿using Alabo.Web.Mvc.Attributes;
using System.ComponentModel;

namespace Alabo.UI.Enum {

    /// <summary>
    ///     图标大小
    /// </summary>
    [ClassProperty(Name = "图标大小")]
    public enum IconSize {

        /// <summary>
        ///     1.3倍
        /// </summary>
        [Description("fa-lg")] Large,

        /// <summary>
        ///     2倍
        /// </summary>
        [Description("fa-2x")] Large2X,

        /// <summary>
        ///     3倍
        /// </summary>
        [Description("fa-3x")] Large3X,

        /// <summary>
        ///     4倍
        /// </summary>
        [Description("fa-4x")] Large4X,

        /// <summary>
        ///     5倍
        /// </summary>
        [Description("fa-5x")] Large5X
    }
}