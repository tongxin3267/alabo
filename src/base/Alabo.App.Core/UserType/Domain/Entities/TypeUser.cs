using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Domain.Entities {

    /// <summary>
    ///     用户类型
    ///     重点理解：EntityId
    /// </summary>
    [ClassProperty(Name = "用户类型用户", Icon = "fa fa-puzzle-piece", Description = "Edit",
        SideBarType = SideBarType.ProvinceSideBar)]
    public class TypeUser : AggregateDefaultUserRoot<TypeUser> {

        /// <summary>
        ///     用户类型数据表Id
        ///     与UserType的Id对应
        ///  如果是供应商的时候,TypeId=Store.Id
        /// </summary>
        [Display(Name = "用户类型数据表Id")]
        public long TypeId { get; set; }

        /// <summary>
        ///     用户类型ID
        /// </summary>
        [Display(Name = "用户类型")]
        public Guid UserTypeId { get; set; }

        /// <summary>
        ///     用户等级Id
        ///     每个类型对应一个等级
        /// </summary>
        [Display(Name = "等级Id")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "110", ListShow = true, SortOrder = 800)]
        public string GradeId { get; set; }
    }

    public class UserTypeUserAgentTableMap : MsSqlAggregateRootMap<TypeUser> {

        protected override void MapTable(EntityTypeBuilder<TypeUser> builder) {
            builder.ToTable("User_TypeUser");
        }

        protected override void MapProperties(EntityTypeBuilder<TypeUser> builder) {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.GradeId).HasMaxLength(24);
            builder.Ignore(e => e.UserName);
            builder.Ignore(e => e.Version);
            if (TenantContext.IsTenant) {
                // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
            }
        }
    }
}