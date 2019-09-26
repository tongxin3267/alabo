using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Core.Enums.Enum {

    [ClassProperty(Name = "地区类型")]
    public enum RegionLevel {

        /// <summary>
        ///     国家
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Info)]
        [Display(Name = "国家")]
        Country = 1,

        /// <summary>
        ///     省份
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Brand)]
        [Display(Name = "省份")]
        Province = 2,

        /// <summary>
        ///     城市
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "城市")]
        City = 3,

        /// <summary>
        ///     区县
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "区县")]
        County = 4
    }
}