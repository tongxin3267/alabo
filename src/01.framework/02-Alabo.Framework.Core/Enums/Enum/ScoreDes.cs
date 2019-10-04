using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum {

    [ClassProperty(Name = "评分类型")]
    public enum ScoreDes {

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "排斥")]
        排斥 = 1,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "失望")]
        失望 = 2,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "一般")]
        一般 = 3,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "满意")]
        满意 = 4,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "推荐")]
        推荐 = 5
    }
}