using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Cms.Articles.Domain.Enums {

    /// <summary>
    /// 排序方式
    /// </summary>
    [ClassProperty(Name = "排序方式")]
    public enum ArticleSortOrder {

        /// <summary>
        ///     根据排序号排序，默认排序
        ///     排序号越小越在前面
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "根据排序号排序，默认排序")]
        SortOrder = 0,

        /// <summary>
        ///     添加时间
        ///     时间越晚，在前面
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "添加时间")]
        CreateTime = 1,

        /// <summary>
        ///     查看次数
        ///     查看次数多的在前面
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "查看次数")]
        ViewCount = 2,
    }
}