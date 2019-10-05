using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Data.Targets.Iterations.Domain.Enums
{
    /// <summary>
    /// 迭代阶段
    /// </summary>
    public enum IterationPhase
    {
        /// <summary>
        ///     未开始
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "未开始")]
        NotBegin = 1,

        /// <summary>
        ///     进行中
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "进行中")]
        OnGoing = 2,

        /// <summary>
        ///     已完成
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "已完成")]
        Over = 3
    }
}