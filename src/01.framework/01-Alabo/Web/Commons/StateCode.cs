using Alabo.Web.Mvc.Attributes;

namespace Alabo.Web.Commons
{
    /// <summary>
    ///     状态码
    /// </summary>
    [ClassProperty(Name = "状态码")]
    public enum StateCode
    {
        /// <summary>
        ///     成功
        /// </summary>
        Ok = 1,

        /// <summary>
        ///     失败
        /// </summary>
        Fail = 2
    }
}