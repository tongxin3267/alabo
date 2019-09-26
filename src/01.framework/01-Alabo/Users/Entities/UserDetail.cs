using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Mapping.Dynamic;
using Alabo.Users.Enum;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Users.Entities
{
    /// <summary>
    ///     Class UserDetail.
    /// </summary>
    [ClassProperty(Name = "用户详情", PageType = ViewPageType.List, PostApi = "Api/User/Update",
        ListApi = "Api/User/Update")]
    public class UserDetail : AggregateDefaultUserRoot<UserDetail>
    {
        /// <summary>
        ///     密码
        /// </summary>
        [Display(Name = "密码")]
        [DynamicIgnore]
        public string Password { get; set; }

        /// <summary>
        ///     支付密码
        /// </summary>
        [Display(Name = "支付密码")]
        [DynamicIgnore]
        public string PayPassword { get; set; }

        /// <summary>
        ///     所属区域ID
        /// </summary>
        [Display(Name = "所属区域ID")]
        public long RegionId { get; set; } = 0;

        /// <summary>
        ///     保存到UserAddress表中的Id
        /// </summary>
        [Display(Name = "地址Id")]
        public string AddressId { get; set; } = string.Empty;

        /// <summary>
        ///     性别
        /// </summary>
        [Display(Name = "性别")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", EditShow = true, SortOrder = 5)]
        public Sex Sex { get; set; } = Sex.Man;

        /// <summary>
        ///     Gets or sets the avator.
        /// </summary>
        [Display(Name = "头像")]
        [Field(ControlsType = ControlsType.ImagePreview, GroupTabId = 1, Width = "150", EditShow = true, SortOrder = 1)]
        public string Avator { get; set; }

        /// <summary>
        ///     Gets or sets the name of the nick.
        /// </summary>
        [Display(Name = "昵称")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", EditShow = true, SortOrder = 2)]
        public string NickName { get; set; }

        /// <summary>
        ///     出生日期 生日,年龄根据出生日期计算
        /// </summary>
        [Display(Name = "出生日期")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", EditShow = true, SortOrder = 3)]
        public DateTime Birthday { get; set; } = DateTime.Now;

        /// <summary>
        ///     注册IP
        /// </summary>
        [Display(Name = "注册IP")]
        public string RegisterIp { get; set; } = "127.0.0.1";

        /// <summary>
        ///     登录次数
        /// </summary>
        [Display(Name = "登录次数")]
        public long LoginNum { get; set; } = 0;

        /// <summary>
        ///     最后一次登录时间
        /// </summary>
        [Display(Name = "最后一次登录时间")]
        public DateTime LastLoginTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     最后一次登录IP
        /// </summary>
        [Display(Name = "最后一次登录IP")]
        public string LastLoginIp { get; set; } = "127.0.0.1";

        /// <summary>
        ///     最后更新时间
        /// </summary>
        [Display(Name = "最后更新时间")]
        public DateTime ModifiedTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     Gets or sets the open identifier.
        /// </summary>
        [StringLength(255)]
        [Display(Name = "开放式认证系统")]
        public string OpenId { get; set; } = "";

        /// <summary>
        ///     备注
        /// </summary>
        [Display(Name = "备注")]
        [DynamicNotIgnoreEmpty]
        public string Remark { get; set; }

        /// <summary>
        ///     用于接收数据 非数据库字段
        /// </summary>
        [NotMapped]
        [Display(Name = "邮箱")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", EditShow = true, SortOrder = 1)]
        public string Email { get; set; }

        /// <summary>
        ///     是否实名认证
        /// </summary>
        public IdentityStatus IdentityStatus { get; set; }
    }

    /// <summary>
    ///     应用程序映射配置
    /// </summary>
    public class UserDetailTableMap : MsSqlAggregateRootMap<UserDetail>
    {
        /// <summary>
        ///     映射表
        /// </summary>
        protected override void MapTable(EntityTypeBuilder<UserDetail> builder)
        {
            builder.ToTable("User_UserDetail");
        }

        /// <summary>
        ///     映射属性
        /// </summary>
        protected override void MapProperties(EntityTypeBuilder<UserDetail> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.UserName);
            builder.Ignore(e => e.Version);
        }
    }
}