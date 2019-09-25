using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Asset.Withdraws.Domain.Enums;
using Alabo.App.Asset.Withdraws.Domain.Services;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Dtos.WithDraw;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Exceptions;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.UI.AutoTables;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.ViewModels.WithDraw {

    /// <summary>
    ///     Class ViewAdminWithDraw.
    /// </summary>
    [ClassProperty(Name = "提现管理", Icon = "fa fa-puzzle-piece", Description = "提现管理",
        SideBarType = SideBarType.FinanceSideBar)]
    public class ViewAdminWithDraw : UIBase, IAutoTable<ViewAdminWithDraw> {

        /// <summary>
        ///     序列号
        ///     10位数序列号
        /// </summary>
        [Display(Name = "交易号")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, IsMain = true, GroupTabId = 1, Width = "80", SortOrder = 1)]
        public string Serial { get; set; }

        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     交易用户
        /// </summary>
        public Users.Entities.User User { get; set; }

        /// <summary>
        ///     Gets or sets 会员Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the 会员.
        /// </summary>
        [Display(Name = "交易用户")]
        [NotMapped]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, IsMain = true, GroupTabId = 1, Width = "100",
            Link = "/Admin/User/Edit?id=[[UserId]]", SortOrder = 2)]
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the 会员 grade.
        /// </summary>
        public UserGradeConfig UserGrade { get; set; }

        /// <summary>
        ///     银行卡信息
        /// </summary>
        public BankCard BankCard { get; set; }

        /// <summary>
        ///     Gets or sets the amount.
        /// </summary>
        [Display(Name = "申请金额")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.NumberRang, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            LabelColor = LabelColor.Warning, ListShow = true, GroupTabId = 1, Width = "80", SortOrder = 4)]
        [Range(1, 99999999, ErrorMessage = ErrorMessage.NameNotInRang)]
        public decimal Amount { get; set; }

        /// <summary>
        ///     Gets or sets the after amount.
        /// </summary>
        [Display(Name = "账后金额")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            LabelColor = LabelColor.Brand, ListShow = true, GroupTabId = 1, Width = "80", SortOrder = 6)]
        public decimal AfterAmount { get; set; }

        /// <summary>
        ///     手续费
        /// </summary>
        [Display(Name = "手续费")]
        public decimal Fee { get; set; }

        /// <summary>
        ///     Gets or sets the actual amount.
        /// </summary>
        [Display(Name = "实际金额")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            LabelColor = LabelColor.Default, ListShow = true, GroupTabId = 1, Width = "80", SortOrder = 7)]
        public decimal ActualAmount { get; set; }

        /// <summary>
        ///     Gets or sets the check amount.
        /// </summary>
        [Display(Name = "应付人民币")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            LabelColor = LabelColor.Info, ListShow = true, GroupTabId = 1, Width = "80", SortOrder = 8)]
        public decimal CheckAmount { get; set; }

        /// <summary>
        ///     Gets or sets the failured reason.
        /// </summary>
        [Display(Name = "不通过原因")]
        public string FailuredReason { get; set; }

        /// <summary>
        ///     Gets or sets the remark.
        /// </summary>
        [Display(Name = "汇款备注")]
        public string Remark { get; set; }

        /// <summary>
        ///     Gets or sets the create time.
        /// </summary>
        [Display(Name = "申请时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.DropdownList,
            DataSourceType = typeof(WithdrawStatus), IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, GroupTabId = 1, Width = "80", SortOrder = 9)]
        public WithdrawStatus Status { get; set; }

        /// <summary>
        ///     Gets or sets the name of the status.
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.DropdownList,
            DataSourceType = typeof(WithdrawStatus), ListShow = true, GroupTabId = 1,
            Width = "80", SortOrder = 9)]
        public string StatusName { get; set; }

        /// <summary>
        ///     Gets or sets the money 类型 configuration.
        /// </summary>
        public MoneyTypeConfig MoneyTypeConfig { get; set; }

        /// <summary>
        ///     Gets or sets the intro.
        /// </summary>
        public string Intro { get; set; }

        /// <summary>

        /// <summary>
        ///     Json 格式的银行卡信息
        /// </summary>
        public string ExtraDate { get; set; }

        /// <summary>
        ///     Gets or sets the 会员 remark.
        /// </summary>
        [Display(Name = "用户备注")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, GroupTabId = 1, Width = "120", SortOrder = 10)]
        public string UserRemark { get; set; }

        /// <summary>
        ///     Gets or sets the pay password.
        /// </summary>
        [Display(Name = "支付密码")]
        [Required(ErrorMessage = "请填写您的支付密码，不可以为空")]
        public string PayPassword { get; set; }

        public List<TableAction> Actions() {
            return new List<TableAction>();
        }

        public PageResult<ViewAdminWithDraw> PageTable(object query) {
            var model = Resolve<IWithdrawService>().GetAdminPageList(query);
            return ToPageResult(model);
        }

        public PageResult<ViewAdminWithDraw> PageTable(object query, AutoBaseModel autoModel) {
            var userInput = ToQuery<WithDrawApiInput>();

            if (autoModel.Filter == FilterType.Admin) {
                //var model = Resolve<IWithdrawService>().GetUserList(userInput);
                //var view = new PagedList<ViewAdminWithDraw>();
                //foreach (var item in model) {
                //    var outPut = AutoMapping.SetValue<ViewAdminWithDraw>(item);
                //    view.Add(outPut);
                //}
                //return ToPageResult(view);
            }
            if (autoModel.Filter == FilterType.User) {
                //// userInput.UserId = autoModel.BasicUser.Id;
                //// userInput.LoginUserId = autoModel.BasicUser.Id;
                //var model = Resolve<IWithdrawService>().GetUserList(userInput);
                //var view = new PagedList<ViewAdminWithDraw>();
                //foreach (var item in model) {
                //    var outPut = AutoMapping.SetValue<ViewAdminWithDraw>(item);
                //    view.Add(outPut);
                //}
                //return ToPageResult(view);
            } else {
                throw new ValidException("类型权限不正确");
            }

            return null;
        }
    }
}