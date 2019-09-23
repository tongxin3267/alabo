using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.Things.Brands.Domain.Entities {

    /// <summary>
    /// 店铺品牌
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Things_Brand")]
    public class Brand : AggregateMongodbRoot<Brand> {

        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, ListShow = true, SortOrder = 1)]
        [Display(Name = "品牌名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Name { get; set; }

        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, EditShow = true, SortOrder = 2)]
        [Display(Name = "网址")]
        public string Url { get; set; }

        [Display(Name = "品牌Logo")]
        [Field(ControlsType = ControlsType.AlbumUploder, GroupTabId = 1, SortOrder = 3)]
        public string Logo { get; set; }

        [Display(Name = "SEO标题")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 2, SortOrder = 4)]
        public string MetaTitle { get; set; }

        /// <summary>
        ///     SEO关键字
        /// </summary>
        [Display(Name = "SEO关键字")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 2, SortOrder = 5)]
        public string MetaKeywords { get; set; }

        /// <summary>
        ///     SEO描述
        /// </summary>
        [Display(Name = "SEO描述")]
        [Field(ControlsType = ControlsType.TextArea, GroupTabId = 2, SortOrder = 6)]
        public string MetaDescription { get; set; }

        /// <summary>
        ///     排序,越小排在越前面
        /// </summary>
        [Display(Name = "排序", Order = 1000)]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, GroupTabId = 3, SortOrder = 10000, Width = "110")]
        [Range(0, 99999, ErrorMessage = "请输入0-99999之间的数字")]
        [HelpBlock("排序,越小排在越前面，请输入0-99999之间的数字")]
        public long SortOrder { get; set; } = 1000;

        /// <summary>
        ///     通用状态 状态：0正常,1冻结,2删除
        ///     实体的软删除通过此字段来实现
        ///     软删除：指的是将实体标记为删除状态，不是真正的删除，可以通过回收站找回来
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.RadioButton, ListShow = true, SortOrder = 10003, GroupTabId = 3,
            Width = "110", DataSource = "Alabo.Domains.Enums.Status")]
        public Status Status { get; set; } = Status.Normal;

        /// <summary>
        ///     备注，此备注一般表示管理员备注，前台会员不可以修改
        /// </summary>
        [Display(Name = "备注")]
        [Field(ControlsType = ControlsType.TextArea, Row = 5, GroupTabId = 3, SortOrder = 10004)]
        public string Remark { get; set; }
    }
}