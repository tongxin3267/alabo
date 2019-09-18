using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.UserType.Domain.Enums;
using Alabo.Core.Regex;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.UserType.ViewModels {

    public class ViewUserTypeEdit : BaseViewModel {
        public long Id { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public UserTypeConfig UserTypeConfig { get; set; }

        public Domain.Entities.UserType UserType { get; set; }

        [Display(Name = "状态")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public UserTypeStatus Status { get; set; }

        [Display(Name = "所属用户名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [RegularExpression(RegularExpressionHelper.UserName, ErrorMessage = "用户名格式错误，必须为数字和字母，不能是汉字，长度得在3-16之间")]
        public string UserName { get; set; }

        [Display(Name = "推荐人")] public string ParentUserName { get; set; }

        public string Key { get; set; }

        public Guid GradeId { get; set; }

        public string RoldId { get; set; }

        public IEnumerable<SelectListItem> RolesItems { get; set; }

        /// <summary>
        ///     详细介绍
        /// </summary>
        [Display(Name = "详细介绍")]
        public string Intro { get; set; }

        /// <summary>
        ///     详细介绍
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}