using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Cms.Articles.Domain.Entities {

    /// <summary>
    ///     专题
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("CMS_Special")]
    [ClassProperty(Name = "专题")]
    public class Special : AggregateMongodbRoot<Special> {

        /// <summary>
        ///     标识,专题标识，唯一
        ///     url通过此专题来访问
        ///     通过该URl来访问页面地址
        /// </summary>
        [Display(Name = "专题标识")]
        [Required(ErrorMessage = ErrorMessage.NameNotCorrect)]
        public string Key { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotCorrect)]
        public string Name { get; set; }

        /// <summary>
        ///     浏览次数
        /// </summary>
        [Display(Name = "浏览次数")]
        public int ViewCount { get; set; } = 0;

        /// <summary>
        ///     SEO标题
        /// </summary>
        [Display(Name = "SEO标题")]
        [StringLength(200, ErrorMessage = "Seo标题长度不能超过200个字符")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextBox)]
        public string MetaTitle { get; set; }

        /// <summary>
        ///     SEO关键字
        /// </summary>
        /// </summary>
        [Display(Name = "SEO关键字")]
        [StringLength(300, ErrorMessage = "SEO关键字长度不能超过300个字符")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextBox)]
        public string MetaKeywords { get; set; }

        /// <summary>
        ///     SEO描述
        /// </summary>
        [Display(Name = "SEO描述")]
        [StringLength(400, ErrorMessage = "SEO描述长度不能超过400个字符")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextArea)]
        public string MetaDescription { get; set; }
    }
}