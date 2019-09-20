using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.Domain.Dtos.WithDraw {

    /// <summary>
    ///     Class WithDrawInput.
    ///     提现申请
    /// </summary>
    [ClassProperty(Name = "提现管理", Icon = "fa fa-puzzle-piece", Description = "提现管理", PostApi = "Api/WithDraw/Add",
        SuccessReturn = "pages/WithDraw/view")]
    public class WithDrawInput : EntityDto {

        /// <summary>
        ///     Id
        /// </summary>
        public long Id { get; set; }

        public long UserId { get; set; }

        /// <summary>
        ///     Gets or sets 会员Id
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long LoginUserId { get; set; }

        public string BankCard { get; set; }

        /// <summary>
        ///     Gets or sets the bank card identifier.
        /// </summary>
        [Display(Name = "选择银行卡")]
        [Field(ControlsType = ControlsType.DropdownList, ApiDataSource = "Api/BankCard/GetList", SortOrder = 1)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string BankCardId { get; set; }

        /// <summary>
        ///     货币类型
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, EditShow = true,
            ApiDataSource = "Api/WithDraw/GetAccountType", SortOrder = 2)]
        [Display(Name = "提现账户")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     开始金额
        /// </summary>
        [Display(Name = "申请金额")]
        [Field(ControlsType = ControlsType.TextBox, EditShow = true, SortOrder = 4)]
        [Range(0.01, 10000, ErrorMessage = ErrorMessage.NameNotInRang)]
        public decimal Amount { get; set; }

        /// <summary>
        ///     Gets or sets the pay password.
        /// </summary>
        [Display(Name = "支付密码")]
        [Required(ErrorMessage = "请填写您的支付密码，不可以为空")]
        [Field(ControlsType = ControlsType.Password, EditShow = true, ListShow = true, SortOrder = 3)]
        public string PayPassword { get; set; }

        /// <summary>
        ///     用户备注
        /// </summary>
        [Display(Name = "备注")]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 99)]
        public string UserRemark { get; set; }
    }
}