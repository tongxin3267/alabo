using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Query.Dto;
using Alabo.Validations;

namespace Alabo.Industry.Shop.Orders.Dtos
{
    public class ConfirmInput : ApiInputDto
    {
        /// <summary>
        ///     支付密码
        /// </summary>
        [Display(Name = "支付密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string PayPassword { get; set; }
    }
}