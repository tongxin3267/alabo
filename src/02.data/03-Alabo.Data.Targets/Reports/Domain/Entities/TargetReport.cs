using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.App.Asset.Recharges.Domain.Entities;
using Alabo.Data.Targets.Reports.Domain.Enums;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.Targets.Reports.Domain.Entities
{
    /// <summary>
    /// 目标完成统计
    /// 表只增不减少
    /// </summary>
    [ClassProperty(Name = "目标统计")]
    public class TargetReport : AggregateDefaultUserRoot<TargetReport>
    {
        /// <summary>
        /// 目标Id
        /// </summary>
        public string TargetId { get; set; }

        /// <summary>
        /// 目标名称
        /// </summary>
        [Display(Name = "目标名称")]
        [Required]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 1, ListShow = true, GroupTabId = 1)]
        public string Name { get; set; }

        /// <summary>
        /// 统计方式
        /// </summary>
        public TargetReportType Type { get; set; }

        /// <summary>
        ///     奖励货币类型
        /// </summary>
        [Display(Name = "奖励货币类型")]
        [Field(ControlsType = ControlsType.DropdownList, DataSourceType = typeof(MoneyTypeConfig))]
        [HelpBlock("奖励货币类型,任务完成后，奖金会自动到任务处理人相应的账号上")]
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        /// 奖金
        /// </summary>
        [Display(Name = "奖金")]
        [Field(ControlsType = ControlsType.Decimal, SortOrder = 10, ListShow = true, GroupTabId = 2)]
        [HelpBlock("完成该任务后,由主管部分对新任务进行满意度评价,根据满意度获取奖金。比如任务奖金为1000元,任务完成后，主管部门对任务进行评价满意度为90%，则所得到的奖金为900元")]
        public decimal Bonus { get; set; } = 0m;

        /// <summary>
        /// 目标贡献值
        /// </summary>
        [Display(Name = "目标贡献值")]
        [Field(ControlsType = ControlsType.Decimal, SortOrder = 10, ListShow = true, GroupTabId = 2)]
        public decimal Contribution { get; set; } = 0m;
    }

    public class TargetReportTableMap : MsSqlAggregateRootMap<TargetReport>
    {
        protected override void MapTable(EntityTypeBuilder<TargetReport> builder) {
            builder.ToTable("Target_Report");
        }

        protected override void MapProperties(EntityTypeBuilder<TargetReport> builder) {
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.UserName);
        }
    }
}