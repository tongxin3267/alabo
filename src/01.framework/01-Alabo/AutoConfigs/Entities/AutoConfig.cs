using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alabo.AutoConfigs.Entities {

    /// <summary>
    ///     通用配置
    /// </summary>
    [ClassProperty(Name = "自动配置")]
    public class AutoConfig : AggregateDefaultRoot<AutoConfig> {

        /// <summary>
        ///     配置键名
        /// </summary>
        [Display(Name = "配置键名")]
        public string Type { get; set; }

        [Display(Name = "应用程序名称")] public string AppName { get; set; }

        /// <summary>
        ///     配置值（json）
        /// </summary>
        [Display(Name = "配置值")]
        public string Value { get; set; }

        /// <summary>
        ///     最后更新时间
        /// </summary>
        [Display(Name = "最后更新时间")]
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }

    public class GenericConfigTableMap : MsSqlAggregateRootMap<AutoConfig> {

        protected override void MapTable(EntityTypeBuilder<AutoConfig> builder) {
            builder.ToTable("Core_AutoConfig");
        }

        protected override void MapProperties(EntityTypeBuilder<AutoConfig> builder) {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Type).HasMaxLength(255);
            builder.Property(p => p.Value).HasColumnType("ntext");
            builder.Ignore(e => e.Version);
        }
    }
}