using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Market.BookingSignup.Domain.Enums
{
    /// <summary>
    /// 活动状态
    /// </summary>
    public enum BookingSignupState
    {
        /// <summary>
        /// 预定
        /// </summary>
        [Display(Name = "预定中")]
        Book = 1,
        /// <summary>
        /// 进行中
        /// </summary>
        [Display(Name = "进行中")]
        processing = 2,
        /// <summary>
        /// 结束
        /// </summary>
        [Display(Name = "已结束")]
        End = 3
    }
}
