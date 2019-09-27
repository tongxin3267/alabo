using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Newtonsoft.Json;

namespace Alabo.Framework.Core.WebUis.Design.AutoImages
{
    /// <summary>
    ///     通用图片
    /// </summary>
    public class AutoImage
    {
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
    }
}