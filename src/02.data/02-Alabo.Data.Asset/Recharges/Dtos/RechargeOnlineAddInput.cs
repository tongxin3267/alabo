using System;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Asset.Recharges.Domain.Enums;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Asset.Recharges.Dtos
{
    [ClassProperty(Name = "线上充值")]
    public class RechargeOnlineAddInput : ApiInputDto
    {
        /// <summary>
        ///     充值方式
        ///     线上充值，和线下充值
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public RechargeType RechargeType { get; set; } = RechargeType.Online;

        /// <summary>
        ///     账户Id
        /// </summary>
        [Display(Name = "货币类型")]
        [Field(ControlsType = ControlsType.DropdownList, IsShowAdvancedSerach = true,
            DataSource = "Alabo.App.Core.Finance.Domain.CallBacks.MoneyTypeConfig", IsShowBaseSerach = false,
            SortOrder = 200)]
        public Guid MoneyTypeId { get; set; } = Guid.Parse("E97CCD1E-1478-49BD-BFC7-E73A5D699000");

        ///// <summary>
        ///// 支付方式
        ///// 线上支付时，支付方式必须填写，线下汇款不需要填写
        ///// </summary>
        //public PayType PayType { get; set; }

        /// <summary>
        ///     充值金额  RMB为单位
        /// </summary>
        [Display(Name = "金额")]
        [Range(0.01, 100000, ErrorMessage = ErrorMessage.NameNotInRang)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, Width = "80", SortOrder = 1)]
        public decimal Amount { get; set; }

        /// <summary>
        ///     用户留言
        /// </summary>
        [StringLength(50, MinimumLength = 1, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}