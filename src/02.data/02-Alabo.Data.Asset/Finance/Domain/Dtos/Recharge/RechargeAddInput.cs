using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.Domain.Dtos.Recharge {

    /// <summary>
    ///     Class RechargeAddInput.
    ///     线上和线下充值接口
    /// </summary>
    [ClassProperty(Name = "充值", PostApi = "Api/Recharge/AddOffOnline", SuccessReturn = "Api/Recharge/Get",
        Description = "充值")]
    public class RechargeAddInput : ApiInputDto {
        /// <summary>
        ///     充值方式
        ///     线上充值，和线下充值
        /// </summary>
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        //public RechargeType RechargeType { get; set; }

        /// <summary>
        ///     账户Id
        /// </summary>
       // [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public Guid MoneyTypeId { get; set; }

        ///// <summary>
        ///// 支付方式
        ///// 线上支付时，支付方式必须填写，线下汇款不需要填写
        ///// </summary>
        //[Display(Name = "支付方式")]
        //[Field(ControlsType = ControlsType.DropdownList, DataSource = "Alabo.App.Core.Finance.Domain.Enums.PayType", Width = "80", SortOrder = 2)]
        //public PayType PayType { get; set; }

        /// <summary>
        ///     充值金额  RMB为单位
        /// </summary>
        [Display(Name = "充值金额")]
        [Range(0.01, 100000, ErrorMessage = ErrorMessage.NameNotInRang)]
        [Field(ControlsType = ControlsType.Decimal, Width = "80", EditShow = true, SortOrder = 1)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public decimal Amount { get; set; }

        /// <summary>
        ///     Gets or sets the bank 类型.
        ///     汇款银行
        ///     线下充值时，汇款银行必须填写
        /// </summary>
        [Display(Name = "汇款银行")]
        [Field(ControlsType = ControlsType.DropdownList, EditShow = false, DataSource = "Alabo.Core.Enums.Enum.BankType", Width = "80",
            SortOrder = 3)]
        public BankType BankType { get; set; }

        /// <summary>
        ///     银行卡号
        ///     线下充值时，汇款银行必须填写
        /// </summary>
        [Display(Name = "银行卡号")]
        [Field(ControlsType = ControlsType.TextBox, EditShow = false, Width = "80", SortOrder = 4)]
        public string BankNumber { get; set; }

        /// <summary>
        ///     持卡人姓名
        /// </summary>
        [Display(Name = "持卡人姓名")]
        [Field(ControlsType = ControlsType.TextBox, EditShow = false, Width = "80", SortOrder = 5)]
        public string BankName { get; set; }

        /// <summary>
        ///     用户留言
        /// </summary>
        [Display(Name = "用户留言")]
        [Field(ControlsType = ControlsType.TextArea, EditShow = false, Width = "80", SortOrder = 10)]
        public string Remark { get; set; }
    }
}