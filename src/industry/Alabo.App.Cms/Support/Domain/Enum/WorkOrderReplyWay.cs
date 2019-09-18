using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Cms.Support.Domain.Enum {

    /// <summary>
    ///     工单回复方式
    /// </summary>
    [ClassProperty(Name = "工单回复方式")]
    public enum WorkOrderReplyWay {

        /// <summary>
        ///     公开回复方式
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "公开回复方式")]
        PublicWay = 0,

        /// <summary>
        ///     私密回复方式
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "私密回复方式")]
        PrivateWay = 1
    }
}