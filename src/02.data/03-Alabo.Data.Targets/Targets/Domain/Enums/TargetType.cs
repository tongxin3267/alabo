using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Data.Targets.Targets.Domain.Enums
{
    /// <summary>
    /// 目录类型
    /// </summary>
    public enum TargetType
    {
        /// <summary>
        /// 内部目标
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "内部目标")]
        Internal = 1,

        /// <summary>
        /// 外包目标
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "外包目标")]
        Outsourcing = 2,
    }
}