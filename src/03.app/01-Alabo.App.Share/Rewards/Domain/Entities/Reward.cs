using System;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Share.Rewards.Domain.Enums;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.App.Share.Rewards.Domain.Entities
{
    /// <summary>
    ///     分润明细表，奖金表
    ///     奖金明细，奖励明细
    /// </summary>
    [ClassProperty(Name = "奖励明细", PageType = ViewPageType.List)]
    [BsonIgnoreExtraElements]
    public class Reward : AggregateDefaultUserRoot<Reward>
    {
        /// <summary>
        ///     分润订单Id,对应ShareOrder表，不是商城的Order_Order 表
        /// </summary>
        [Display(Name = "分润订单Id")]
        public long OrderId { get; set; }

        /// <summary>
        ///     触发分润用户的Id
        /// </summary>
        [Required]
        [Display(Name = "触发用户")]
        [Field(ListShow = true, EditShow = true, SortOrder = 3)]
        public long OrderUserId { get; set; }

        /// <summary>
        ///     分润货币ID
        /// </summary>
        [Required]
        [Display(Name = "货币类型")]
        [Field(ListShow = true, EditShow = true, SortOrder = 4)]
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     分润金额
        /// </summary>
        [Required]
        [Display(Name = "金额")]
        [Field(ListShow = true, EditShow = true, SortOrder = 1)]
        public decimal Amount { get; set; }

        /// <summary>
        ///     分润后改账户金额
        /// </summary>
        [Required]
        [Display(Name = "账后金额")]
        [Field(ListShow = true, EditShow = true)]
        public decimal AfterAmount { get; set; }

        /// <summary>
        ///     分润状态
        /// </summary>
        [Required]
        [Display(Name = "分润状态")]
        [Field(ListShow = true, EditShow = true, SortOrder = 1)]
        public FenRunStatus Status { get; set; }

        /// <summary>
        ///     分润类型Id：在分润维度设计的配置模块
        /// </summary>
        [Required]
        [Display(Name = "分润类型Id")]
        public Guid ModuleId { get; set; }

        /// <summary>
        ///     分润配置Id
        /// </summary>
        [Display(Name = "分润配置Id")]
        public long ModuleConfigId { get; set; }

        /// <summary>
        ///     分润简要介绍
        ///     有分润维度的日志模板生成
        /// </summary>
        [Required]
        [Display(Name = "简介")]
        [Field(ListShow = true, EditShow = true)]
        public string Intro { get; set; }

        /// <summary>
        ///     根据Id自动生成12位序列号
        /// </summary>
        [Display(Name = "根据Id自动生成12位序列号")]
        public string Serial
        {
            get
            {
                var searSerial = $"9{Id.ToString().PadLeft(9, '0')}";
                if (Id.ToString().Length == 10) searSerial = $"{Id.ToString()}";

                return searSerial;
            }
        }
    }

    public class DetailTableMap : MsSqlAggregateRootMap<Reward>
    {
        protected override void MapTable(EntityTypeBuilder<Reward> builder)
        {
            builder.ToTable("Share_Reward");
        }

        protected override void MapProperties(EntityTypeBuilder<Reward> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.MoneyTypeId).HasColumnType("uniqueidentifier");
            builder.Property(e => e.Intro).HasMaxLength(255);
            builder.Ignore(e => e.Serial);
            builder.Ignore(e => e.Version);
        }
    }
}