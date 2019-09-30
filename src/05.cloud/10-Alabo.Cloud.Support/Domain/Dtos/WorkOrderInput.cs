using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Cloud.Support.Domain.Dtos
{
    /// <summary>
    /// </summary>
    [ClassProperty(Name = "工单系统")]
    public class WorkOrderInput : ApiInputDto
    {
        /// <summary>
        /// </summary>
        [Required(ErrorMessage = "描述不能为空")]
        [Display(Name = "问题描述")]
        [Field(ControlsType = ControlsType.TextArea)]
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        public string Attachment { get; set; }

        /// <summary>
        /// </summary>
        public string ClassId { get; set; }

        /// <summary>
        /// </summary>
        [Field(ControlsType = ControlsType.AlbumUploder)]
        [Display(Name = "图片描述")]
        [HelpBlock("可上传发生问题的图片")]
        public string Image { get; set; }
    }
}