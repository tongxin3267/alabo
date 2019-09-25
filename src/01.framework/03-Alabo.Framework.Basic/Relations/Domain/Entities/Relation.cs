using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Common.Domain.Entities {

    /// <summary>
    ///     通用级联关系
    /// </summary>
    [ClassProperty(Name = "通用级联关系")]
    public class Relation : EntityCommonWithSeo<Relation> {

        /// <summary>
        ///     所属类型：比如城市表：City、商品分类ProductClass、文章标签ArticleTag
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Display(Name = "所属类型")]
        public string Type { get; set; }

        /// <summary>
        ///     用户Id
        /// </summary>
        [Display(Name = "用户Id")]
        public long UserId { get; set; } = 0;

        /// <summary>
        ///     名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [MaxLength(15, ErrorMessage = "名称长度不能多于15个字符")]
        public string Name { get; set; }

        /// <summary>
        ///     父级ID
        /// </summary>
        [Display(Name = "父级ID")]
        public long FatherId { get; set; } = 0;

        /// <summary>
        ///     个性化数据（用Json保存，比如商品分类有seo标题，商品分类图标、商品描述等，个性化数据）
        /// </summary>
        [Display(Name = "个性化数据")]
        public string Value { get; set; }

        /// 以下前台为快速开发添加字段，后期用json保存到Value当中去
        /// <summary>
        ///     图标
        /// </summary>
        [Display(Name = "图标/图片")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextBox)]
        public string Icon { get; set; }
    }

    public class RelationTableMap : MsSqlAggregateRootMap<Relation> {

        protected override void MapTable(EntityTypeBuilder<Relation> builder) {
            builder.ToTable("Basic_Relation");
        }

        protected override void MapProperties(EntityTypeBuilder<Relation> builder) {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(255);
            builder.Property(e => e.Value).HasColumnType("ntext");
            builder.Ignore(e => e.Version);
            if (TenantContext.IsTenant) {
                // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
            }
        }
    }
}