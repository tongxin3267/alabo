using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Share.HuDong.Domain.Enums
{
    /// <summary>
    ///     互动获奖类型
    /// </summary>
    public enum HudongAwardType
    {
        /// <summary>
        ///     储值
        /// </summary>
        [Display(Name = "储值")] StoreValue = 1,

        /// <summary>
        ///     消费额
        /// </summary>
        [Display(Name = "消费额")] Discount = 2,

        ///// <summary>
        ///// 兑换券
        ///// </summary>
        //[Display(Name = "兑换券")]
        //Exchange = 3,

        /// <summary>
        ///     优惠券
        /// </summary>
        [Display(Name = "优惠券")] Coupon = 4,

        /// <summary>
        ///     无奖品
        /// </summary>
        [Display(Name = "无奖品")] None = 5,

        /// <summary>
        ///     商品礼品
        /// </summary>
        [Display(Name = "商品礼品")] Gift = 6,

        /// <summary>
        ///     积分
        /// </summary>
        [Display(Name = "积分")] Integral = 7,

        /// <summary>
        ///     其他
        /// </summary>
        [Display(Name = "其他")] Others = 8
    }
}