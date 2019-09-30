using Alabo.Tool.Payment;
using Alabo.Validations;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Cloud.School.BookingSignup.Dtos
{
    public class BookingBuyInput
    {
        /// <summary>
        ///     Gets or sets 会员Id
        /// </summary>
        [Display(Name = "用户Id")]
        public long UserId { get; set; }

        /// <summary>
        ///     支付方式Id
        ///     与 Alabo.App.Core.Finance.Domain.CallBacks.PaymentConfig，的Id对应
        /// </summary>
        [Display(Name = "支付方式")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public PayType PayType { get; set; }

        /// <summary>
        ///     订单信息
        /// </summary>
        // public BookingSignupOrder Order { get; set; }

        public string BookingId { get; set; }

        /// <summary>
        ///     活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     单价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     人数
        /// </summary>
        public long Count { get; set; }

        /// <summary>
        ///     总价格
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        ///     推荐人姓名
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        ///     联系人
        /// </summary>
        public string Contacts { get; set; }
    }
}