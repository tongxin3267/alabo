﻿using Alabo.App.Shop.Store.Domain.Entities.Extensions;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Shop.Store.Domain.Entities {

    /// <summary>
    ///     供应商店铺
    /// </summary>
    [ClassProperty(Name = "供应商管理",
        GroupName = "基本信息,高级选项", Icon = "fa fa-puzzle-piece", SortOrder = 1, Description = "设置以及查看供应商的详细信息")]
    [AutoDelete(IsAuto = true)]
    public class Store : AggregateDefaultUserRoot<Store> {

        /// <summary>
        ///     供应商名称
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, SortOrder = 1)]
        [Display(Name = "供应商名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("供应商名称")]
        public string Name { get; set; }

        /// <summary>
        ///     店铺等级
        ///     与AutoConfig中的SupplierGradeConfig对应
        /// </summary>
        [Display(Name = "店铺等级")]
        public Guid GradeId { get; set; }

        /// <summary>
        ///     推荐人用户Id
        /// </summary>
        [Display(Name = "推荐人")]
        public long ParentUserId { get; set; }

        /// <summary>
        ///     店铺状态
        /// </summary>
        [Display(Name = "店铺状态")]
        public UserTypeStatus Status { get; set; } = UserTypeStatus.Pending;

        /// <summary>
        ///     是否为平台,
        ///     平台只能有一个
        /// </summary>
        [Display(Name = "是否为平台")]
        public bool IsPlanform { get; set; } = false;

        /// <summary>
        ///     店铺的扩展属性
        /// </summary>
        [Display(Name = "店铺扩展")]
        public string Extension { get; set; }

        /// <summary>
        ///     店铺的扩展属性
        /// </summary>
        [Display(Name = "店铺的扩展属性")]
        public StoreExtension StoreExtension { get; set; }
    }

    public class StoreTableMap : MsSqlAggregateRootMap<Store> {

        protected override void MapTable(EntityTypeBuilder<Store> builder) {
            builder.ToTable("ZKShop_Store");
        }

        protected override void MapProperties(EntityTypeBuilder<Store> builder) {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired();
            builder.Ignore(e => e.StoreExtension);
            builder.Ignore(e => e.Version);
        }
    }
}