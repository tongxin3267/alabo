using System;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Asset.Withdraws.Domain.Entities.Extension;
using Alabo.App.Asset.Withdraws.Domain.Enums;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Entities.Extension;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alabo.App.Asset.Withdraws.Domain.Entities {

    [ClassProperty(Name = "提现", Icon = "fa fa-puzzle-piece", Description = "提现")]
    public class Withdraw : AggregateDefaultUserRoot<Withdraw> {

        /// <summary>
        ///     本次变动的货币类型id
        ///     与配置 MoneyTypeConfig 关联
        /// </summary>
        [Display(Name = "货币类型")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, SortOrder = 3)]
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        /// 银行卡Id
        /// </summary>
        public string BankCardId { get; set; }

        /// <summary>
        ///     本次变动的货币量
        ///     资金流向为收入时货币量大于0
        ///     资金流向为支出时货币量小于0
        ///     货币量不能为0
        /// </summary>
        [Display(Name = "交易金额")]
        [Range(0, 99999999, ErrorMessage = "金额不能小于0")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, SortOrder = 5)]
        public decimal Amount { get; set; }

        /// <summary>
        ///     应付人民币
        /// </summary>
        [Display(Name = "应付人民币")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            LabelColor = LabelColor.Info, ListShow = true, GroupTabId = 1, Width = "80", SortOrder = 8)]
        public decimal CheckAmount { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        public WithdrawStatus Status { get; set; } = WithdrawStatus.Pending;

        /// <summary>
        ///     付款时间
        /// </summary>
        [Display(Name = "交易时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, GroupTabId = 1, Width = "150", ListShow = true,
            SortOrder = 7)]
        public DateTime PayTime { get; set; } = DateTime.MinValue;

        /// <summary>
        ///     手续费
        /// </summary>
        [Display(Name = "手续费")]
        public decimal Fee { get; set; }

        /// <summary>
        /// 银行卡提现扩展
        /// </summary>
        public WithdrawExtension WithdrawExtension { get; set; }

        /// <summary>
        /// 扩展数据保存
        /// </summary>
        public string Extensions { get; set; }

        /// <summary>
        ///     Gets the serial.
        /// </summary>
        [Display(Name = "序号")]
        public string Serial {
            get {
                var searSerial = $"T{Id.ToString().PadLeft(9, '0')}";
                if (Id.ToString().Length == 10) {
                    searSerial = $"{Id.ToString()}";
                }

                return searSerial;
            }
        }
    }

    public class WithdrawTableMap : MsSqlAggregateRootMap<Withdraw> {

        protected override void MapTable(EntityTypeBuilder<Withdraw> builder) {
            builder.ToTable("Asset_Withdraw");
        }

        protected override void MapProperties(EntityTypeBuilder<Withdraw> builder) {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.Serial);
            builder.Ignore(e => e.UserName);
            builder.Ignore(e => e.WithdrawExtension);
            builder.Ignore(e => e.Version);
        }
    }
}