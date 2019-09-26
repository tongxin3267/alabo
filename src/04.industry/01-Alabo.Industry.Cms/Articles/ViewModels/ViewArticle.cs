using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.Industry.Cms.Articles.ViewModels {

    public class ViewArticle : BaseViewModel {
        [Display(Name = "编号")] public long Id { get; set; }

        [Display(Name = "标题")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Title { get; set; }

        [Display(Name = "URL链接")] public string LinkUrl { get; set; }

        [Display(Name = "排序数字")] public int SortOrder { get; set; }

        [Display(Name = "浏览次数")] public int ViewCount { get; set; }

        [Display(Name = "创建时间")] public DateTime CreateTime { get; set; }

        [Display(Name = "修改时间")] public DateTime ModifiedTime { get; set; }

        [Display(Name = "作者")] public string Author { get; set; }

        public Status Status { get; set; }

        /// <summary>
        ///     图片地址
        /// </summary>
        [Display(Name = "缩略图")]
        public string ImageUrl { get; set; }
    }
}