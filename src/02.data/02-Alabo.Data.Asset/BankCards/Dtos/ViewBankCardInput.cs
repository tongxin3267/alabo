using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Asset.BankCards.Dtos
{
    public class ViewBankCardInput : ApiInputDto
    {
        public long UserId { get; set; }

        public ObjectId Id { get; set; }

        /// <summary>
        ///     持卡人姓名
        /// </summary>
        [Display(Name = "持卡人姓名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, Width = "120", IsShowAdvancedSerach = true,
            ListShow = true, SortOrder = 5)]
        public string Name { get; set; }

        /// <summary>
        ///     银行类型
        /// </summary>
        [Display(Name = "银行类型")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.DropdownList, Width = "120",
            ListShow = true, SortOrder = 5)]
        public string BankCardTypeName { get; set; }

        public string BankLogo { get; set; }

        /// <summary>
        ///     银行卡号
        /// </summary>
        [Display(Name = "银行卡号")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, Width = "220", IsShowAdvancedSerach = true,
            ListShow = true, SortOrder = 7)]
        [StringLength(20, ErrorMessage = "银行卡号不能超过20位")]
        public string Number { get; set; }

        /// <summary>
        ///     开户行
        /// </summary>
        [Display(Name = "开户行")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, SortOrder = 8)]
        public string Address { get; set; }
    }
}