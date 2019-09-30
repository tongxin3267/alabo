using Alabo.App.Asset.Refunds.Domain.Entities.Extension;
using Alabo.App.Asset.Refunds.Domain.Enums;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Asset.Refunds.Domain.Entities
{
    [ClassProperty(Name = "退款管理", Icon = "fa fa-puzzle-piece", Description = "退款管理")]
    public class Refund : AggregateDefaultUserRoot<Refund>
    {
        /// <summary>
        ///     本次变动的货币类型id
        ///     与配置 MoneyTypeConfig 关联
        /// </summary>
        [Display(Name = "货币类型")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, SortOrder = 3)]
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        /// 退款原因
        /// </summary>
        [Display(Name = "退款原因")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, SortOrder = 3)]
        public Guid ConfigId { get; set; }

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
        /// 手续费
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        public RefundStatus Status { get; set; } = RefundStatus.Pending;

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
        [Field(ExtensionJson = "RefundExtension")]
        [Display(Name = "扩展")]
        public string Extension { get; set; }

        /// <summary>
        ///     Gets or sets the with draw 扩展.
        /// </summary>
        [Display(Name = "贸易扩展")]
        public RefundExtension RefundExtension { get; set; } = new RefundExtension();

        /// <summary>
        ///     会员备注
        /// </summary>
        [Display(Name = "会员备注")]
        public string UserRemark { get; set; }

        /// <summary>
        ///     ss
        ///     备注，此备注表示管理员备注，前台会员不可以修改
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        ///     失败原因
        /// </summary>
        public string FailureReason { get; set; }

        /// <summary>
        ///     Gets the serial.
        /// </summary>
        [Display(Name = "序号")]
        public string Serial
        {
            get
            {
                var searSerial = $"T{Id.ToString().PadLeft(9, '0')}";
                if (Id.ToString().Length == 10) searSerial = $"{Id.ToString()}";

                return searSerial;
            }
        }
    }

    public class RefundTableMap : MsSqlAggregateRootMap<Refund>
    {
        protected override void MapTable(EntityTypeBuilder<Refund> builder)
        {
            builder.ToTable("Asset_Refund");
        }

        protected override void MapProperties(EntityTypeBuilder<Refund> builder)
        {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.RefundExtension);
            builder.Ignore(e => e.Serial);
            builder.Ignore(e => e.UserName);
        }
    }
}