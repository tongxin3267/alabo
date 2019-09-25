using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.UI.AutoVideos
{
    public class AutoVideo
    {
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
        ///     远程连接视图地址
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