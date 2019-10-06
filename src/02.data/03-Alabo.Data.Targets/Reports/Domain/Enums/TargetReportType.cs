using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Alabo.Data.Targets.Reports.Domain.Enums
{
    /// <summary>
    /// 统计方式
    /// </summary>
    public enum TargetReportType
    {
        /// <summary>
        /// 安排人
        /// </summary>
        [Display(Name = "安排人统计")]
        ArrangeUser = 1,

        /// <summary>
        /// 处理人
        /// </summary>
        [Display(Name = "处理人统计")]
        HandlerUser = 2,

        /// <summary>
        /// 检视人
        /// </summary>
        [Display(Name = "检视人统计")]
        AuditorUser = 3,
    }
}