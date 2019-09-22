using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Domains.Enums
{
    /// <summary>
    ///     视图样式
    /// </summary>
    [ClassProperty(Name = "视图样式")]
    public enum ViewPageType
    {
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "编辑")]
        Edit = 1,

        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "列表")]
        List = 2,

        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "日期")]
        Data = 3
    }
}