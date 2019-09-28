using Alabo.App.Asset.Transfers.Domain.Configs;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Users.Entities;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Asset.Transfers.Dtos
{
    /// <summary>
    ///     Class ViewAdminWithDraw.
    /// </summary>
    [ClassProperty(Name = "转账详情", Icon = "fa fa-puzzle-piece", Description = "转账详情",
        SideBarType = SideBarType.FinanceSideBar)]
    public class ViewAdminTransfer : BaseViewModel
    {
        /// <summary>
        ///     序列号
        ///     10位数序列号
        /// </summary>
        [Display(Name = "交易号")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, IsMain = true, GroupTabId = 1, Width = "80", SortOrder = 1)]
        public string Serial { get; set; }

        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     交易用户
        /// </summary>
        public User User { get; set; }

        /// <summary>
        ///     转出账户
        /// </summary>
        public MoneyTypeConfig MoneyType { get; set; }

        /// <summary>
        ///     Gets or sets 会员Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     转账类型
        /// </summary>
        public TransferConfig TransferConfig { get; set; }

        /// <summary>
        ///     手续费
        /// </summary>
        [Display(Name = "手续费")]
        public decimal ServiceFee { get; set; }

        /// <summary>
        ///     实际接受金额
        /// </summary>
        public decimal Amoumt { get; set; }

        /// <summary>
        ///     Gets or sets the check amount.
        /// </summary>
        [Display(Name = "应付人民币")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            LabelColor = LabelColor.Info, ListShow = true, GroupTabId = 1, Width = "80", SortOrder = 8)]
        public decimal CheckAmount { get; set; }

        /// <summary>
        ///     Gets or sets the remark.
        /// </summary>
        [Display(Name = "汇款备注")]
        public string Remark { get; set; }

        /// <summary>
        ///     Gets or sets the create time.
        /// </summary>
        [Display(Name = "交易时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     Gets or sets the money 类型 configuration.
        /// </summary>
        public MoneyTypeConfig TragetMoneyType { get; set; }

        public User TragetUser { get; set; }
    }
}