using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Cms.Articles.Domain.Services;
using Alabo.App.Core.Common.Domain.CallBacks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Cms.Articles.Domain.Entities {

    /// <summary>
    ///     文章类
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("CMS_Article")]
    [ClassProperty(Name = "文章", Icon = "fa fa-puzzle-piece", SideBarType = SideBarType.ArticleSideBarSideBar)]
    public class Article : AggregateMongodbUserRoot<Article> {

        /// <summary>
        ///     级联Id
        /// </summary>
        [Display(Name = "级联Id")]
        public long RelationId { get; set; }

        /// <summary>
        ///     频道ID
        /// </summary>
        /// <value>The channel identifier.</value>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Display(Name = "频道ID")]
        public ObjectId ChannelId { get; set; }

        /// <summary>
        ///     标题
        /// </summary>
        /// <value>The title.</value>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Display(Name = "标题")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true, IsMain = true,
            ListShow = true, SortOrder = 2)]
        public string Title { get; set; }

        /// <summary>
        ///     副标题
        /// </summary>
        /// <value>The sub title.</value>
        [Display(Name = "副标题")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true,
            ListShow = true, SortOrder = 2)]
        public string SubTitle { get; set; }

        /// <summary>
        ///     简介
        /// </summary>
        /// <value>The intro.</value>
        [Display(Name = "简介")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = false, IsShowBaseSerach = false,
            ListShow = true, SortOrder = 2)]
        public string Intro { get; set; }

        /// <summary>
        ///     详细内容
        /// </summary>
        /// <value>The content.</value>
        [Display(Name = "详细内容")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = false, IsShowBaseSerach = false,
            ListShow = false, SortOrder = 2)]
        public string Content { get; set; }

        /// <summary>
        ///     CMS的附加内容，根据频道返回JSON格式的数据
        /// </summary>
        /// <value>The attach value.</value>
        [Display(Name = "附加内容")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = false, IsShowBaseSerach = false,
            ListShow = false, SortOrder = 2)]
        public string AttachValue { get; set; }

        /// <summary>
        ///     来源
        /// </summary>
        /// <value>The source.</value>
        [Display(Name = "来源")]
        public string Source { get; set; }

        /// <summary>
        ///     作者
        /// </summary>
        /// <value>The author.</value>
        [Display(Name = "作者")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true,
            ListShow = true, SortOrder = 3)]
        public string Author { get; set; }

        /// <summary>
        ///     外部链接
        /// </summary>
        /// <value>The link URL.</value>
        [Display(Name = "外部链接")]
        public string LinkUrl { get; set; }

        /// <summary>
        ///     图片地址
        /// </summary>
        /// <value>The image URL.</value>
        [Display(Name = "缩略图")]
        [Field(ControlsType = ControlsType.ImagePreview, IsShowAdvancedSerach = false, IsShowBaseSerach = false,
            ListShow = true, SortOrder = 1)]
        public string ImageUrl { get; set; }

        /// <summary>
        ///     浏览次数
        /// </summary>
        /// <value>The view count.</value>
        [Display(Name = "浏览次数")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowAdvancedSerach = false, IsShowBaseSerach = false,
            ListShow = true, EditShow = false, SortOrder = 4)]
        public int ViewCount { get; set; }

        /// <summary>
        ///     文章跳转、置顶、热门等功能
        /// </summary>
        /// <value>The state of the article.</value>
        [Display(Name = "属性")]
        public string ArticleState { get; set; }

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

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.DropdownList, DataSource = " Alabo.Domains.Enums.Status", Width = "100",
            IsShowAdvancedSerach = true, IsShowBaseSerach = true, ListShow = true, SortOrder = 5)]
        public Status Status { get; set; } = Status.Normal;

        /// <summary>
        ///     字段列表
        /// </summary>
        /// <value>The data fields.</value>
        [BsonIgnore]
        [Display(Name = "字段列表")]
        public List<DataField> DataFields { get; set; }

        /// <summary>
        ///     分类
        /// </summary>
        /// <value>The classes.</value>
        [BsonIgnore]
        [Display(Name = "分类")]
        public string Classes { get; set; }

        /// <summary>
        ///     标签
        /// </summary>
        /// <value>The tags.</value>
        [Display(Name = "标签")]
        public string Tags { get; set; }
    }
}