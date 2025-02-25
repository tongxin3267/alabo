﻿using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Industry.Shop.OrderDeliveries.Domain.Entities.Extensions;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Alabo.Industry.Shop.OrderDeliveries.Domain.Entities
{
    /// <summary>
    ///     发货记录表
    /// </summary>
    [ClassProperty(Name = "发货记录表", Description = "店铺管理")]
    public class OrderDelivery : AggregateDefaultUserRoot<OrderDelivery>
    {
        /// <summary>
        ///     快递公司的名称
        /// </summary>
        [Display(Name = "快递公司的名称")]
        public string Name { get; set; }

        #region

        /// <summary>
        ///     订单号ID
        /// </summary>
        [Display(Name = "订单号ID")]
        public long OrderId { get; set; }

        /// <summary>
        ///     店铺Id
        /// </summary>
        [Display(Name = "店铺Id")]
        [JsonConverter(typeof(ObjectIdConverter))]
        public string StoreId { get; set; }

        /// <summary>
        ///     快递公司guid
        /// </summary>
        [Display(Name = "快递公司guid")]
        public Guid ExpressGuid { get; set; }

        /// <summary>
        ///     物流单号
        /// </summary>
        [Display(Name = "物流单号")]
        public string ExpressNumber { get; set; }

        /// <summary>
        ///     总共发货数量
        /// </summary>
        [Display(Name = "总共发货数量")]
        public long TotalCount { get; set; }

        /// <summary>
        ///     扩展属性
        /// </summary>
        [Field(ExtensionJson = "OrderDeliveryExtension")]
        [Display(Name = "扩展属性")]
        public string Extension { get; set; }

        /// <summary>
        ///     订单发货数据
        /// </summary>

        [Display(Name = "订单发货数据")]
        public OrderDeliveryExtension OrderDeliveryExtension { get; set; } = new OrderDeliveryExtension();

        #endregion
    }

    public class OrderDeliveryTableMap : MsSqlAggregateRootMap<OrderDelivery>
    {
        protected override void MapTable(EntityTypeBuilder<OrderDelivery> builder)
        {
            builder.ToTable("Shop_OrderDelivery");
        }

        protected override void MapProperties(EntityTypeBuilder<OrderDelivery> builder)
        {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.OrderDeliveryExtension);
            builder.Ignore(e => e.Name);
        }
    }
}