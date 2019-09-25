using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Alabo.Data.Things.Brands.Domain.Entities.Extensions;
using Alabo.Data.Things.Goodss.Domain.Enums;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Datas.Queries.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Tenants;
using Alabo.UI;
using Alabo.UI.AutoTables;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson;

namespace Alabo.Data.Things.Goodss.Domain.Entities {

    /// <summary>
    /// 中台商品
    /// 可以对接第三方商品信息
    /// </summary>
    [ClassProperty(Name = "商品管理")]
    public class Goods : AggregateDefaultRoot<Goods> {

        /// <summary>
        ///     商品名称
        /// </summary>
        [Display(Name = "商品名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, PlaceHolder = "请输入商品名称", Operator = Operator.Contains,
            IsShowAdvancedSerach = true)]
        [StringLength(60, ErrorMessage = "60个字以内")]
        public string Name { get; set; }

        /// <summary>
        ///     商品货号
        /// </summary>
        [Display(Name = "货号")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, PlaceHolder = "请输入货号",
            IsShowAdvancedSerach = true, GroupTabId = 3, IsMain = true, Width = "150",
            ListShow = true, SortOrder = 2)]
        [StringLength(60, ErrorMessage = "60个字以内")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Bn { get; set; }

        /// <summary>
        /// 商品类型
        /// </summary>
        public GoodsType Type { get; set; }

        /// <summary>
        ///     商品品牌ID
        ///     商品的品牌Id存放在Store表中
        ///     关联店铺的品牌
        /// </summary>
        [Display(Name = "商品品牌")]
        public string BrandId { get; set; }

        /// <summary>
        /// 支持多个供应商，第一个供应商为主供应商
        /// </summary>
        public string SupplierIds { get; set; }

        /// <summary>
        ///     商品成本价，指卖家的成本价格，该价格统称
        /// </summary>
        [Display(Name = "成本价")]
        [Range(0, 99999999, ErrorMessage = "商品成本价必须为大于等于0的数字")]
        public decimal CostPrice { get; set; }

        /// <summary>
        ///     销售价，价格计算时通过这个价格来计算
        /// </summary>
        [Display(Name = "销售价")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.NumberRang, ListShow = true, Width = "120", SortOrder = 4)]
        [Range(0, 99999999, ErrorMessage = "商品销售价必须为大于等于0的数字")]
        public decimal Price { get; set; }

        /// <summary>
        /// 商品状态
        /// </summary>
        [Display(Name = "商品状态")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsTabSearch = true, Width = "120", SortOrder = 10)]
        public GoodsStatus Status { get; set; } = GoodsStatus.Auditing;

        /// <summary>
        ///     Gets or sets the sort order.
        /// </summary>
        [Display(Name = "排序顺序")]
        public long SortOrder { get; set; }

        /// <summary>
        ///     商品所有属性，包括销售属性，和非销售属性 (非数据库字段)
        /// </summary>
        [Display(Name = "商品所有属性")]
        public GoodsExtensions GoodsExtensions { get; set; }

        /// <summary>
        /// 扩展属性
        /// </summary>
        public string Extensions { get; set; }
    }

    public class GoodsTableMap : MsSqlAggregateRootMap<Goods> {

        protected override void MapTable(EntityTypeBuilder<Goods> builder) {
            builder.ToTable("Things_Goods");
        }

        protected override void MapProperties(EntityTypeBuilder<Goods> builder) {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(250);
            builder.Ignore(e => e.Version);
            builder.Ignore(e => e.GoodsExtensions);
        }
    }
}