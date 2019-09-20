using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Domains.Enums
{
    /// <summary>
    ///     状态
    /// </summary>
    [ClassProperty(Name = "状态")]
    public enum Status
    {
        /// <summary>
        ///     正常
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "正常")] [Field(Icon = "la la-check-circle-o")]
        Normal = 1,

        /// <summary>
        ///     冻结
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)] [Display(Name = "冻结")] [Field(Icon = "la la-lock")]
        Freeze = 2,

        /// <summary>
        ///     软删除，不是真正的删除，只是在数据库标记
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)] [Display(Name = "删除")] [Field(Icon = "fa fa-trash-o")]
        Deleted = 3
    }
}