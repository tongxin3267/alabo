using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum {

    /// <summary>
    ///     商品范围
    /// </summary>
    [ClassProperty(Name = "商品范围")]
    public enum ProductRange {

        /// <summary>
        ///     所有商品
        /// </summary>
        [Display(Name = "所有商品")] All = 1,

        /// <summary>
        ///     所属商城
        /// </summary>
        [Display(Name = "所属商城")] Subordinate = 2,

        /// <summary>
        ///     所属商品线
        /// </summary>
        [Display(Name = "所属商品线")] ProductLine = 3,

        /// <summary>
        ///     自定义活动商品()
        /// </summary>
        [Display(Name = "指定活动商品")] Customer = 4
    }
}