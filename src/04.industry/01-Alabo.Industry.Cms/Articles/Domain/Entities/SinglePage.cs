using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Industry.Cms.Articles.Domain.Entities {

    /// <summary>
    ///     单页面
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("CMS_SinglePage")]
    [ClassProperty(Name = "文章")]
    public class SinglePage : AggregateMongodbRoot<SinglePage> {

        /// <summary>
        ///     标识,页面标识，唯一
        ///     url通过此页面来访问
        /// </summary>
        [Display(Name = "页面标识")]
        [Required(ErrorMessage = ErrorMessage.NameNotCorrect)]
        public string Key { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotCorrect)]
        public string Name { get; set; }

        /// <summary>
        ///     页面内容
        /// </summary>
        [Display(Name = "内容")]
        [Required(ErrorMessage = ErrorMessage.NameNotCorrect)]
        public string Content { get; set; }

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

        [Display(Name = "状态")]
        public Status Status { get; set; }
    }
}