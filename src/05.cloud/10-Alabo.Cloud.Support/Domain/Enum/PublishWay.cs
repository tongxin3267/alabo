using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Cloud.Support.Domain.Enum {

    [ClassProperty(Name = "公开类型")]
    public enum PublishWay {

        [LabelCssClass(BadgeColorCalss.Info)]
        [Display(Name = "私有")]
        Pri,

        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "公开")]
        Pub
    }
}