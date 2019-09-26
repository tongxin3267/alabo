// ***********************************************************************
// Assembly         : Alabo.App.Share
// Author           : zkclo
// Created          : 03-06-2018
//
// Last Modified By : zkclo
// Last Modified On : 04-06-2018
// ***********************************************************************
// <copyright file="ViewAdminReward.cs" company="Alabo.App.Share">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.App.Share.Share.Domain.Entities;
using Alabo.App.Share.Share.Domain.Enums;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Users.Entities;
using ZKCloud.Open.Share.Models;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Share.Share.ViewModels {

    /// <summary>
    ///     Class ViewAdminReward.
    /// </summary>
    [ClassProperty(Name = "分润数据", Icon = "fa fa-puzzle-piece", Description = "分润数据")]
    public class ViewAdminReward : BaseViewModel {

        /// <summary>
        ///     Gets or sets the reward.
        /// </summary>
        /// <value>The reward.</value>
        public Reward Reward { get; set; }

        /// <summary>
        ///     Gets or sets the share user.
        /// </summary>
        /// <value>The share user.</value>
        public User ShareUser { get; set; }

        /// <summary>
        ///     Gets or sets the order user.
        /// </summary>
        /// <value>The order user.</value>
        public User OrderUser { get; set; }

        /// <summary>
        ///     Gets or sets the type of the money.
        /// </summary>
        /// <value>The type of the money.</value>
        public MoneyTypeConfig MoneyType { get; set; }

        /// <summary>
        ///     Gets or sets the share module.
        /// </summary>
        /// <value>The share module.</value>
        public ShareModule ShareModule { get; set; }

        /// <summary>
        ///     Gets or sets the task module attribute.
        /// </summary>
        /// <value>The task module attribute.</value>
        public TaskModuleAttribute TaskModuleAttribute { get; set; }

        /// <summary>
        ///     根据Id自动生成12位序列号
        /// </summary>
        [Display(Name = "编号")]
        [Field(ListShow = true, EditShow = true, SortOrder = 1, Link = "/Admin/Reward/Edit?id=[[RewardId]]", Width = "75")]
        public string Serial {
            get {
                var searSerial = $"9{Reward.Id.ToString().PadLeft(9, '0')}";
                if (Reward.Id.ToString().Length == 10) {
                    searSerial = $"{Reward.Id.ToString()}";
                }

                return searSerial;
            }
        }

        public long RewardId { get; set; }

        public long OrderUserId { get; set; }
        public long ShareUserId { get; set; }
        public Guid ModuleId { get; set; }
        public long ShareModuleId { get; set; }

        /// <summary>
        ///     获得分润用户
        /// </summary>
        /// <value>The share user.</value>
        [Required]
        [Display(Name = "分润用户")]
        [Field(ListShow = true, EditShow = false, SortOrder = 2, Link = "/admin/user/edit?id=[[ShareUserId]]", Width = "100")]
        public string ShareUserName { get; set; }

        /// <summary>
        ///     触发分润用户
        /// </summary>
        [Required]
        [Display(Name = "触发用户")]
        [Field(ListShow = true, EditShow = false, SortOrder = 3, Link = "/admin/user/edit?id=[[OrderUserId]]", Width = "100")]
        public string OrderUserName { get; set; }

        /// <summary>
        /// 所属维度
        /// </summary>
        [Required]
        [Display(Name = "所属维度")]
        [Field(ListShow = true, EditShow = false, SortOrder = 4, Link = "/Admin/Reward/ModuleConfigList?moduleid=[[ModuleId]]", Width = "100")]
        public string TaskModuleAttributeName { get; set; }

        /// <summary>
        /// 奖金/配置
        /// </summary>
        [Required]
        [Display(Name = "奖金/配置")]
        [Field(ListShow = true, EditShow = false, SortOrder = 5, Link = "/Admin/Reward/EditMoudle?id=[[ShareModuleId]]&moduleId=[[ModuleId]]", Width = "100")]
        public string ShareModuleName { get; set; }

        /// <summary>
        /// 资产账户
        /// </summary>
        [Required]
        [Display(Name = "资产账户")]
        [Field(ListShow = true, EditShow = false, SortOrder = 6, LabelColor = LabelColor.Brand, Width = "100")]
        public string MoneyTypeName { get; set; }

        /// <summary>
        /// 分润金额
        /// </summary>
        [Required]
        [Display(Name = "分润金额")]
        [Field(ListShow = true, EditShow = false, SortOrder = 7, TableDispalyStyle = TableDispalyStyle.Code, Width = "100")]
        public decimal RewardAmount { get; set; }

        /// <summary>
        /// 账后金额
        /// </summary>
        [Required]
        [Display(Name = "账后金额")]
        [Field(ListShow = true, EditShow = false, SortOrder = 8, TableDispalyStyle = TableDispalyStyle.Code, Width = "100")]
        public decimal AfterAmount { get; set; }

        /// <summary>
        /// 详情
        /// </summary>
        [Required]
        [Display(Name = "详情")]
        [Field(ListShow = true, EditShow = false, SortOrder = 9, Width = "300")]
        public string Intro { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        [Display(Name = "状态")]
        [Field(ListShow = true, EditShow = false, SortOrder = 10, Width = "80")]
        public FenRunStatus Status { get; set; }

        /// <summary>
        /// 分润时间
        /// </summary>
        [Required]
        [Display(Name = "分润时间")]
        [Field(ListShow = true, EditShow = false, SortOrder = 11, Width = "110")]
        public string CreateTimeStr { get; set; }

        /// <summary>
        ///     获取链接
        /// </summary>
        public IEnumerable<ViewLink> ViewLinks() {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("明细", "/Admin/Reward/Edit?id=[[RewardId]]", Icons.Edit, LinkType.ColumnLink)
            };
            return quickLinks;
        }
    }
}