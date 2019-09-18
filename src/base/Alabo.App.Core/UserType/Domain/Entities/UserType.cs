using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.UserType.Domain.Entities.Extensions;
using Alabo.App.Core.UserType.Domain.Enums;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Domain.Entities {

    /// <summary>
    ///     用户类型
    ///     重点理解：EntityId
    /// </summary>
    [ClassProperty(Name = "用户类型", Icon = "fa fa-puzzle-piece", Description = "Edit",
        SideBarType = SideBarType.ProvinceSideBar)]
    public class UserType : AggregateDefaultUserRoot<UserType> {

        /// <summary>
        ///     用户类型ID
        /// </summary>
        [Display(Name = "用户类型")]
        public Guid UserTypeId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(20, ErrorMessage = "不能超过8个字符")]
        [Field(IsShowBaseSerach = true, PlaceHolder = "请输入用户名", DataField = "UserId",
            ControlsType = ControlsType.TextBox, GroupTabId = 1, IsMain = true, Width = "280", ListShow = true,
            EditShow = true, SortOrder = 2)]
        public string Name { get; set; }

        /// <summary>
        ///     相关的实体Id，比如如果是省代理，城市代理，区域代理，则使用Common_Region中的Id
        ///     如果是商圈，则使用Common_Cicle的Id
        ///     如果时候供应商，则使用ZKShop_Store 的Id
        ///     所属区域ID
        ///     一个区域只能有个
        ///     广东省 省代理只有一个
        ///     东莞市 市代理也只有一个
        /// </summary>
        [Display(Name = "区域")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "110", ListShow = true, SortOrder = 600)]
        public long EntityId { get; set; }

        /// <summary>
        ///     推荐人用户Id
        /// </summary>
        [Display(Name = "推荐人")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "110", ListShow = true, SortOrder = 700)]
        public long ParentUserId { get; set; }

        /// <summary>
        ///     用户等级Id
        ///     每个类型对应一个等级
        /// </summary>
        [Display(Name = "等级Id")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "110", ListShow = true, SortOrder = 800)]
        public Guid GradeId { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.DropdownList,
            DataSource = "Alabo.App.Core.UserType.Domain.Enums.UserTypeStatus", GroupTabId = 1, Width = "110",
            ListShow = true, SortOrder = 1005)]
        public UserTypeStatus Status { get; set; } = UserTypeStatus.Pending;

        /// <summary>
        ///     类型的组织架构图
        ///     计算分润时，可加快速度
        /// </summary>
        [Display(Name = "组织架构图")]
        public string ParentMap { get; set; }

        /// <summary>
        ///     扩展属性
        /// </summary>
        [Field(ExtensionJson = "Alabo.App.Core.UserType.Domain.Entities.Extensions.UserTypeExtensions")]
        [Display(Name = "扩展属性")]
        public string Extensions { get; set; }

        /// <summary>
        ///     扩展属性
        ///     每种类型的扩展数据，可能不同
        /// </summary>
        [Display(Name = "用户类型扩展属性")]
        public UserTypeExtensions UserTypeExtensions { get; set; }
    }

    public class UserTypeAgentTableMap : MsSqlAggregateRootMap<UserType> {

        protected override void MapTable(EntityTypeBuilder<UserType> builder) {
            builder.ToTable("User_UserType");
        }

        protected override void MapProperties(EntityTypeBuilder<UserType> builder) {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.Name).HasMaxLength(20);
            builder.Ignore(e => e.UserTypeExtensions);
            builder.Ignore(e => e.UserName);
            builder.Ignore(e => e.Version);
        }
    }
}