using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alabo.App.Kpis.Kpis.Domain.Entities
{
    /// <summary>
    ///     统计表（该表数据，只增，不编辑，不删除)
    ///     通过此表结构实现（绩效管理、数据统计）
    ///     常用场景：
    ///     2.商城销售额
    ///     3.后期分润封顶操作，包括日封顶、月封顶、年度封顶
    ///     4.绩效管理应用
    ///     5.后期给来做统计报表
    /// </summary>
    [ClassProperty(Name = "统计表")]
    public class Kpi : AggregateDefaultUserRoot<Kpi>
    {
        /// <summary>
        ///     考核类型Id，通过
        /// </summary>
        [Display(Name = "考核类型Id")]
        public Guid ModuleId { get; set; }

        /// <summary>
        ///     对应的实体Id
        ///     比如：分润时为ModuleId，可以统计出单个维度配置的金额，实现日封顶，周封顶等操作
        /// </summary>
        [Display(Name = "对应的实体Id")]
        public long EntityId { get; set; }

        /// <summary>
        ///     时间统计方式
        /// </summary>
        [Display(Name = "时间统计方式")]
        public TimeType Type { get; set; }

        /// <summary>
        ///     本次增加数值
        /// </summary>
        [Display(Name = "本次增加数值")]
        public decimal Value { get; set; }

        /// <summary>
        ///     累计数值
        /// </summary>
        [Display(Name = "累计数值")]
        public decimal TotalValue { get; set; }
    }

    public class KPITableMap : MsSqlAggregateRootMap<Kpi>
    {
        protected override void MapTable(EntityTypeBuilder<Kpi> builder)
        {
            builder.ToTable("Kpi_Kpi");
        }

        protected override void MapProperties(EntityTypeBuilder<Kpi> builder)
        {
            builder.HasKey(e => e.Id);
         
        }
    }
}