using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Asset.Transfers.Domain.Entities {

    [ClassProperty(Name = "充值", Icon = "fa fa-puzzle-piece", Description = "充值")]
    public class Transfer : AggregateDefaultUserRoot<Transfer> {

        /// <summary>
        ///     转账配置Id
        /// </summary>
        public Guid ConfigId { get; set; }

        /// <summary>
        ///     对方用户
        /// </summary>
        public long OtherUserId { get; set; }

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

        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [Display(Name = "手续费")]
        [HelpBlock("手续费转出账户的比例,例如转出金额100，手续费0.05 实际转出到账95，手续费5")]
        public decimal Fee { get; set; }

        /// <summary>
        ///     转账说明
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox)]
        [Display(Name = "转账说明")]
        public string Intro { get; set; }

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
            builder.ToTable("Asset_Transfer");
        }

        protected override void MapProperties(EntityTypeBuilder<Transfer> builder) {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.Serial);
            builder.Ignore(e => e.UserName);
            builder.Ignore(e => e.Version);
        }
    }
}