using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Validations;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.Industry.Cms.Articles.ViewModels {

    /// <summary>
    /// </summary>
    public class ArticleItem : BaseViewModel {

        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        /// <value>
        ///     Id标识
        /// </value>
        [Display(Name = "编号")]
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        /// <value>
        ///     The title.
        /// </value>
        [Display(Name = "标题")]
        [Required(ErrorMessage = ErrorMessage.NameNotCorrect)]
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets the sub title.
        /// </summary>
        /// <value>
        ///     The sub title.
        /// </value>
        [Display(Name = "副标题")]
        public string SubTitle { get; set; }

        /// <summary>
        ///     Gets or sets the link URL.
        /// </summary>
        /// <value>
        ///     The link URL.
        /// </value>
        [Display(Name = "URL链接")]
        public string LinkUrl { get; set; }

        /// <summary>
        ///     Gets or sets the view count.
        /// </summary>
        /// <value>
        ///     The view count.
        /// </value>
        [Display(Name = "浏览次数")]
        public int ViewCount { get; set; }

        /// <summary>
        ///     Gets or sets the create time.
        /// </summary>
        /// <value>
        ///     The create time.
        /// </value>
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     图片地址
        /// </summary>
        /// <value>
        ///     The image URL.
        /// </value>
        [Display(Name = "缩略图")]
        public string ImageUrl { get; set; }

        /// <summary>
        ///     Gets or sets the author.
        /// </summary>
        /// <value>
        ///     The author.
        /// </value>
        [Display(Name = "作者")]
        public string Author { get; set; }

        /// <summary>
        ///     Gets or sets the relation identifier.
        /// </summary>
        /// <value>
        ///     The relation identifier.
        /// </value>
        [Display(Name = "级联ID")]
        public long RelationId { get; set; }
    }
}