using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Cms.Articles.Domain.Enums {

    [ClassProperty(Name = "文章显示动作类型")]
    public enum ArticleState {

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "置顶")]
        Top = 0,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "推荐")]
        Recommend = 1,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "热门")]
        Hot = 2,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "跳转")]
        Jump = 3,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "幻灯片")]
        Slide = 4
    }
}