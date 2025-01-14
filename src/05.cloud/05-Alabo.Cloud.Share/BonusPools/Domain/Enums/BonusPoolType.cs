﻿using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace _05_Alabo.Cloud.Share.BonusPools.Domain.Enums
{
    /// <summary>
    ///     奖金池统计方式
    /// </summary>
    [ClassProperty(Name = "奖金池统计方式")]
    public enum BonusPoolType
    {
        /// <summary>
        ///     按分润订单金额保留
        /// </summary>
        [Display(Name = "按分润订单金额保留")]
        [LabelCssClass(BadgeColorCalss.Danger)]
        ByShareOrderAmount = 2,

        /// <summary>
        ///     按分润订单金额保留
        /// </summary>
        [Display(Name = "按分润订单总分润金额保留")]
        [LabelCssClass(BadgeColorCalss.Danger)]
        ByShareOrderTotalAmount = 3
    }
}