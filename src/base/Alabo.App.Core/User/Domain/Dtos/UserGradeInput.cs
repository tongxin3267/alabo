using System;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.Domains.Entities;

namespace Alabo.App.Core.User.Domain.Dtos {

    public class UserGradeInput {

        /// <summary>
        ///     用户Id不能为空
        /// </summary>
        [Display(Name = "用户Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long UserId { get; set; }

        /// <summary>
        ///     用户Id不能为空
        /// </summary>
        [Display(Name = "等级Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public Guid GradeId { get; set; }

        public UpgradeType Type { get; set; }
    }
}