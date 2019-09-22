using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Cms.Articles.Domain.Entities {

    /// <summary>
    ///     关于我们
    /// </summary>
    [ClassProperty(Name = "关于我们", Icon = "fa fa-building", Description = "关于我们", GroupName = "基本设置,搜索引擎优化,高级选项",
        PageType = ViewPageType.List, SortOrder = 20, SideBarType = SideBarType.CustomerServiceSideBar
    )]
    [BsonIgnoreExtraElements]
    [Table("CMS_About")]
    public class About : AggregateMongodbRoot<About> {

        /// <summary>
        ///     名称
        /// </summary>
        /// <value>The name.</value>
        [Display(Name = "名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotCorrect)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Field(ListShow = true, ControlsType = ControlsType.TextBox, GroupTabId = 1)]
        public string Name { get; set; }

        /// <summary>
        ///     内容详情
        /// </summary>
        /// <value>The content.</value>
        [Display(Name = "内容详情")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.Editor, GroupTabId = 1)]
        public string Content { get; set; }

        /// <summary>
        ///     SEO标题
        /// </summary>
        /// <value>The meta title.</value>
        [Display(Name = "SEO标题")]
        [StringLength(200, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextBox, GroupTabId = 2)]
        public string MetaTitle { get; set; }

        /// <summary>
        ///     SEO关键字
        /// </summary>
        /// <value>The meta keywords.</value>
        [Display(Name = "SEO关键字")]
        [StringLength(300, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextBox, GroupTabId = 2)]
        public string MetaKeywords { get; set; }

        /// <summary>
        ///     SEO描述
        /// </summary>
        /// <value>The meta description.</value>
        [Display(Name = "SEO描述")]
        [StringLength(400, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextArea, GroupTabId = 2)]
        public string MetaDescription { get; set; }
    }
}