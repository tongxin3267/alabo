using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alabo.Industry.Shop.Categories.Domain.Entities
{
    /// <summary>
    ///     类目属性值表
    /// </summary>
    [ClassProperty(Name = "类目属性值表")]
    public class CategoryPropertyValue : AggregateRoot<CategoryPropertyValue, Guid>
    {
        public CategoryPropertyValue() : this(Guid.Empty)
        {
        }

        public CategoryPropertyValue(Guid id) : base(id)
        {
        }

        /// <summary>
        ///     属性值GUID
        /// </summary>
        [Required]
        [Display(Name = "属性值GUID")]
        public Guid PropertyId { get; set; }

        /// <summary>
        ///     属性名称
        /// </summary>
        [Display(Name = "属性名称")]
        public string ValueName { get; set; }

        /// <summary>
        ///     属性值别名，仅作为显示用
        /// </summary>
        [Display(Name = "属性值别名")]
        public string ValueAlias { get; set; }

        /// <summary>
        ///     是否为显示图片，可以用图片来显示文字
        ///     仅作为显示用
        /// </summary>
        [Display(Name = "是否为显示图片")]
        public string Image { get; set; }

        /// <summary>
        ///     排列序号
        /// </summary>
        [Display(Name = "排列序号")]
        public long SortOrder { get; set; } = 1000;

        /// <summary>
        ///     是否选择
        /// </summary>
        [Display(Name = "是否选择")]
        public bool IsCheck { get; set; }
    }

    public class CategoryPropertyValueTableMap : MsSqlAggregateRootMap<CategoryPropertyValue>
    {
        protected override void MapTable(EntityTypeBuilder<CategoryPropertyValue> builder)
        {
            builder.ToTable("Shop_CategoryPropertyValue");
        }

        protected override void MapProperties(EntityTypeBuilder<CategoryPropertyValue> builder)
        {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.ValueAlias);
            builder.Ignore(e => e.Image);
            builder.Ignore(e => e.IsCheck);
            builder.Ignore(e => e.Version);
            if (TenantContext.IsTenant)
            {
                // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
            }
        }
    }
}