using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoTables;
using Alabo.Web.Mvc.Attributes;
using ZKCloud.Open.Message.Models;

namespace _01_Alabo.Cloud.Core.SendSms.UI {

    public class MessageRecordAutoTable : UIBase, IAutoTable<MessageRecordAutoTable> {
        public long UserId { get; set; }

        public long Id { get; set; }

        public Guid ProjectId { get; set; }

        public int ChannelId { get; set; }

        /// <summary>
        /// 模块ID
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, Width = "100", ListShow = true, SortOrder = 2)]
        [Display(Name = "手机号")]
        public string Mobile { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, Width = "100", ListShow = true, SortOrder = 5)]
        [Display(Name = "短信内容")]
        public string Content { get; set; }

        public string Parameters { get; set; }

        /// <summary>
        /// 短信状态
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, DataSource = "ZKCloud.Open.Message.Models.MessageStatus", Width = "100", ListShow = true, SortOrder = 2)]
        [Display(Name = "短信状态")]
        public MessageStatus Status { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime RequestTime { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [Display(Name = "发送时间")]
        [Field(ControlsType = ControlsType.TextBox, Width = "100", ListShow = true, SortOrder = 10)]
        public DateTime SendTime { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAdress { get; set; }

        public List<TableAction> Actions() {
            return new List<TableAction>();
        }

        public PageResult<MessageRecordAutoTable> PageTable(object query, AutoBaseModel autoModel) {
            return ToPageResult(new PagedList<MessageRecordAutoTable>());
        }
    }
}