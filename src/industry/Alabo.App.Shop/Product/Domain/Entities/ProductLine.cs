using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Tenants;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Shop.Product.Domain.Entities {

    /// <summary>
    ///     产品线
    /// </summary>
    [ClassProperty(Name = "产品线", Icon = "fa fa-puzzle-piece", Description =
            "将相同的产品归集到一起，不同的产品线可以参与不同的分润规则"
        , SideBarType = SideBarType.ProductSideBar
    )]
    [AutoDelete(IsAuto = true)]
    public class ProductLine : AggregateDefaultRoot<ProductLine> {

        /// <summary>
        ///     产品线名称
        /// </summary>
        [Required(ErrorMessage = "请填写产品线名称")]
        [Display(Name = "名称")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true,
            GroupTabId = 1, IsMain = true, Width = "150", ListShow = true, SortOrder = 2,
            Link = "/Admin/ProductLine/Edit?Id=[[Id]]")]
        [HelpBlock("请填写产品线的名称")]
        public string Name { get; set; }

        /// <summary>
        ///     产品线介绍
        /// </summary>
        [Required(ErrorMessage = "请填写产品线介绍")]
        [Field(ControlsType = ControlsType.TextArea, GroupTabId = 1, ListShow = true, Width = "400", SortOrder = 3)]
        [Display(Name = "产品线介绍")]
        public string Intro { get; set; }

        /// <summary>
        ///     商品ID，用json保存 List<long>().ToJson()
        /// </summary>
        [Display(Name = "商品ID")]
        public string ProductIds { get; set; }

        /// <summary>
        ///     排序
        /// </summary>
        [Display(Name = "排序")]
        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1, ListShow = false, SortOrder = 1001)]
        public long SortOrder { get; set; } = 1000;

        /// <summary>
        ///     最后更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        [Field(EditShow = false, ListShow = true, Width = "130", SortOrder = 2001)]
        public DateTime ModifiedTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     获取链接
        /// </summary>
        public IEnumerable<ViewLink> ViewLinks() {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("产品线添加", "/Admin/ProductLine/Edit", Icons.Add, LinkType.TableQuickLink),
                new ViewLink("商品管理", "/Admin/ProductLine/Productmanagement?Id=[[Id]]", Icons.List, LinkType.ColumnLink),
                new ViewLink("产品线管理", "/Admin/ProductLine/Index", Icons.List, LinkType.FormQuickLink),
                new ViewLink("产品线编辑", "/Admin/ProductLine/Edit?Id=[[Id]]", Icons.Edit, LinkType.ColumnLink),
                new ViewLink("删除", "/Admin/Basic/Delete?Service=IProductLineService&Method=Delete&id=[[Id]]",
                    Icons.Delete, LinkType.ColumnLinkDelete)
            };
            return quickLinks;
        }
    }

    public class ProductLineTableMap : MsSqlAggregateRootMap<ProductLine> {

        protected override void MapTable(EntityTypeBuilder<ProductLine> builder) {
            builder.ToTable("ZKShop_ProductLine");
        }

        protected override void MapProperties(EntityTypeBuilder<ProductLine> builder) {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(250);
            builder.Ignore(e => e.Version);
            if (TenantContext.IsTenant) {
                // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
            }
        }
    }
}