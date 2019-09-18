﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Shop.Order.Domain.Entities.Extensions;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Order.Domain.Entities {

    /// <summary>
    ///     订单操作表
    ///     发货记录保存到此处
    /// </summary>
    [ClassProperty(Name = "订单操作表")]
    public class OrderAction : AggregateDefaultRoot<OrderAction> {

        /// <summary>
        ///     订单号ID
        /// </summary>
        [Display(Name = "订单号ID")]
        public long OrderId { get; set; }

        /// <summary>
        ///     操作介绍，不能为空
        /// </summary>
        [Display(Name = "操作介绍")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Intro { get; set; }

        /// <summary>
        ///     订单操作类型
        /// </summary>
        /// <value>
        ///     The type of the order action.
        /// </value>
        [Display(Name = "订单操作类型")]
        public OrderActionType OrderActionType { get; set; }

        /// <summary>
        ///     操作员管理Id
        /// </summary>
        [Display(Name = "操作员管理Id")]
        public long ActionUserId { get; set; }

        /// <summary>
        ///     操作记录扩展
        /// </summary>
        [Display(Name = "操作记录扩展")]
        public string Extensions { get; set; }

        [Display(Name = "指令动作扩展")]
        public OrderActionExtension OrderActionExtension { get; set; }
    }

    public class OrderOperateTableMap : MsSqlAggregateRootMap<OrderAction> {

        protected override void MapTable(EntityTypeBuilder<OrderAction> builder) {
            builder.ToTable("ZKShop_OrderAction");
        }

        protected override void MapProperties(EntityTypeBuilder<OrderAction> builder) {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Intro).IsRequired();
            builder.Ignore(e => e.OrderActionExtension);
            builder.Ignore(e => e.Version);
            if (TenantContext.IsTenant) {
                // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
            }
        }
    }
}