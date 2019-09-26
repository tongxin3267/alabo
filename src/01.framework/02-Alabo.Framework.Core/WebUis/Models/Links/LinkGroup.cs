using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Core.WebUis.Models.Links {

    [ClassProperty(Name = "链接")]
    public class LinkGroup : BaseComponent {

        public LinkGroup() {
        }

        public LinkGroup(string name, string url, string image) {
            Name = name;
            Url = url;
            Image = image;
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
        ///     分组链接
        /// </summary>
        public List<Link> Links { get; set; } = new List<Link>();
    }
}