using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Cloud.People.Relationship.Domain.Enums
{
    /// <summary>
    ///     关系图类型
    /// </summary>
    [ClassProperty(Name = "关系图类型")]
    public enum RelationshipIndexType
    {
        /// <summary>
        ///     内部合伙人
        /// </summary>
        [Display(Name = "内部合伙人(一)")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-0000-1111-1111-E73A5D600001")]
        InternalPartners = 1,

        /// <summary>
        ///     内部合伙人(二)
        /// </summary>
        [Display(Name = "内部合伙人(二)")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699002")]
        InternalPartners2 = 2,

        /// <summary>
        ///     内部合伙人(二)
        /// </summary>
        [Display(Name = "内部合伙人(三)")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699003")]
        InternalPartners3 = 3,

        /// <summary>
        ///     内部合伙人
        /// </summary>
        [Display(Name = "高等级可平级为推荐人(一)")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-0000-1111-1111-E73A5D600004")]
        HighevelRecommended1 = 4,

        /// <summary>
        ///     内部合伙人(二)
        /// </summary>
        [Display(Name = "高等级可平级为推荐人(二)")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699005")]
        HighevelRecommended2 = 5,

        /// <summary>
        ///     内部合伙人(二)
        /// </summary>
        [Display(Name = "高等级可平级为推荐人(三)")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D699006")]
        HighevelRecommended3 = 6
    }
}