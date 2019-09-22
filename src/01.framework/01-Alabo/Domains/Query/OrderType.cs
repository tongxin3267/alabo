using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Domains.Query
{
    /// <summary>
    ///     排序方式
    /// </summary>
    [ClassProperty(Name = "排序方式")]
    public enum OrderType
    {
        /// <summary>
        ///     倒序
        /// </summary>
        [Display(Name = "倒序")] Descending = 1,

        /// <summary>
        ///     升序
        /// </summary>
        [Display(Name = "升序")] Ascending = 2
    }
}