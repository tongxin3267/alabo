using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.App.Asset.Pays.Dtos
{
    [ClassProperty(Name = "收银台", Icon = "fa fa-puzzle-piece", Description = "提现管理" )]
    public class PayPrivew
    {
        /// <summary>
        ///     id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     userid
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Display(Name = "用户名")]
        [NotMapped]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 1)]
        public string UserName { get; set; }

        public MoneyTypeConfig MoneyTypeConfig { get; set; }

        /// <summary>
        ///     moneyTypeId
        /// </summary>
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     货币类型
        /// </summary>
        [Display(Name = "申请账户")]
        public string MoneyTypeName { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        public string Status { get; set; }

        /// <summary>
        ///     显示银行卡信息
        /// </summary>
        [Display(Name = "银行卡信息")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.NumberRang, IsMain = true, IsShowBaseSerach = false,
            IsShowAdvancedSerach = false, LabelColor = LabelColor.Warning, ListShow = false, GroupTabId = 1,
            Width = "80",
            SortOrder = 4)]
        public string BankCardInfo { get; set; }

        /// <summary>
        ///     申请金额
        /// </summary>
        [Display(Name = "申请金额")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "80", SortOrder = 4)]
        public decimal Amount { get; set; }
    }
}