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
        /// 一星难度
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "一星难度")]
        OneStar = 1,

        /// <summary>
        /// 二星难度
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "二星难度")]
        TwoStar = 2,

        /// <summary>
        /// 三星难度
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "三星难度")]
        ThreeStar = 3,

        /// <summary>
        /// 四星难度
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "四星难度")]
        FourStar = 4,

        /// <summary>
        /// 五星难度
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "五星难度")]
        FiveStar = 5,
    }
}