using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Asset.Coupons.Domain.Enums {

    /// <summary>
    /// 优惠方式
    /// </summary>
    public enum CouponType {

        /// <summary>
        /// 立减
        /// </summary>
        [Display(Name = "立减")]
        Reduce = 1,

        /// <summary>
        /// 打折
        /// </summary>
        [Display(Name = "打折")]
        Discount = 2,
    }

    /// <summary>
    /// 优惠券状态
    /// </summary>
    public enum CouponStatus
    {
        /// <summary>
        /// 未使用
        /// </summary>
        [Display(Name = "未使用")]
        Normal = 1,

        /// <summary>
        /// 已使用
        /// </summary>
        [Display(Name = "已使用")]
        Used = 2,

        /// <summary>
        /// 已过期
        /// </summary>
        [Display(Name = "已过期")]
        Expired = 3,
    }
}