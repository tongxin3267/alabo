// ReSharper disable InconsistentNaming

using Alabo.Web.Mvc.Attributes;

namespace Alabo.Domains.Enums
{
    /// <summary>
    ///     背景颜色
    /// </summary>
    [ClassProperty(Name = "背景颜色")]
    public enum BackgroudColor
    {
        /// <summary>
        ///     The bg success
        /// </summary>
        bg_success = 1,

        /// <summary>
        ///     The bg blue
        /// </summary>
        bg_blue = 2,

        /// <summary>
        ///     The bg blue dark
        /// </summary>
        bg_blue_dark = 3,

        /// <summary>
        ///     The bg green
        /// </summary>
        bg_green = 101,

        /// <summary>
        ///     The bg green meadow
        /// </summary>
        bg_green_meadow,

        /// <summary>
        ///     The bg red
        /// </summary>
        bg_red = 102,

        /// <summary>
        ///     The bg yellow
        /// </summary>
        bg_yellow = 103,

        /// <summary>
        ///     The bg dark
        /// </summary>
        bg_dark = 105,

        /// <summary>
        ///     The bg danger
        /// </summary>
        bg_danger,

        /// <summary>
        ///     The bg warning
        /// </summary>
        bg_warning,

        /// <summary>
        ///     The bg primary
        /// </summary>
        bg_primary = 110,

        /// <summary>
        ///     The bg purple
        /// </summary>
        bg_purple
    }
}