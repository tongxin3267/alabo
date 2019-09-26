using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Asset.Transfers.Dtos
{
    [ClassProperty(Name = "转账", Icon = "fa fa-puzzle-piece", Description = "转账", PostApi = "Api/Transfer/Add",
        SuccessReturn = "Api/Transfer/Get")]
    public class TransferAddInput : ApiInputDto
    {
        /// <summary>
        ///     转出用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the other 会员.
        /// </summary>
        [Display(Name = "对方用户")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 2)]
        public string OtherUserName { get; set; }

        /// <summary>
        ///     Gets or sets the transfer identifier.
        ///     "/api/transfer/gettransferconfis"
        /// </summary>
        [Display(Name = "转账类型")]
        [Field(ControlsType = ControlsType.DropdownList, ApiDataSource = "/Api/Transfer/GetTransferConfis",
            SortOrder = 1)]
        public Guid TransferConfigId { get; set; }

        /// <summary>
        ///     Gets or sets the amount.
        /// </summary>
        [RegularExpression(@"^([1-9]\d*\.\d*|0\.\d*[1-9]\d*)|([1-9]\d*)$", ErrorMessage = "转账金额不能小于0.1")]
        [Required(ErrorMessage = "请输入转账金额")]
        [Display(Name = "转账金额")]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 3)]
        public decimal Amount { get; set; }

        /// <summary>
        ///     Gets or sets the pay password.
        /// </summary>
        [Display(Name = "支付密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.Password, SortOrder = 4)]
        public string PayPassword { get; set; }

        /// <summary>
        ///     会员备注
        /// </summary>
        [Display(Name = "备注")]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 5)]
        public string UserRemark { get; set; }
    }
}