using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.App.Shop.Activitys.Domain.Entities;
using Alabo.App.Shop.Activitys.Domain.Entities.Extension;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.UI.AutoForms;
using Alabo.Validations;

namespace Alabo.App.Shop.Activitys.Dtos
{
    /// <summary>
    /// activity edit model for output
    /// </summary>
    public class ActivityEditOutput
    {
        /// <summary>
        /// product id
        /// </summary>
        [Display(Name = "活动类型")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long ProductId { get; set; }

        /// <summary>
        /// datetime range
        /// </summary>
        public string DateTimeRange { get; set; }

        /// <summary>
        /// user range
        /// </summary>
        public UserRange UserRange { get; set; } = UserRange.ByUserGrade;

        /// <summary>
        /// activity
        /// </summary>
        [Display(Name = "活动类型")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public Activity Activity { get; set; }

        /// <summary>
        /// activity custom set
        /// </summary>
        public object ActivityRules { get; set; }

        /// <summary>
        /// autoform
        /// </summary>
        public AutoForm AutoForm { get; set; }
    }
}