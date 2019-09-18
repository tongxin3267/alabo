﻿using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Tasks.Domain.Enums {

    /// <summary>
    ///     分润订单处理状态
    /// </summary>
    [ClassProperty(Name = "分润订单处理状态")]
    public enum ShareOrderStatus {

        /// <summary>
        ///     待处理
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "待处理")]
        [Field(Icon = "la la-lock")]
        Pending = 1,

        /// <summary>
        ///     处理中，暂未使用
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "处理中")]
        [Field(Icon = "la la-lock")]
        Processing = 2,

        /// <summary>
        ///     处理完毕
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "已处理")]
        [Field(Icon = "la la-check-circle-o")]
        Handled = 3,

        /// <summary>
        ///     发生错误
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "发生错误")]
        [Field(Icon = "la la-lock")]
        Error = 10,

        /// <summary>
        ///     已取消
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "已取消")]
        [Field(Icon = "la la-lock")]
        Canceld = 11
    }
}