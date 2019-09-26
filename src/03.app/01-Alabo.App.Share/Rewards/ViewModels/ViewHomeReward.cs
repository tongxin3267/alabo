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

using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Share.Share.Domain.Dto;
using Alabo.App.Share.Share.Domain.Entities;
using Alabo.App.Share.Share.Domain.Enums;
using Alabo.App.Share.Share.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoLists;
using Alabo.Framework.Core.WebUis.Design.AutoTables;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Helpers;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.Share.ViewModels {

    /// <summary>
    ///     Class ViewHomeReward.
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Share_ViewHomeReward")]
    [ClassProperty(Name = "我的分润", Icon = "fa fa-puzzle-piece", Description = "我的分润", PageType = ViewPageType.List, PostApi = "Api/Reward/RewardList", ListApi = "Api/Reward/RewardList")]
    public class ViewHomeReward : UIBase, IAutoTable<ViewHomeReward>, IAutoList {

        /// <summary>
        ///     Gets or sets the serial.
        /// </summary>
        /// <value>
        ///     The serial.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        ///     触发分润用户的Id
        /// </summary>
        public long OrderUserId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the user.
        /// </summary>
        /// <value>
        ///     The name of the user.
        /// </value>
        [Display(Name = "订单用户")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true, Width = "100",
            ListShow = true, SortOrder = 3)]
        public string OrderUserName { get; set; }

        /// <summary>
        ///     Gets or sets the name of the task module attribute.
        /// </summary>
        /// <value>
        ///     The name of the task module attribute.
        /// </value>
        [Display(Name = "所属维度")]
        //[Field(ControlsType = ControlsType.TextBox, Width = "100", ListShow = true, SortOrder = 4)]
        public string TaskModuleAttributeName { get; set; }

        /// <summary>
        ///     Gets or sets the name of the money type.
        /// </summary>
        /// <value>
        ///     The name of the money type.
        /// </value>
        [Display(Name = "资产账户")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Default, IsMain = true, Width = "100",
            ListShow = true, SortOrder = 6)]
        public string MoneyTypeName { get; set; }

        /// <summary>
        ///     Gets or sets the amount.
        /// </summary>
        /// <value>
        ///     The amount.
        /// </value>
        [Display(Name = "分润金额")]
        [Field(ControlsType = ControlsType.NumberRang, LabelColor = LabelColor.Info, IsShowAdvancedSerach = true,
            IsShowBaseSerach = true, IsMain = true, Width = "100", ListShow = true, SortOrder = 7)]
        public decimal Amount { get; set; }

        /// <summary>
        ///     Gets or sets the AfterAmount.
        /// </summary>
        /// <value>
        ///     The amount.
        /// </value>
        [Display(Name = "账户金额")]
        [Field(ControlsType = ControlsType.NumberRang, LabelColor = LabelColor.Primary, IsShowAdvancedSerach = true,
            IsShowBaseSerach = true, Width = "100", ListShow = true, SortOrder = 8)]
        public decimal AfterAmount { get; set; }

        /// <summary>
        ///     分润状态
        /// </summary>
        [Display(Name = "分润状态")]
        [Field(ControlsType = ControlsType.DropdownList, IsTabSearch = true,
            DataSource = "Alabo.App.Share.Share.Domain.Enums.FenRunStatus", Width = "90", ListShow = false,
            SortOrder = 9)]
        public FenRunStatus Status { get; set; }

        /// <summary>
        ///     分润类型Id：在分润维度设计的配置模块
        /// </summary>
        public Guid ModuleId { get; set; }

        /// <summary>
        ///     分润维度说明
        /// </summary>
        [Display(Name = "详情")]
        [Field(ControlsType = ControlsType.TextBox, Width = "200", ListShow = true, SortOrder = 8)]
        public string Intro { get; set; }

        /// <summary>
        ///     根据Id自动生成12位序列号
        /// </summary>
        [Display(Name = "编号")]
        [Field(ControlsType = ControlsType.TextBox, TableDispalyStyle = TableDispalyStyle.Code, Width = "100",
            ListShow = true, SortOrder = 1)]
        public string Serial {
            get {
                var searSerial = $"9{Id.ToString().PadLeft(8, '0')}";
                if (Id.ToString().Length >= 9) {
                    searSerial = $"{Id.ToString()}";
                }

                return searSerial;
            }
        }

        public List<TableAction> Actions() {
            return new List<TableAction>();
        }

        public PageResult<AutoListItem> PageList(object query, AutoBaseModel autoModel) {
            var dic = query.ToObject<Dictionary<string, string>>();

            dic.TryGetValue("loginUserId", out string userId);
            dic.TryGetValue("pageIndex", out string pageIndexStr);
            var pageIndex = pageIndexStr.ToInt64();
            if (pageIndex <= 0) {
                pageIndex = 1;
            }
            var temp = new ExpressionQuery<Reward> {
                EnablePaging = true,
                PageIndex = (int)pageIndex,
                PageSize = (int)15
            };
            temp.And(e => e.UserId == userId.ToInt64());
            temp.OrderByDescending(s => s.CreateTime);
            //temp.And(u => u.Type == TradeType.Withraw);
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var model = Resolve<IRewardService>().GetPagedList(temp);
            var users = Resolve<IUserDetailService>().GetList();
            var list = new List<AutoListItem>();
            foreach (var item in model) {
                var apiData = new AutoListItem {
                    Title = moneyTypes.FirstOrDefault(u => u.Id == item.MoneyTypeId)?.Name,// + " - " + item.Type.GetDisplayName(),
                    Intro = item.Intro,//$"{item.CreateTime}",
                    Value = item.Amount,
                    Image = users.FirstOrDefault(u => u.UserId == item.UserId)?.Avator,
                    Id = item.Id,
                    Url = $"/pages/index?path=share_show&id={item.Id}"
                };
                list.Add(apiData);
            }
            return ToPageList(list, model);
        }

        public PageResult<ViewHomeReward> PageTable(object query, AutoBaseModel autoModel) {
            var userInput = ToQuery<RewardInput>();
            if (autoModel.Filter == FilterType.Admin) {
                var rewardList = Resolve<IRewardService>().GetViewRewardPageList(userInput, HttpWeb.HttpContext);
                // var users = Resolve<IUserService>().GetList();
                var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
                var result = new List<ViewHomeReward>();
                rewardList.ForEach(u => {
                    var view = AutoMapping.SetValue<ViewHomeReward>(u);
                    view.Amount = u.RewardAmount;
                    //view.OrderUserName = Resolve<IUserService>().GetHomeUserStyle(users.FirstOrDefault(z => z.Id == u.UserId));
                    //view.MoneyTypeName = moneyTypes.FirstOrDefault(z => z.Id == u.MoneyTypeId)?.Name;
                    result.Add(view);
                });
                var model = PagedList<ViewHomeReward>.Create(result, rewardList.RecordCount, rewardList.PageSize, rewardList.PageIndex);
                return ToPageResult(model);
            }
            if (autoModel.Filter == FilterType.User) {
                userInput.UserId = autoModel.BasicUser.Id;
                var rewardList = Resolve<IRewardService>().GetViewRewardPageList(userInput, HttpWeb.HttpContext);
                var userIds = rewardList.Select(r => r.OrderUserId).ToList();

                var users = Resolve<IUserService>().GetList(userIds);
                var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
                var result = new List<ViewHomeReward>();
                rewardList.ForEach(u => {
                    var view = AutoMapping.SetValue<ViewHomeReward>(u);
                    view.Amount = u.RewardAmount;
                    view.OrderUserName = string.Empty;
                    view.Id = u.RewardId;
                    var orderUser = users.FirstOrDefault(r => r.Id == u.OrderUserId);
                    view.OrderUserName = orderUser?.GetUserName();
                    //view.OrderUserName = Resolve<IUserService>().GetHomeUserStyle(users.FirstOrDefault(z => z.Id == u.UserId));
                    //view.MoneyTypeName = moneyTypes.FirstOrDefault(z => z.Id == u.MoneyTypeId)?.Name;
                    result.Add(view);
                });
                var model = PagedList<ViewHomeReward>.Create(result, rewardList.RecordCount, rewardList.PageSize, rewardList.PageIndex);
                return ToPageResult(model);
            }

            return null;
        }

        public Type SearchType() {
            return typeof(Reward);
        }

        //#region

        ///// <summary>
        ///// Gets or sets the reward.
        ///// </summary>
        ///// <value>The reward.</value>
        //public Reward Reward { get; set; }

        ///// <summary>
        ///// Gets or sets the share user.
        ///// </summary>
        ///// <value>The share user.</value>
        //public User ShareUser { get; set; }

        ///// <summary>
        ///// Gets or sets the order user.
        ///// </summary>
        ///// <value>The order user.</value>
        //public User OrderUser { get; set; }

        ///// <summary>
        ///// Gets or sets the type of the money.
        ///// </summary>
        ///// <value>The type of the money.</value>
        //public MoneyTypeConfig MoneyType { get; set; }

        ///// <summary>
        ///// Gets or sets the share module.
        ///// </summary>
        ///// <value>The share module.</value>
        //public ShareModule ShareModule { get; set; }

        ///// <summary>
        ///// Gets or sets the task module attribute.
        ///// </summary>
        ///// <value>The task module attribute.</value>
        //public TaskModuleAttribute TaskModuleAttribute { get; set; }

        //#endregion
    }
}