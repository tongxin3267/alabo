using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.Core.Enums.Enum;
using Alabo.Core.Regex;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.ViewModels.Account {

    /// <summary>
    ///     Class ViewAccount.
    /// </summary>
    public class ViewAccount {

        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     钱包地址
        /// </summary>
        [Display(Name = "钱包地址")]
        [Field(ListShow = true, IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, GroupTabId = 1, Width = "400", SortOrder = 4, LabelColor = LabelColor.Info)]
        public string Token { get; set; }

        /// <summary>
        ///     用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     资金操作类型，改字段不插入数据库，仅用于视图显示
        /// </summary>
        public long ActionType { get; set; }

        /// <summary>
        ///     货币类型id
        /// </summary>
        [Display(Name = "货币类型")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     货币名称
        /// </summary>
        [Display(Name = "货币名称")]
        public Currency Currency { get; set; }

        /// <summary>
        ///     货币量(余额)
        /// </summary>
        [Display(Name = "金额")]
        [Required(ErrorMessage = "请填写操作金额")]
        [RegularExpression(RegularExpressionHelper.DecimalThanZero, ErrorMessage = "金额必须大于0")]
        public decimal Amount { get; set; }

        /// <summary>
        ///     冻结的货币量
        /// </summary>
        public decimal FreezeAmount { get; set; }

        /// <summary>
        ///     历史累计转入货币量(截至总收入)
        /// </summary>
        public decimal HistoryAmount { get; set; }

        /// <summary>
        ///     Gets or sets the 会员.
        /// </summary>
        public User.Domain.Entities.User User { get; set; }

        /// <summary>
        ///     Gets or sets the money 类型 configuration.
        /// </summary>
        [Display(Name = "操作类型")]
        public MoneyTypeConfig MoneyTypeConfig { get; set; }

        /// <summary>
        ///     Gets or sets the account list.
        /// </summary>
        public IList<Domain.Entities.Account> AccountList { get; set; }

        /// <summary>
        ///     备注，主要用于视图的页面操作
        ///     该字段不插入数据库，仅用于视图显示
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}