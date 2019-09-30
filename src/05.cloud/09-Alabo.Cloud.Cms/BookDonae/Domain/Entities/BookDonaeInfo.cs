using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.WebUis;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Cloud.Cms.BookDonae.Domain.Entities
{
    /// <summary>
    ///     183.60.143.29
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Books_BookDonaeInfo")]
    [ClassProperty(Name = "书籍管理", Description = "书籍管理", Icon = IconFlaticon.route)]
    public class BookDonaeInfo : AggregateMongodbRoot<BookDonaeInfo>
    {
        /// <summary>
        ///     名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, Width = "300", ListShow = true, EditShow = true,
            IsShowBaseSerach = true, SortOrder = 3)]
        public string Name { get; set; }

        /// <summary>
        ///     分类
        /// </summary>

        public ObjectId ClassId { get; set; }

        /// <summary>
        ///     分类名称
        /// </summary>
        [Display(Name = "分类名称")]
        [Field(ControlsType = ControlsType.TextBox, Width = "300", ListShow = true, EditShow = true,
            IsShowBaseSerach = true, SortOrder = 3)]
        public string ClassName { get; set; }

        /// <summary>
        ///     地址
        /// </summary>
        [Display(Name = "下载地址")]
        [Field(ControlsType = ControlsType.TextBox, Width = "300", ListShow = true, EditShow = true,
            IsShowBaseSerach = true, SortOrder = 3)]
        public string Url { get; set; }

        /// <summary>
        ///     是否是销售状态
        /// </summary>
        [Display(Name = "是否是销售状态")]
        [Field(ControlsType = ControlsType.Switch, Width = "300", ListShow = true, EditShow = true,
            IsShowBaseSerach = true, SortOrder = 3)]
        public bool IsOnSale { get; set; } = true;

        /// <summary>
        ///     排序
        /// </summary>
        [Display(Name = "排序")]
        [Required(ErrorMessage = "排序")]
        [Field(ControlsType = ControlsType.Numberic, Width = "100", EditShow = true, ListShow = true, SortOrder = 6)]
        public long SortOrder { get; set; } = 1000;
    }
}