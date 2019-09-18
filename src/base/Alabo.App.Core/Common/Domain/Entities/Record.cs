using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Common.Domain.Entities {

    /// <summary>
    ///     记录管理
    /// </summary>
    [ClassProperty(Name = "记录管理")]
    public class Record : AggregateDefaultUserRoot<Record> {

        /// <summary>
        ///     记录类型
        /// </summary>
        [Display(Name = "记录类型")]
        public string Type { get; set; }

        /// <summary>
        ///     值
        /// </summary>
        [Display(Name = "值")]
        public string Value { get; set; }
    }

    public class RecordTableMap : MsSqlAggregateRootMap<Record> {

        protected override void MapTable(EntityTypeBuilder<Record> builder) {
            builder.ToTable("Common_Record");
        }

        protected override void MapProperties(EntityTypeBuilder<Record> builder) {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.UserName);
            builder.Ignore(e => e.Version);
            if (TenantContext.IsTenant) {
                // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
            }
        }
    }
}