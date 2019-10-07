using Alabo.App.Asset.Recharges.Domain.Enums;
using Alabo.App.Asset.Recharges.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Extensions;
using Alabo.UI;
using Alabo.UI.Design.AutoTables;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.App.Asset.Recharges.Dtos
{
    /// <summary>
    ///     充值管理
    /// </summary>
    [ClassProperty(Name = "充值管理", Icon = "fa fa-puzzle-piece", Description = "充值管理")]
    public class RechargeOutput : UIBase, IAutoTable<RechargeOutput>
    {
        /// <summary>
        ///     id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     userid
        /// </summary>
        public long UserId { get; set; }

        public string Extension { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Display(Name = "用户名")]
        [NotMapped]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsShowBaseSerach = true, Width = "150",
            Link = "/Admin/User/Edit?id=[[UserId]]", IsShowAdvancedSerach = true, SortOrder = 1)]
        public string UserName { get; set; }

        /// <summary>
        ///     moneyTypeId
        /// </summary>
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     货币类型
        /// </summary>
        [Display(Name = "申请账户")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Info, ListShow = true, Width = "80",
            SortOrder = 2)]
        public string MoneyTypeName { get; set; }

        /// <summary>
        ///     申请金额
        /// </summary>
        [Display(Name = "充值金额")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.NumberRang, IsMain = true, IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, LabelColor = LabelColor.Warning, ListShow = true, GroupTabId = 1, Width = "80",
            SortOrder = 4)]
        [Range(1, 99999999, ErrorMessage = "金额必须大于等于0！")]
        public decimal Amount { get; set; }

        /// <summary>
        ///     交易状态
        /// </summary>
        /// <value>
        ///     The status.
        /// </value>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.DropdownList,
            DataSource = "Alabo.App.Core.Finance.Domain.Enums.TradeStatus", ListShow = false, EditShow = true,
            Width = "80",
            SortOrder = 10)]
        public RechargeStatus Status { get; set; } = RechargeStatus.Pending;

        /// <summary>
        ///     交易状态
        /// </summary>
        /// <value>
        ///     The status.
        /// </value>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.TextBox, Width = "80", LabelColor = LabelColor.Info, EditShow = false,
            ListShow = true, SortOrder = 9)]
        public string StatusName
        {
            get => Status.GetDisplayName();
            set => _ = value;
        }

        /// <summary>
        ///     交易类型
        /// </summary>
        [Display(Name = "交易类型")]
        [Field(ControlsType = ControlsType.DropdownList,
            DataSource = "Alabo.App.Core.Finance.Domain.Enums.RechargeType", ListShow = false, Width = "80",
            SortOrder = 6)]
        public RechargeType RechargeType { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Field(ControlsType = ControlsType.DateTimeRang,
            ListShow = true, Width = "120", SortOrder = 8)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     支付时间
        /// </summary>
        [Display(Name = "充值时间")]
        [Field(ControlsType = ControlsType.DateTimeRang,
            ListShow = true, Width = "120", SortOrder = 10)]
        public DateTime PayTime { get; set; }

        /// <summary>
        ///     扩展信息
        /// </summary>
        public string ExtraDate { get; set; }

        public List<TableAction> Actions()
        {
            return null;
            //var list = new List<TableAction>
            //{
            //    ToLinkAction("充值管理", "/RechargeOutput/list")
            //};
            //return list;
        }

        public PageResult<RechargeOutput> PageTable(object query, AutoBaseModel autoModel)
        {
            //    var queryInput = ToQuery<RechargeOutputPara>();
            //    if (queryInput.Amount != null) {
            //        var i = 0M;
            //        var b = Decimal.TryParse(queryInput.Amount, out i);
            //        if (i == 0) {
            //            throw new ValidException("查询金额格式不正确");
            //        }
            //    }

            //    var userInput = ToQuery<RechargeAddInput>();

            //    var userService = Resolve<IUserService>();
            //    var moneyType = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            //    if (autoModel.Filter == FilterType.Admin) {
            //        var model = Resolve<IRechargeService>().GetUserList(userInput);

            //        var users = userService.GetList(s => model.Select(i => i.UserId).Contains(s.Id));
            //        var view = new PagedList<RechargeOutput>();
            //        foreach (var item in model) {
            //            var outPut = AutoMapping.SetValue<RechargeOutput>(item);
            //            //申请账号
            //            outPut.MoneyTypeName = moneyType.SingleOrDefault(s => s.Id == item.MoneyTypeId)?.Name;
            //            //用户
            //            var user = users.SingleOrDefault(s => s.Id == item.UserId);
            //            if (user != null) {
            //                outPut.UserName = $"{user.Name}({user.UserName})";
            //            }

            //            view.Add(outPut);
            //        }
            //        if (queryInput.UserName != null) {
            //            var result = view.Where(p => p.UserName == queryInput.UserName).ToList();

            //            foreach (var item in result) {
            //                view.Add(item);
            //            }
            //        }
            //        return ToPageResult(view);
            //    }
            //    if (autoModel.Filter == FilterType.User) {
            //        userInput.LoginUserId = autoModel.BasicUser.Id;
            //        var model = Resolve<IRechargeService>().GetUserList(userInput);
            //        var users = userService.GetList(s => model.Select(i => i.UserId).Contains(s.Id));
            //        var view = new PagedList<RechargeOutput>();
            //        foreach (var item in model) {
            //            var outPut = AutoMapping.SetValue<RechargeOutput>(item);
            //            //申请账号
            //            outPut.MoneyTypeName = moneyType.SingleOrDefault(s => s.Id == item.MoneyTypeId)?.Name;
            //            //用户
            //            var user = users.SingleOrDefault(s => s.Id == item.UserId);
            //            if (user != null) {
            //                outPut.UserName = $"{user.Name}({user.UserName})";
            //            }

            //            view.Add(outPut);
            //        }
            //        return ToPageResult(view);
            //    } else {
            //        throw new ValidException("类型权限不正确");
            //    }
            //}
            return null;
        }

        /// <summary>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PageResult<RechargeOutput> PageTable(object query)
        {
            var model = Resolve<IRechargeService>().GetPageList(query);
            var result = ToPageResult(model);
            return result;
        }

        /// <summary>
        /// </summary>
        public class RechargeOutputPara : PagedInputDto
        {
            public string UserName { get; set; }

            public string Amount { get; set; }
        }
    }
}