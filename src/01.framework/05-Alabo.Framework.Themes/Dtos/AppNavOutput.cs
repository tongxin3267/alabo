using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Themes.Dtos {

    public class AppNavOutput {

        /// <summary>
        ///     所属应用
        /// </summary>
        public string App { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     链接
        /// </summary>
        public List<AppNavOutputLink> Links { get; set; }
    }

    /// <summary>
    ///     左侧导航链接
    /// </summary>
    public class AppNavOutputLink {

        /// <summary>
        ///     图标
        /// </summary>
        [Display(Name = "图标")]
        [Field(ListShow = true, EditShow = true, IsImagePreview = true, ControlsType = ControlsType.AlbumUploder,
            SortOrder = 1)]
        public string Icon { get; set; }

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
        ///     子导航链接
        /// </summary>
        public List<AppNavOutputLinkItem> SubLinks { get; set; }
    }

    /// <summary>
    /// </summary>
    public class AppNavOutputLinkItem {

        /// <summary>
        ///     图标
        /// </summary>
        [Display(Name = "图标")]
        [Field(ListShow = true, EditShow = true, IsImagePreview = true, ControlsType = ControlsType.AlbumUploder,
            SortOrder = 1)]
        public string Icon { get; set; }

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
    }
}