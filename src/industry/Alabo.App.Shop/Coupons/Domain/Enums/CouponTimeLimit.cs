using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Alabo.App.Shop.Coupons.Domain.Enums {

    /// <summary>
    /// 有效期方式
    /// </summary>
    public enum CouponTimeLimit {

        /// <summary>
        /// 领券后多少天
        /// </summary>
        [Display(Name = "领券后多少天")]
        Days = 1,

        /// <summary>
        /// 有效期内
        /// </summary>
        [Display(Name = "有效期内")]
        PeriodOfValidity = 2,
    }
}