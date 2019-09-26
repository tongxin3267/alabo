using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Cloud.Support.Domain.Enum;
using Alabo.Domains.Enums;
using Alabo.Users.Entities;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.Cloud.Support.Domain.ViewModels
{
    public class ViewWorkOrder : BaseViewModel
    {
        [Display(Name = "标题")]
        [Required(ErrorMessage = ErrorMessage.NameNotCorrect)]
        public string Title { get; set; }

        [Display(Name = "问题描述")]
        [Required(ErrorMessage = ErrorMessage.NameNotCorrect)]
        public string Description { get; set; }

        [Display(Name = "工单发起人")]
        [Required(ErrorMessage = ErrorMessage.NameNotCorrect)]
        public long UserId { get; set; }

        public long AcceptUserId { get; set; }

        [Display(Name = "工单状态")]
        [Required(ErrorMessage = ErrorMessage.NameNotCorrect)]
        public WorkOrderState State { get; set; }

        [Display(Name = "工单类型")]
        [Required(ErrorMessage = ErrorMessage.NameNotCorrect)]
        public WorkOrderType Type { get; set; }

        [Display(Name = "确认时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, ListShow = true, EditShow = false, SortOrder = 10003,
            Width = "160")]
        public DateTime ConfirmTime { get; set; } = DateTime.Now;

        public User User { get; set; }

        /// <summary>
        ///     /// 受理工单的用户
        /// </summary>
        [Display(Name = "受理工单的用户")]
        public User AcceptUser { get; set; }

        public long Id { get; set; }
    }
}