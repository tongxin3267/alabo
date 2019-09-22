﻿using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.Domain.Enums {

    [ClassProperty(Name = "充值类型")]
    public enum RechargeType {

        /// <summary>
        ///     线上充值
        /// </summary>
        [Display(Name = "线上充值")]
        [LabelCssClass(BadgeColorCalss.Primary)]
        Online = 1,

        /// <summary>
        ///     线下充值
        /// </summary>
        [Display(Name = "线下充值")]
        [LabelCssClass(BadgeColorCalss.Success)]
        Offline = 2
    }
}