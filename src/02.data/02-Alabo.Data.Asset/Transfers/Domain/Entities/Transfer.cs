﻿using System;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Asset.Transfers.Domain.Entities.Extension;
using Alabo.App.Asset.Transfers.Domain.Enums;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Entities.Extension;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alabo.App.Asset.Transfers.Domain.Entities {

    [ClassProperty(Name = "充值", Icon = "fa fa-puzzle-piece", Description = "充值")]
    public class Transfer : AggregateDefaultUserRoot<Transfer> {

        /// <summary>
        ///     本次变动的货币类型id
        ///     与配置 MoneyTypeConfig 关联
        /// </summary>
        [Display(Name = "货币类型")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, SortOrder = 3)]
        public Guid MoneyTypeId { get; set; }

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
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        public TransferStatus Status { get; set; } = TransferStatus.Pending;

        /// <summary>
        ///     付款时间
        /// </summary>
        [Display(Name = "交易时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, GroupTabId = 1, Width = "150", ListShow = true,
            SortOrder = 7)]
        public DateTime PayTime { get; set; } = DateTime.MinValue;

        /// <summary>
        ///     Gets or sets the 扩展.
        /// </summary>
        [Field(ExtensionJson = "TransferExtension")]
        [Display(Name = "扩展")]
        public string Extension { get; set; }

        /// <summary>
        ///     Gets or sets the with draw 扩展.
        /// </summary>
        [Display(Name = "贸易扩展")]
        public TransferExtension TransferExtension { get; set; } = new TransferExtension();

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

    public class TransferTableMap : MsSqlAggregateRootMap<Transfer> {

        protected override void MapTable(EntityTypeBuilder<Transfer> builder) {
            builder.ToTable("Finance_Transfer");
        }

        protected override void MapProperties(EntityTypeBuilder<Transfer> builder) {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.TransferExtension);
            builder.Ignore(e => e.Serial);
            builder.Ignore(e => e.UserName);
            builder.Ignore(e => e.Version);
        }
    }
}