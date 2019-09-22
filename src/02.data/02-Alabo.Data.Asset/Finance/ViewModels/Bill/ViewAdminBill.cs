using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Dtos.Bill;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Finance.ViewModels.Bill {

    /// <summary>
    ///     Class ViewAdminBill.
    /// </summary>
    [ClassProperty(Name = "财务明细", Icon = "fa fa-puzzle-piece", SideBarType = SideBarType.FullScreen, PageType = ViewPageType.List, PostApi = "Api/Bill/ViewBillList", ListApi = "Api/Bill/ViewBillList")]
    public class ViewAdminBill : UIBase, IAutoTable<ViewAdminBill> {

        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Gets or sets the serial.
        /// </summary>
        [Display(Name = "序号")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, IsMain = true, GroupTabId = 1, Width = "100",
            Link = "/Admin/Bill/Edit?id=[[Id]]", SortOrder = 1)]
        public string Serial { get; set; }

        /// <summary>
        ///     Gets or sets the bill.
        /// </summary>
        public Domain.Entities.Bill Bill { get; set; }

        /// <summary>
        ///     交易用户
        /// </summary>
        public User.Domain.Entities.User User { get; set; }

        /// <summary>
        ///     Gets or sets the name of the 会员.
        /// </summary>
        [Display(Name = "交易用户")]
        [NotMapped]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, IsMain = true, GroupTabId = 1, Width = "80",
            //Link = "/Admin/User/Edit?id=[[UserId]]",
            SortOrder = 2)]
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the amount.
        /// </summary>
        [Display(Name = "金额")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            LabelColor = LabelColor.Warning, ListShow = true, GroupTabId = 1, Width = "80", SortOrder = 5)]
        public decimal? Amount { get; set; }

        /// <summary>
        ///     Gets or sets the after amount.
        /// </summary>
        [Display(Name = "账后金额")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            LabelColor = LabelColor.Brand, ListShow = true, GroupTabId = 1, Width = "150", SortOrder = 6)]
        public decimal? AfterAmount { get; set; }

        /// <summary>
        ///     Gets or sets 会员Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     Gets or sets the other 会员.
        /// </summary>
        public User.Domain.Entities.User OtherUser { get; set; }

        /// <summary>
        ///     Gets or sets the name of the other 会员.
        /// </summary>
        [Display(Name = "对方用户")]
        [NotMapped]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, IsMain = true, GroupTabId = 1, Width = "80",
            Link = "/Admin/User/Edit?id=[[OtherUserId]]", SortOrder = 7)]
        public string OtherUserName { get; set; }

        /// <summary>
        ///     Gets or sets the other 会员 identifier.
        /// </summary>
        public long OtherUserId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the bill 类型.
        /// </summary>
        [Display(Name = "交易类型")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "80", ListShow = true, SortOrder = 8)]
        public string BillTypeName { get; set; }

        /// <summary>
        ///     Gets or sets the creatime string.
        /// </summary>
        [Display(Name = "交易时间")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "120", SortOrder = 10)]
        public string CreatimeStr { get; set; }

        /// <summary>
        ///     Gets or sets the flow amoumt string.
        /// </summary>
        [Display(Name = "流向")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "60", SortOrder = 4)]
        public string FlowAmoumtStr { get; set; }

        /// <summary>
        ///     Gets or sets the flow.
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, DataSource = "Alabo.Core.Enums.Enum.AccountFlow",
            ListShow = false,
            Width = "120", SortOrder = 4)]
        public AccountFlow Flow { get; set; }

        /// <summary>
        ///     Gets or sets the name of the money 类型.
        /// </summary>
        [Display(Name = "账户名称")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Info, GroupTabId = 1, Width = "80",
            IsShowBaseSerach = true, IsShowAdvancedSerach = true, ListShow = true, SortOrder = 3)]
        public string MoneyTypeName { get; set; }

        /// <summary>
        ///     Gets or sets the money 类型 configuration.
        /// </summary>
        public MoneyTypeConfig MoneyTypeConfig { get; set; }

        /// <summary>
        ///     Gets or sets the 会员 grade.
        /// </summary>
        public UserGradeConfig UserGrade { get; set; }

        /// <summary>
        ///     Gets or sets the intro.
        /// </summary>
        [Display(Name = "交易详情")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "270", SortOrder = 9)]
        public string Intro { get; set; }

        /// <summary>
        ///     Gets or sets the other 会员 grade.
        /// </summary>
        public UserGradeConfig OtherUserGrade { get; set; }

        /// <summary>
        ///     下一条账单
        /// </summary>
        public Domain.Entities.Bill NextBill { get; set; }

        /// <summary>
        ///     上一条账单
        /// </summary>
        public Domain.Entities.Bill PrexBill { get; set; }

        /// <summary>
        ///     视图s the links.
        /// </summary>
        public IEnumerable ViewLinks() {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("财务详情", "/Admin/Bill/Edit?id=[[Id]]", Icons.Edit, LinkType.ColumnLink)
            };
            return quickLinks;
        }

        public PageResult<ViewAdminBill> PageTable(object query, AutoBaseModel autoModel) {
            var userInput = ToQuery<BillInput>();
            var model = new PagedList<ViewAdminBill>();
            if (autoModel.Filter == FilterType.Admin) {
                model = Resolve<IFinanceAdminService>().GetViewBillPageList(userInput);
            } else if (autoModel.Filter == FilterType.User || autoModel.Filter == FilterType.City) {
                userInput.UserId = autoModel.BasicUser.Id;
                model = Resolve<IFinanceAdminService>().GetViewBillPageList(userInput);
            } else {
                userInput.UserId = autoModel.BasicUser.Id;
                model = Resolve<IFinanceAdminService>().GetViewBillPageList(userInput);
                //throw new ValidException("非法请求");//去掉非法请求,默认请求自己下级的
            }
            if (autoModel.Filter == FilterType.User) {
                model.ForEach(r => { r.UserName = r.User?.GetUserName(); });
            }
            return ToPageResult(model);
        }

        public List<TableAction> Actions() {
            var list = new List<TableAction> {
                //ToLinkAction("财务明细", "Edit")
            };
            return list;
        }
    }
}