using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.WebUis;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Cloud.Cms.BookDonae.Domain.Entities
{
    /// <summary>
    ///     书籍分类
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Books_Class")]
    [ClassProperty(Name = "书籍分类", Description = "书籍分类", Icon = IconFlaticon.route)]
    public class BooksClass : AggregateMongodbRoot<BooksClass>
    {
        /// <summary>
        ///     分类名称
        /// </summary>
        [Display(Name = "分类名称")]
        [Required(ErrorMessage = "分类名称")]
        [Field(ControlsType = ControlsType.TextBox, Width = "300", ListShow = true, EditShow = true,
            IsShowBaseSerach = true, SortOrder = 3)]
        public string Name { get; set; }

        /// <summary>
        ///     排序
        /// </summary>
        [Display(Name = "排序")]
        [Required(ErrorMessage = "排序")]
        [Field(ControlsType = ControlsType.Numberic, Width = "100", EditShow = true, ListShow = true, SortOrder = 6)]
        public long SortOrder { get; set; } = 1000;

        /// <summary>
        ///     服务器网址
        /// </summary>
        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "服务器网址")]
        [Required(ErrorMessage = "服务器网址")]
        [Field(ControlsType = ControlsType.TextBox, Width = "300", ListShow = true, EditShow = true,
            IsShowBaseSerach = true, SortOrder = 3)]
        public string Host { get; set; }
    }
}