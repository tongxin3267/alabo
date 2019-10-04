using Alabo.Web.Mvc.Attributes;
using System.ComponentModel;

namespace Alabo.Web.Clients {

    /// <summary>
    ///     内容类型
    /// </summary>
    [ClassProperty(Name = "内容类型")]
    public enum HttpContentType {

        /// <summary>
        ///     application/x-www-form-urlencoded
        /// </summary>
        [Description("application/x-www-form-urlencoded")]
        FormUrlEncoded,

        /// <summary>
        ///     application/json
        /// </summary>
        [Description("application/json")] Json,

        /// <summary>
        ///     text/xml
        /// </summary>
        [Description("text/xml")] Xml
    }
}