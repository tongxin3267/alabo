using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.Domains.Entities;
using Alabo.Validations;

namespace Alabo.App.Shop.Order.ViewModels
{
    /// <summary>
    /// OrderExpressViewModel
    /// </summary>
    public class OrderExpressViewModel
    {
        /// <summary>
        /// Login user id
        /// </summary>
        [Display(Name = "用户ID")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 99999999, ErrorMessage = "用户ID必须大于0")]
        public long LoginUserId { get; set; }

        /// <summary>
        /// Order id
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 99999999, ErrorMessage = "订单ID必须大于0")]
        [Display(Name = "订单ID")]
        public long OrderId { get; set; }

        /// <summary>
        /// Express amount
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(0, 99999999, ErrorMessage = "金额不能小于0")]
        [Display(Name = "运费")]
        public decimal ExpressAmount { get; set; }

        /// <summary>
        /// Calculate express amount
        /// </summary>
        public decimal CalculateExpressAmount { get; set; }

        /// <summary>
        /// Express description
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Display(Name = "修改邮费原因")]
        public string ExpressDescription { get; set; }
    }
}
