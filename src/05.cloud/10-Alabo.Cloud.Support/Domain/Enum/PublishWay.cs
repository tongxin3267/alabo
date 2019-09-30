using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Cloud.Support.Domain.Enum
{
    [ClassProperty(Name = "公开类型")]
    public enum PublishWay
    {
        [LabelCssClass(BadgeColorCalss.Info)]
        [Display(Name = "私有")]
        Pri,

        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "公开")]
        Pub
    }
}