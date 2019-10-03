using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;

namespace Alabo.App.Asset.Withdraws.Dtos
{
    /// <summary>
    ///     Class WithDrawApiInput.
    /// </summary>
    public class WithDrawApiInput : ApiInputDto
    {
        /// <summary>
        ///     Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Gets or sets 会员Id
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long UserId { get; set; }

        /// <summary>
        ///     Gets or sets the bank card identifier.
        /// </summary>
        [Display(Name = "银行卡Id")]
        [Field(ControlsType = ControlsType.DropdownList, EditShow = true,
            ApiDataSource = "Api/BankCard/GetList?loginUserId={0}", SortOrder = 2)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string BankCardId { get; set; }

        /// <summary>
        ///     货币类型
        /// </summary>
        [Display(Name = "提现账户")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, EditShow = true,
           DataSourceType = typeof(MoneyTypeConfig), IsShowBaseSerach = false,
            SortOrder = 200)]
        public string MoneyTypeName { get; set; } = "提现账户";

        /// <summary>
        ///     货币类型
        /// </summary>
        //[Field(ControlsType = ControlsType.Label, IsShowAdvancedSerach = true, ListShow = true, EditShow = true,
        //   DataSourceType = typeof(MoneyTypeConfig), IsShowBaseSerach = false,
        //    SortOrder = 200)]
        [Display(Name = "提现账户")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     开始金额
        /// </summary>
        [Display(Name = "申请金额")]
        [Field(ControlsType = ControlsType.TextBox, EditShow = true, SortOrder = 2)]
        [Range(0.01, 10000, ErrorMessage = ErrorMessage.NameNotInRang)]
        public decimal Amount { get; set; }

        /// <summary>
        ///     Gets or sets the pay password.
        /// </summary>
        [Display(Name = "支付密码")]
        [Required(ErrorMessage = "请填写您的支付密码，不可以为空")]
        [Field(ControlsType = ControlsType.Password, EditShow = true, SortOrder = 3)]
        public string PayPassword { get; set; }

        /// <summary>
        ///     用户备注
        /// </summary>
        [Display(Name = "备注")]
        [Field(ControlsType = ControlsType.TextArea, EditShow = true, SortOrder = 10)]
        public string UserRemark { get; set; }
    }
}