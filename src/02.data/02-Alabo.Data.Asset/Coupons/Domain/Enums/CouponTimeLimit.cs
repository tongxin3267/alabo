using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Asset.Coupons.Domain.Enums {

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