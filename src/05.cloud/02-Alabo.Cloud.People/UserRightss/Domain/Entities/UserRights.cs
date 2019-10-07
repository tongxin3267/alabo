using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Framework.Core.WebUis;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Cloud.People.UserRightss.Domain.Entities
{
    /// <summary>
    ///     会员权益
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Cloud_People_UserRight")]
    [ClassProperty(Name = "会员权益", Description = "查看会员会员权益", Icon = IconFlaticon.route)]
    public class UserRights : AggregateDefaultUserRoot<UserRights>
    {
        /// <summary>
        ///     等级用户Id
        /// </summary>
        [Display(Name = "所属等级")]
        [Field(ListShow = true, SortOrder = 10, EditShow = true, DataSourceType = typeof(UserGradeConfig),
            ControlsType = ControlsType.DropdownList, DisplayMode = DisplayMode.Grade)]
        public Guid GradeId { get; set; }

        /// <summary>
        ///     累计使用数量，不能高于
        /// </summary>
        [Field(ListShow = true, SortOrder = 20, EditShow = true, ControlsType = ControlsType.TextBox,
            TableDispalyStyle = TableDispalyStyle.Code)]
        [Display(Name = "已使用数量")]
        public long TotalUseCount { get; set; }

        /// <summary>
        ///     单个用户的总数量
        ///     原则上与等级权益配置数量相同
        ///     考虑到可以单独为用户设置的情况
        /// </summary>
        [Display(Name = "总数量")]
        [Field(ListShow = true, SortOrder = 12, EditShow = true, ControlsType = ControlsType.TextBox,
            TableDispalyStyle = TableDispalyStyle.Code)]
        public long TotalCount { get; set; }
    }

    public class UserRightsTableMap : MsSqlAggregateRootMap<UserRights>
    {
        protected override void MapTable(EntityTypeBuilder<UserRights> builder)
        {
            builder.ToTable("Market_UserRights");
        }

        protected override void MapProperties(EntityTypeBuilder<UserRights> builder)
        {
            //应用程序编号
            builder.HasKey(e => e.Id);
        }
    }
}