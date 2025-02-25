﻿using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Asset.Settlements.Domain.Enums
{
    public enum SettlementStatus
    {
        /// <summary>
        ///     待处理
        /// </summary>
        [Display(Name = "待处理")]
        [LabelCssClass(BadgeColorCalss.Primary)]
        Pending = 1,

        /// <summary>
        ///     等待审核
        /// </summary>
        [Display(Name = "初审成功")]
        [LabelCssClass(BadgeColorCalss.Success)]
        FirstCheckSuccess = 2,

        ///// <summary>
        /////管理员审核成功
        /////提现的两种操作
        ///// </summary>
        //[Display(Name = "管理员审核成功")]
        //[LabelCssClass(BadgeColorCalss.Success)]
        //AdminCheckSuccess = 3,

        /// <summary>
        ///     失败
        /// </summary>
        [Display(Name = "失败")]
        [LabelCssClass(BadgeColorCalss.Danger)]
        Failured = 5,

        /// <summary>
        ///     付款成功
        /// </summary>
        [Display(Name = "付款成功")]
        [LabelCssClass(BadgeColorCalss.Success)]
        Success = 6
    }
}