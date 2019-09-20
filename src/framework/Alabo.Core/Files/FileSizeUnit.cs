using System.ComponentModel;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Core.Files
{
    /// <summary>
    ///     文件大小单位
    /// </summary>
    [ClassProperty(Name = "文件大小单位")]
    public enum FileSizeUnit
    {
        /// <summary>
        ///     字节
        /// </summary>
        [Description("B")] Byte,

        /// <summary>
        ///     K字节
        /// </summary>
        [Description("KB")] K,

        /// <summary>
        ///     M字节
        /// </summary>
        [Description("MB")] M,

        /// <summary>
        ///     G字节
        /// </summary>
        [Description("GB")] G
    }
}