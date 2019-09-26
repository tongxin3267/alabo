using System.ComponentModel.DataAnnotations;
using Alabo.Validations;

namespace Alabo.Industry.Shop.Orders.ViewModels.OrderEdit {

    /// <summary>
    ///     收银台视图
    /// </summary>
    public class OrderEditPay {
        /// <summary>
        ///     获取或设置应付金额
        /// </summary>

        public decimal PaymentAmount { get; set; }

        /// <summary>
        ///     获取或设置支付信息,用于显示
        /// </summary>
        public string PaymentInfo { get; set; }

        public long OrderId { get; set; }

        public long UserId { get; set; }

        /// <summary>
        ///     支付密码
        /// </summary>
        [Display(Name = "支付密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string PayPassword { get; set; }
    }
}