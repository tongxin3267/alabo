using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alabo.Framework.Basic.Relations.Domain.Entities
{
    /// <summary>
    ///     级联关系 与实体之间的索引、可以实现一对多、多对多的关系图
    ///     比如文章分类:一个篇文章可能有多个分类
    ///     比如商品标签：每个商品可能有多个标签
    ///     比如客户分类：每个客户只能有一个分类
    ///     比如发货地址：每个地址唯一对应一个城市ID
    /// </summary>
    [ClassProperty(Name = "级联关系")]
    public class RelationIndex : AggregateDefaultRoot<RelationIndex>
    {
        /// <summary>
        ///     所属类型：比如城市表：City、商品分类ProductClass、文章标签ArticleTag
        /// </summary>

        [Display(Name = "所属类型")]
        public string Type { get; set; }

        /// <summary>
        ///     实体ID,比如文章ID、客户ID
        /// </summary>
        [Display(Name = "实体ID")]
        public long EntityId { get; set; }

        /// <summary>
        ///     级联关系ID
        /// </summary>
        [Display(Name = "级联关系ID")]
        public long RelationId { get; set; }
    }

    public class RelationIndexTableMap : MsSqlAggregateRootMap<RelationIndex>
    {
        protected override void MapTable(EntityTypeBuilder<RelationIndex> builder)
        {
            builder.ToTable("Basic_RelationIndex");
        }

        protected override void MapProperties(EntityTypeBuilder<RelationIndex> builder)
        {
            //应用程序编号
            builder.HasKey(t => t.Id);
            builder.Property(e => e.Type).HasMaxLength(255);
         
            if (TenantContext.IsTenant)
            {
                // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
            }
        }
    }
}