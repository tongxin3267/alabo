﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Common.Domain.Enum;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Tenants;
using Alabo.UI;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Common.Domain.Entities {

    /// <summary>
    ///     消息发送队列
    ///     在Mongdob的事物未解决之前不能换成Mongdob，在分润中有事物操作
    /// </summary>
    [ClassProperty(Name = "消息发送队列")]
    public class MessageQueue : AggregateDefaultRoot<MessageQueue>, IAutoTable<MessageQueue> {

        /// <summary>
        ///     短信模板id（非模板短信id为0）
        /// </summary>
        [Display(Name = "消息发送队列")]
        [Field(ControlsType = ControlsType.TextBox, Width = "110", ListShow = true, SortOrder = 4)]
        public long TemplateCode { get; set; } = 0;

        /// <summary>
        ///     接受短信的号码
        /// </summary>
        [Display(Name = "接受短信的号码")]
        [Field(ControlsType = ControlsType.TextBox, Width = "110", ListShow = true, SortOrder = 2)]
        public string Mobile { get; set; }

        /// <summary>
        ///     短信文字
        /// </summary>
        [Display(Name = "短信文字")]
        [Field(ControlsType = ControlsType.TextBox, Width = "110", ListShow = true, SortOrder = 5)]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        ///     参数文本
        /// </summary>
        [Display(Name = "参数文本")]
        public string Parameters { get; set; } = string.Empty;

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.TextBox, Width = "110", ListShow = true, SortOrder = 3)]
        public MessageStatus Status { get; set; } = MessageStatus.Pending;

        /// <summary>
        ///     执行记录
        /// </summary>
        [Display(Name = "执行记录")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        ///     操作记录备注信息
        /// </summary>
        [Display(Name = "操作记录备注信息")]
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        ///     请求时间
        /// </summary>
        [Display(Name = "请求时间")]
        public DateTime RequestTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     发送时间
        /// </summary>
        [Display(Name = "发送时间")]
        [Field(ControlsType = ControlsType.TextBox, Width = "110", ListShow = true, SortOrder = 7)]
        public DateTime SendTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     客户端发送IP地址
        /// </summary>
        [Display(Name = "客户端发送IP地址")]
        [Field(ControlsType = ControlsType.TextBox, Width = "110", ListShow = true, SortOrder = 6)]
        public string IpAdress { get; set; } = string.Empty;

        public List<TableAction> Actions() {
            var list = new List<TableAction>
            {
                ToLinkAction("消息发送队列", "/MessageQueue/list")
            };
            return list;
        }

        public PageResult<MessageQueue> PageTable(object query, AutoBaseModel autoModel) {
            var model = Resolve<IMessageQueueService>().GetPagedList(query);//服务重新写一个分页
            var result = ToPageResult(model);
            return result;
        }
    }

    public class MessageQueueTableMap : MsSqlAggregateRootMap<MessageQueue> {

        protected override void MapTable(EntityTypeBuilder<MessageQueue> builder) {
            builder.ToTable("Common_MessageQueue");
        }

        protected override void MapProperties(EntityTypeBuilder<MessageQueue> builder) {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.Version);
            if (TenantContext.IsTenant) {
                // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
            }
        }
    }
}