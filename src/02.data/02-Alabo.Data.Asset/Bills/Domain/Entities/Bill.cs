using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Asset.Bills.Domain.Entities
{
    /// <summary>
    ///     账单记录
    ///     参考支付宝与淘宝交易记录、账单记录设计
    ///     在实现程序时，请熟悉支付宝相关操作
    /// </summary>
    [ClassProperty(Name = "账单")]
    public class Bill : AggregateDefaultUserRoot<Bill>
    {
        /// <summary>
        ///     对方的用户ID
        /// </summary>
        [Display(Name = "对方的用户ID")]
        public long OtherUserId { get; set; }

        /// <summary>
        ///     账单类型，操作类型，交易分类
        ///     包括购物、理财、充值、提现、系统增加、以及用户自定义记录
        ///     初始系统时，先根据BillActionType添加到BillTypeConfig中
        /// </summary>
        [Display(Name = "账单类型")]
        [Field(ControlsType = ControlsType.Label, ListShow = false, EditShow = true, SortOrder = 10002, Width = "160")]
        public BillActionType Type { get; set; } = BillActionType.Shopping;

        /// <summary>
        ///     Gets or sets the money 类型 identifier.
        /// </summary>
        [Display(Name = "货币类型")]
        [Field(ControlsType = ControlsType.DropdownList, IsShowAdvancedSerach = true,
            DataSourceType = typeof(MoneyTypeConfig), ListShow = false, EditShow = true, SortOrder = 10002,
            Width = "160")]
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     关联的实体ID
        /// </summary>
        [Display(Name = "关联的实体ID")]
        public long EntityId { get; set; } = 0;

        /// <summary>
        ///     资金流向
        /// </summary>
        [Display(Name = "资金流向")]
        [Field(ControlsType = ControlsType.DropdownList, ListShow = false, IsShowAdvancedSerach = true, EditShow = true,
            SortOrder = 10002, Width = "160")]
        public AccountFlow Flow { get; set; }

        /// <summary>
        ///     本次变动的货币量
        ///     资金流向为收入时货币量大于0
        ///     资金流向为支出时货币量小于0
        ///     货币量不能为0
        /// </summary>
        [Display(Name = "金额")]
        [Field(ControlsType = ControlsType.NumberRang, ListShow = true, IsShowAdvancedSerach = true, EditShow = true,
            SortOrder = 10002, Width = "160")]
        public decimal Amount { get; set; }

        /// <summary>
        ///     本次变动后账户的货币量
        ///     账后金额大于0,
        /// </summary>
        [Range(0, 99999999, ErrorMessage = "账后金额不能为负数，账户余额不能负数")]
        [Display(Name = "本次变动后账户的货币量")]
        [Field(ControlsType = ControlsType.NumberRang, ListShow = false, IsShowAdvancedSerach = true, EditShow = true,
            SortOrder = 10002, Width = "160")]
        public decimal AfterAmount { get; set; }

        /// <summary>
        ///     明细说明
        /// </summary>
        [Display(Name = "明细说明")]
        [Field(ControlsType = ControlsType.Label, ListShow = false, EditShow = true, SortOrder = 10002, Width = "160")]
        public string Intro { get; set; }

        /// <summary>
        ///     Gets the serial.
        /// </summary>
        [Display(Name = "序号")]
        [Field(ControlsType = ControlsType.Label, ListShow = false, IsShowAdvancedSerach = true, EditShow = true,
            SortOrder = 1, Width = "160")]
        public string Serial
        {
            get
            {
                var searSerial = $"9{Id.ToString().PadLeft(8, '0')}";
                if (Id.ToString().Length >= 9) {
                    searSerial = $"{Id.ToString()}";
                }

                return searSerial;
            }
        }
    }

    /// <summary>
    ///     应用程序映射配置
    /// </summary>
    public class BillMap : MsSqlAggregateRootMap<Bill>
    {
        /// <summary>
        ///     映射表
        /// </summary>
        protected override void MapTable(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Asset_Bill");
        }

        /// <summary>
        ///     映射属性
        /// </summary>
        protected override void MapProperties(EntityTypeBuilder<Bill> builder)
        {
            //应用程序编号
            builder.HasKey(t => t.Id);
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Intro).IsRequired();
            builder.Property(e => e.Amount).IsRequired();
            builder.Ignore(e => e.Serial);

            if (TenantContext.IsTenant)
            {
                // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
            }
        }
    }
}