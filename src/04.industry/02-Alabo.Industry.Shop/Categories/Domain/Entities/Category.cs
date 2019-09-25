using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Category.Domain.Entities {

    /// <summary>
    ///     类目
    /// </summary>
    [ClassProperty(Name = "类目")]
    public class Category : AggregateRoot<Category, Guid> {
        /// <summary>
        ///     初始化
        /// </summary>

        public Category()
            : this(Guid.Empty) {
        }

        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="id">主键ID</param>
        public Category(Guid id)
            : base(id) {
        }

        /// <summary>
        ///     类目名称
        /// </summary>
        [Display(Name = "类目名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Name { get; set; }

        /// <summary>
        ///     父类目Id
        /// </summary>
        [Display(Name = "父类目Id")]
        public Guid PartentId { get; set; }

        /// <summary>
        ///     SEO标题
        /// </summary>
        [Display(Name = "SEO标题")]
        [StringLength(200, ErrorMessage = "Seo标题长度不能超过200个字符")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextBox)]
        public string MetaTitle { get; set; }

        /// <summary>
        ///     SEO关键字
        /// </summary>
        [Display(Name = "SEO关键字")]
        [StringLength(300, ErrorMessage = "SEO关键字长度不能超过300个字符")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextBox)]
        public string MetaKeywords { get; set; }

        /// <summary>
        ///     SEO描述
        /// </summary>
        [Display(Name = "SEO描述")]
        [StringLength(400, ErrorMessage = "SEO描述长度不能超过400个字符")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextArea)]
        public string MetaDescription { get; set; }

        /// <summary>
        ///     类目规格（购买用,IsSale=true)
        /// </summary>
        [Display(Name = "类目规格")]
        public List<CategoryProperty> SalePropertys { get; set; } = new List<CategoryProperty>();

        /// <summary>
        ///     类目属性（展示用,IsSale=false)
        ///     商品参数等
        /// </summary>
        [Display(Name = "类目属性")]
        public List<CategoryProperty> DisplayPropertys { get; set; } = new List<CategoryProperty>();

        /// <summary>
        ///     排序,越小排在越前面
        /// </summary>
        [Display(Name = "排序", Order = 1000)]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true, SortOrder = 10000, Width = "110")]
        [Range(0, 99999, ErrorMessage = "请输入0-99999之间的数字")]
        [HelpBlock("排序,越小排在越前面，请输入0-99999之间的数字")]
        public long SortOrder { get; set; } = 1000;

        /// <summary>
        ///     最后更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, ListShow = true, EditShow = false, SortOrder = 10002,
            Width = "160")]
        public DateTime ModifiedTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     通用状态 状态：0正常,1冻结,2删除
        ///     实体的软删除通过此字段来实现
        ///     软删除：指的是将实体标记为删除状态，不是真正的删除，可以通过回收站找回来
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.RadioButton, ListShow = true, EditShow = true,
            SortOrder = 10003, Width = "110", DataSource = "Alabo.Domains.Enums.Status")]
        public Status Status { get; set; } = Status.Normal;
    }

    public class CategoryTableMap : MsSqlAggregateRootMap<Category> {

        protected override void MapTable(EntityTypeBuilder<Category> builder) {
            builder.ToTable("Shop_Category");
        }

        protected override void MapProperties(EntityTypeBuilder<Category> builder) {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired();
            builder.Ignore(e => e.DisplayPropertys);
            builder.Ignore(e => e.SalePropertys);
            builder.Ignore(e => e.Version);
            if (TenantContext.IsTenant) {
                // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
            }
        }
    }
}