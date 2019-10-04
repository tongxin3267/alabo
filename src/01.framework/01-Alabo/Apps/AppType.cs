using Alabo.Web.Mvc.Attributes;

/// <summary>
/// </summary>
namespace Alabo.Apps {

    /// <summary>
    ///     Enum AppType
    /// </summary>
    [ClassProperty(Name = "APPType")]
    public enum AppType {

        /// <summary>
        ///     The dynamic
        /// </summary>
        Dynamic = 1,

        /// <summary>
        ///     The binary
        /// </summary>
        Binary
    }
}