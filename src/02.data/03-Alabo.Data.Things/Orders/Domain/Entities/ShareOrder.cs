using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Data.Things.Orders.Domain.Entities.Extensions;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alabo.Data.Things.Orders.Domain.Entities {

    /// <summary>
    ///     分润订单
    /// </summary>
    [ClassProperty(Name = "分润订单", Icon = "fa fa-file", Description = "分润订单", ListApi = "Api//AdminBasic/List?Service=IShareOrderService&Method=GetPagedList",
        SideBarType = SideBarType.FenRunSideBar)]
    public class ShareOrder : AggregateDefaultUserRoot<ShareOrder> {

        /// <summary>
        ///     订单金额，分润的金额基数，如果是商品金额，则写商品金额，如果是分润价则使用分润价
        /// </summary>
        [Display(Name = "订单金额")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            EditShow = true,
            Width = "100", ListShow = true, SortOrder = 4)]
        public decimal Amount { get; set; }

        /// <summary>
        ///     所对应的实体Id，比如订单时为订单ID
        /// </summary>
        [Display(Name = "分润订单ID")]
        [Field(ControlsType = ControlsType.Numberic, EditShow = false, Width = "100", ListShow = false, IsMain = true,
            SortOrder = 1)]
        public long EntityId { get; set; }

        /// <summary>
        ///     分润触发类型
        /// </summary>
        [Display(Name = "触发类型")]
        [Field(ControlsType = ControlsType.DropdownList, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            DataSourceType = typeof(TriggerType), EditShow = true, Width = "150",
            ListShow = true, SortOrder = 3)]
        public TriggerType TriggerType { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.DropdownList, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            DataSourceType = typeof(ShareOrderStatus), GroupTabId = 1, Width = "100",
            ListShow = true, SortOrder = 1005)]
        public ShareOrderStatus Status { get; set; } = ShareOrderStatus.Pending;

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "系统状态")]
        public ShareOrderSystemStatus SystemStatus { get; set; } = ShareOrderSystemStatus.Pending;

        /// <summary>
        ///     参数文本
        /// </summary>

        [Display(Name = "参数文本")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = false,
            SortOrder = 900)]
        public string Parameters { get; set; } = string.Empty;

        /// <summary>
        ///     操作记录备注信息
        /// </summary>
        [Display(Name = "操作备注")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "110", ListShow = true, SortOrder = 1004)]
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        ///     最后更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "110", ListShow = true, SortOrder = 2000)]
        public DateTime UpdateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     UserExtension的扩展数据.json格式保存
        /// </summary>
        [Field(ExtensionJson = "ShareOrderExtension")]
        [Display(Name = "扩展数据")]
        public string Extension { get; set; }

        /// <summary>
        ///     分润模块执行次数
        /// </summary>
        [Display(Name = "模块执行次数")]
        [Field(ControlsType = ControlsType.Numberic, EditShow = true, Width = "110", ListShow = true, IsMain = true,
            SortOrder = 3000)]
        public long ExecuteCount { get; set; }

        #region 以下字段不插入数据库

        /// <summary>
        ///     扩展数据
        /// </summary>
        [Display(Name = "扩展数据")]
        public ShareOrderExtension ShareOrderExtension { get; set; }

        /// <summary>
        ///     尽在前台显示用，不插入数据库
        /// </summary>
        [Display(Name = "分润数据")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, EditShow = false,
            SortOrder = 1, Link = "/Admin/Reward/List?OrderId=[[Id]]")]
        public string DisplayName { get; set; } = "分润数据";

        /// <summary>
        ///     获取链接
        /// </summary>
        public IEnumerable<ViewLink> ViewLinks() {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("分润测试", "/Admin/ShareOrder/Edit", Icons.Add, LinkType.TableQuickLink),
                new ViewLink("分润订单管理", "/Admin/ShareOrder/Index", Icons.List, LinkType.FormQuickLink),
                //new ViewLink("详情", "/Admin/ShareOrder/Show?Id=[[Id]]", Icons.Edit, LinkType.ColumnLink),
                new ViewLink("分润数据", "/Admin/Reward/List?OrderId=[[Id]]", Icons.Edit, LinkType.ColumnLink)
            };
            return quickLinks;
        }

        #endregion 以下字段不插入数据库
    }

    public class ShareOrderTableMap : MsSqlAggregateRootMap<ShareOrder> {

        protected override void MapTable(EntityTypeBuilder<ShareOrder> builder) {
            builder.ToTable("Task_ShareOrder");
        }

        protected override void MapProperties(EntityTypeBuilder<ShareOrder> builder) {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Ignore(r => r.UserName);
            builder.Ignore(r => r.ShareOrderExtension);
            builder.Ignore(r => r.DisplayName);
            builder.Ignore(e => e.Version);
            if (TenantContext.IsTenant) {
                // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
            }
        }
    }
}