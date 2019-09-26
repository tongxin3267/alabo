using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Alabo.App.Asset.Withdraws.Domain.Services;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoTables;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Helpers;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Finance.Domain.Dtos.WithDraw {

    /// <summary>
    ///     提现输出模型
    /// </summary>
    [ClassProperty(Name = "提现管理", Icon = "fa fa-puzzle-piece", Description = "提现管理", PostApi = "Api/WithDraw/GetList", PageType = ViewPageType.List, ListApi = "Api/WithDraw/GetList",
        SuccessReturn = "Api/WithDraw/GetList", SideBarType = SideBarType.WithDrawSideBar)]
    public class WithDrawOutput : UIBase, IAutoTable<WithDrawOutput> {//EntityDto

        /// <summary>
        ///     id
        /// </summary>
        [Display(Name = "Id")]
        [Field(ControlsType = ControlsType.Hidden, EditShow = true, ListShow = true)]
        public long Id { get; set; }

        /// <summary>
        ///     userid
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Display(Name = "用户名")]
        [NotMapped]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsShowBaseSerach = true, Width = "80",
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
        [Field(ControlsType = ControlsType.DropdownList, IsShowAdvancedSerach = true,
            DataSource = "Alabo.App.Core.Finance.Domain.CallBacks.MoneyTypeConfig", LabelColor = LabelColor.Info,
            ListShow = true, Width = "80",
            SortOrder = 2)]
        public string MoneyTypeName { get; set; }

        /// <summary>
        ///     Gets or sets the name of the status.
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.Label,
   ListShow = true, GroupTabId = 1,
            Width = "80", SortOrder = 9)]
        public string StatusName { get; set; }

        /// <summary>
        ///     申请金额
        /// </summary>
        [Display(Name = "申请金额")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.NumberRang, IsMain = true, IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, LabelColor = LabelColor.Warning, ListShow = true, GroupTabId = 1, Width = "80",
            SortOrder = 4)]
        [Range(1, 99999999, ErrorMessage = "提现额度必须大于等于0！")]
        public decimal Amount { get; set; }

        /// <summary>
        ///     用户提交备注
        /// </summary>
        [Display(Name = "用户备注")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "120", SortOrder = 10)]
        public string UserRemark { get; set; }

        /// <summary>
        ///     手续费
        /// </summary>
        [Display(Name = "手续费")]
        [Field(ControlsType = ControlsType.NumberRang, TableDispalyStyle = TableDispalyStyle.Code,
            IsShowBaseSerach = true, IsShowAdvancedSerach = true, ListShow = true, Width = "80",
            SortOrder = 5)]
        public decimal Fee { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Display(Name = "申请时间")]
        [Field(ControlsType = ControlsType.DateTimeRang, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            ListShow = true, Width = "100", SortOrder = 8)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     支付时间
        /// </summary>
        [Display(Name = "支付时间")]
        [Field(ControlsType = ControlsType.DateTimeRang,
            ListShow = false, Width = "100", SortOrder = 9)]
        public DateTime PayTime { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [Display(Name = "真实姓名")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "80", SortOrder = 10)]
        public string RealName { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        [Display(Name = "银行卡号")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "100", SortOrder = 11)]
        public string CardId { get; set; }

        /// <summary>
        /// 开户行
        /// </summary>
        [Display(Name = "开户行")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "80", SortOrder = 12)]
        public string BankName { get; set; }

        /// <summary>
        /// 审核操作
        /// </summary>

        [Display(Name = "审核")]
        [Field(ControlsType = ControlsType.ColumnButton, ListShow = true, Width = "80", SortOrder = 12)]
        public ColumnAction ColumnAction { get; set; }

        public List<TableAction> Actions() {
            return new List<TableAction>() {
                //ToLinkAction("查看详情", "/Admin/WithDraw/Edit",TableActionType.ColumnAction),//管理员查看详情
                // ToLinkAction("审核", "/Admin/WithDraw/Edit",TableActionType.FormAction),//管理员审核
            };
        }

        public PageResult<WithDrawOutput> PageTable(object query, AutoBaseModel autoModel) {
            //var userInput = ToQuery<WithDrawApiInput>();

            //if (autoModel.Filter == FilterType.Admin) {
            //    var dic = HttpWeb.HttpContext.ToDictionary();
            //    dic = dic.RemoveKey("type");// 移除该type否则无法正常lambda

            //    var model = Resolve<IWithdrawService>().GetAdminPageList(dic.ToJson());
            //    var view = new PagedList<WithDrawOutput>();
            //    //var moneyTypes= Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>()
            //    foreach (var item in model) {
            //        var outPut = AutoMapping.SetValue<WithDrawOutput>(item);
            //        //moneyTypes.FirstOrDefault(s=>s.Id==item.MoneyTypeConfig.Id);
            //        outPut.MoneyTypeName = item.MoneyTypeConfig?.Name;
            //        // outPut.BankName = item.BankCard.Type.GetDisplayName();
            //        outPut.CardId = item.BankCard?.Number;
            //        outPut.RealName = item.BankCard?.Name;
            //        outPut.BankName = item.BankCard?.Address;
            //        outPut.StatusName = item.StatusName;
            //        if (item.Status == TradeStatus.Pending) {
            //            outPut.ColumnAction = new ColumnAction {
            //                Name = "初审",
            //                Type = typeof(UI.AutoForm.WithdrawReviewAutoForm).Name
            //            };
            //        } else if (item.Status == TradeStatus.FirstCheckSuccess) {
            //            outPut.ColumnAction = new ColumnAction {
            //                Name = "二审",
            //                Type = typeof(UI.AutoForm.WithdrawReviewAutoForm).Name
            //            };
            //        } else {
            //            outPut.ColumnAction = new ColumnAction {
            //                Name = "查看",
            //                Type = typeof(UI.AutoForm.WithdrawResultwAutoForm).Name
            //            };
            //        }
            //        view.Add(outPut);
            //    }
            //    return ToPageResult(view);
            //}
            //if (autoModel.Filter == FilterType.User) {
            //    userInput.UserId = autoModel.BasicUser.Id;
            //    var model = Resolve<IWithdrawService>().GetUserList(userInput);
            //    var view = new PagedList<WithDrawOutput>();
            //    foreach (var item in model) {
            //        var outPut = AutoMapping.SetValue<WithDrawOutput>(item);
            //        view.Add(outPut);
            //    }
            //    return ToPageResult(view);
            //} else {
            //    throw new ValidException("类型权限不正确");
            //}
            return null;
        }
    }

    /// <summary>
    ///     Class WithDrawShowOutput.
    /// </summary>
    [ClassProperty(Name = "提现管理", Icon = "fa fa-puzzle-piece", Description = "提现管理", PostApi = "Api/WithDraw/Add",
        SuccessReturn = "Api/WithDraw/Get")]
    public class WithDrawShowOutput {

        /// <summary>
        ///     id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     userid
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Display(Name = "用户名")]
        [NotMapped]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 1)]
        public string UserName { get; set; }

        public MoneyTypeConfig MoneyTypeConfig { get; set; }

        /// <summary>
        ///     moneyTypeId
        /// </summary>
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     货币类型
        /// </summary>
        [Display(Name = "申请账户")]
        public string MoneyTypeName { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        public string Status { get; set; }

        /// <summary>
        ///     显示银行卡信息
        /// </summary>
        [Display(Name = "银行卡信息")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.NumberRang, IsMain = true, IsShowBaseSerach = false,
            IsShowAdvancedSerach = false, LabelColor = LabelColor.Warning, ListShow = false, GroupTabId = 1,
            Width = "80",
            SortOrder = 4)]
        public string BankCardInfo { get; set; }

        /// <summary>
        ///     申请金额
        /// </summary>
        [Display(Name = "申请金额")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "80", SortOrder = 4)]
        public decimal Amount { get; set; }

        /// <summary>
        ///     手续费
        /// </summary>
        [Display(Name = "手续费")]
        public decimal Fee { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Display(Name = "申请时间")]
        //[Field(ControlsType = ControlsType.DateTimeRang, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
        //    ListShow = true, Width = "100", SortOrder = 8)]
        public string CreateTime { get; set; }

        /// <summary>
        ///     支付时间
        /// </summary>
        [Display(Name = "支付时间")]
        //[Field(ControlsType = ControlsType.DateTimeRang, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
        //    ListShow = true, Width = "100", SortOrder = 9)]
        public string PayTime { get; set; }

        /// <summary>
        ///     管理员备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        ///     用户备注
        /// </summary>
        [Field(ControlsType = ControlsType.TextArea, SortOrder = 10)]
        public string UserRemark { get; set; }
    }
}