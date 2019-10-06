using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Data.Targets.Targets.Domain.Enums
{
    /// <summary>
    /// 难度分级
    /// </summary>
    public enum DifficultyClassification
    {
        /// <summary>
        /// 一星目标
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "一星目标")]
        OneStar = 1,

        /// <summary>
        /// 二星目标
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "二星目标")]
        TwoStar = 2,

        /// <summary>
        /// 三星目标
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "三星目标")]
        ThreeStar = 3,

        /// <summary>
        /// 四星目标
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "四星目标")]
        FourStar = 4,

        /// <summary>
        /// 五星目标
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "五星目标")]
        FiveStar = 5,
    }
}