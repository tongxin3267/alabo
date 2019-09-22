using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Cms.Support.Domain.ViewModels {

    public class UserWordOrder {

        [Display(Name = "标题")]
        [Required(ErrorMessage = ErrorMessage.MaxStringLength)]
        [StringLength(50)]
        public string Title { get; set; }

        [Display(Name = "问题描述")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Description { get; set; }

        [Display(Name = "工单发起人")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long UserId { get; set; }

        [Display(Name = "支付密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string PayPassword { get; set; }

        /// <summary>
        ///     分类
        /// </summary>
        [Display(Name = "分类")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public int ClassId { get; set; }
    }

    [ClassProperty(Name = "意见反馈", Icon = "fa fa-puzzle-piece", Description = "意见反馈",
        PostApi = "Api/User/GetPasswordForm", SuccessReturn = "Api/User/ChangePassword")]
    public class UserWordOrderInput : EntityDto {

        [Display(Name = "标题")]
        [Required(ErrorMessage = ErrorMessage.MaxStringLength)]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 1)]
        [StringLength(50)]
        public string Title { get; set; }

        [Display(Name = "意见")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextArea, SortOrder = 3)]
        public string Description { get; set; }
    }
}