using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoTables;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.UI;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Finance.ViewModels.Account {

    [BsonIgnoreExtraElements]
    [ClassProperty(Name = "区块链钱包", Icon = "flaticon-route", SideBarType = SideBarType.FinanceBlockChainSideBar)]
    public class ViewBlockChain : UIBase, IAutoTable<ViewBlockChain> {

        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the 会员.
        /// </summary>
        [Display(Name = "会员")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true,
            IsShowAdvancedSerach = true, IsMain = true, GroupTabId = 1, Width = "100",
            Link = "/Admin/Account/Edit?Id=[[UserId]]", SortOrder = 2)]
        public string UserName { get; set; }

        /// <summary>
        ///     货币类型id
        /// </summary>
        [Display(Name = "货币类型")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     钱包类型
        /// </summary>
        [Display(Name = "钱包类型")]
        [Field(ListShow = true, GroupTabId = 1, Width = "80", SortOrder = 3, LabelColor = LabelColor.Danger)]
        public string MoneyTypeName { get; set; }

        /// <summary>
        ///     钱包地址
        /// </summary>
        [Display(Name = "钱包地址")]
        [Field(ListShow = true, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            GroupTabId = 1, Width = "400", SortOrder = 4, LabelColor = LabelColor.Info)]
        public string Token { get; set; }

        /// <summary>
        ///     货币量(余额)
        /// </summary>
        [Display(Name = "金额")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, TableDispalyStyle = TableDispalyStyle.Code,
            IsShowBaseSerach = true, IsShowAdvancedSerach = true, IsMain = true, GroupTabId = 1, Width = "80", SortOrder = 10)]
        public decimal Amount { get; set; }

        /// <summary>
        ///     冻结的货币量
        /// </summary>
        [Display(Name = "冻结金额")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, TableDispalyStyle = TableDispalyStyle.Code,
            IsMain = true, GroupTabId = 1, Width = "80", SortOrder = 11)]
        public decimal FreezeAmount { get; set; }

        /// <summary>
        ///     历史累计转入货币量(截至总收入)
        /// </summary>
        [Display(Name = "历史累计")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, TableDispalyStyle = TableDispalyStyle.Code,
            IsMain = true, GroupTabId = 1, Width = "80", SortOrder = 12)]
        public decimal HistoryAmount { get; set; }

        /// <summary>
        ///     交易用户
        /// </summary>
        public Users.Entities.User User { get; set; }

        /// <summary>
        ///     Gets or sets the money 类型 configuration.
        /// </summary>
        [Display(Name = "操作类型")]
        public MoneyTypeConfig MoneyTypeConfig { get; set; }

        public List<TableAction> Actions() {
            var list = new List<TableAction>
            {
                ToLinkAction("资产操作", "/Account/BlockChain")
            };
            return list;
        }

        public PageResult<ViewBlockChain> PageTable(object query, AutoBaseModel autoModel) {
            var model = Resolve<IAccountService>().GetBlockChainList(query);
            var result = ToPageResult(model);
            return result;
        }

        public IEnumerable ViewLinks() {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("钱包操作", "/Admin/Account/Edit?Id=[[UserId]]", Icons.Edit, LinkType.ColumnLink)
            };
            return quickLinks;
        }
    }
}