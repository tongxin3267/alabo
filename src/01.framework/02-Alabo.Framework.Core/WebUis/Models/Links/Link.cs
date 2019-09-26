using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Alabo.Framework.Core.WebUis.Models.Links
{
    /// <summary>
    ///     链接、支持图片、链接、广告等
    /// </summary>
    [ClassProperty(Name = "链接")]
    public class Link : BaseComponent
    {
        public Link()
        {
            Id = ObjectId.GenerateNewId();
        }

        public Link(string name, string url, long sortOrder, string image)
        {
            Name = name;
            Url = url;
            SortOrder = sortOrder;
            Image = image;
            Id = ObjectId.GenerateNewId();
        }

        /// <summary>
        ///     图标
        /// </summary>
        [Display(Name = "图片")]
        [Field(ListShow = true, EditShow = true, IsImagePreview = true, ControlsType = ControlsType.AlbumUploder,
            SortOrder = 1)]
        public string Image { get; set; }

        /// <summary>
        ///     链接名称
        /// </summary>
        [Display(Name = "链接名称")]
        [Field(ListShow = true, EditShow = true, ControlsType = ControlsType.TextBox, SortOrder = 2)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(20, ErrorMessage = ErrorMessage.MaxStringLength)]
        public string Name { get; set; }

        /// <summary>
        ///     链接URL
        /// </summary>
        [Display(Name = "URL")]
        [Field(ListShow = true, EditShow = true, ControlsType = ControlsType.TextBox, SortOrder = 3)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Url { get; set; }

        /// <summary>
        ///     说明
        /// </summary>
        [Display(Name = "说明介绍")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextBox, SortOrder = 4)]
        [StringLength(50, ErrorMessage = ErrorMessage.MaxStringLength)]
        [JsonIgnore]
        public string Intro { get; set; }

        /// <summary>
        ///     排序,越小排在越前面
        /// </summary>
        [Display(Name = "排序", Order = 1000)]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 10000,
            Width = "110")]
        [Range(0, 99999, ErrorMessage = "请输入0-99999之间的数字")]
        [HelpBlock("排序,越小排在越前面，请输入0-99999之间的数字")]
        public long SortOrder { get; set; } = 1000;
    }
}