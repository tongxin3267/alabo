using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Tenants;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Alabo.Industry.Shop.Categories.Domain.Entities
{
    /// <summary>
    ///     类目属性值
    /// </summary>
    [ClassProperty(Name = "类目属性值")]
    public class CategoryProperty : AggregateRoot<CategoryProperty, Guid>
    {
        public CategoryProperty() : this(Guid.Empty)
        {
        }

        public CategoryProperty(Guid id) : base(id)
        {
        }

        /// <summary>
        ///     类目的ID
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Display(Name = "类目的ID")]
        public Guid CategoryId { get; set; }

        /// <summary>
        ///     是否销售属性。可选值:true(是),0(否)，销售属性统称规格
        /// </summary>
        [Display(Name = "是否销售属性")]
        public bool IsSale { get; set; } = false;

        /// <summary>
        ///     属性名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Name { get; set; }

        ///// <summary>
        /////     属性名称
        ///// </summary>
        //[Display(Name = "属性说明")]
        //public string Tip { get; set; }

        /// <summary>
        ///     输入类型。可选值:input(文本框）、multiple(多行文本）、Numberic（数字）、下拉(dropdownbox)、复选(checkbox)、datetime(时间）
        /// </summary>
        [Display(Name = "输入类型")]
        public ControlsType ControlsType { get; set; }

        /// <summary>
        ///     排序,越小排在越前面
        /// </summary>
        [Display(Name = "排序", Order = 1000)]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true, SortOrder = 10000,
            Width = "110")]
        [Range(0, 99999, ErrorMessage = "请输入0-99999之间的数字")]
        [HelpBlock("排序,越小排在越前面，请输入0-99999之间的数字")]
        public long SortOrder { get; set; } = 1000;

        /// <summary>
        ///     Gets or sets the display value.
        ///     显示值，在网页上面直接显示
        /// </summary>
        /// <value>
        ///     The display value.
        /// </value>
        [Display(Name = "属性显示值", Order = 1000)]
        public string DisplayValue { get; set; }

        /// <summary>
        ///     自定义值
        /// </summary>
        [Display(Name = "自定义值", Order = 1000)]
        public string SelfDefineValue { get; set; }

        [Field(EditShow = true)]
        public List<CategoryPropertyValue> PropertyValues { get; set; } = new List<CategoryPropertyValue>();

        /// <summary>
        ///     Gets or sets the sku json.
        ///     属性值设置
        /// </summary>
        [Field(ControlsType = ControlsType.Json, PlaceHolder = "属性值设置，可以设计多个属性",
            ListShow = false, EditShow = true, JsonCanAddOrDelete = true, ExtensionJson = "PropertyValues")]
        [JsonIgnore]
        public string PropertyValueJson { get; set; }
    }

    public class CategoryPropertyTableMap : MsSqlAggregateRootMap<CategoryProperty>
    {
        protected override void MapTable(EntityTypeBuilder<CategoryProperty> builder)
        {
            builder.ToTable("Shop_CategoryProperty");
        }

        protected override void MapProperties(EntityTypeBuilder<CategoryProperty> builder)
        {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.PropertyValues);
            builder.Ignore(e => e.DisplayValue);
            builder.Ignore(e => e.PropertyValueJson);
            builder.Ignore(e => e.SelfDefineValue);
            builder.Ignore(e => e.Version);
            if (TenantContext.IsTenant)
            {
                // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
            }
        }
    }
}