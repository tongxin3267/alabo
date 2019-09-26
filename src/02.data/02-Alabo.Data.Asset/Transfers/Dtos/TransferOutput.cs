using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Asset.Withdraws.Domain.Enums;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Asset.Transfers.Dtos {

    /// <summary>
    ///     转账管理
    /// </summary>
    [ClassProperty(Name = "转账管理", Icon = "fa fa-puzzle-piece", Description = "转账管理",
        SideBarType = SideBarType.TransferSideBar)]
    public class TransferOutput : EntityDto {

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
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Info, ListShow = true, Width = "80",
            SortOrder = 2)]
        public string MoneyTypeName { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.DropdownList, IsTabSearch = true,
            DataSource = "Alabo.App.Core.Finance.Domain.Enums.TradeStatus", ListShow = true, Width = "80",
            SortOrder = 7)]
        public WithdrawStatus Status { get; set; }

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
        /// </summary>
        [NotMapped]
        [Field(ListShow = false, EditShow = false, Width = "80",
            DataSource = "Alabo.App.Core.Finance.Domain.CallBacks.TransferConfig")]
        public Guid TransferConfigId { get; set; }

        /// <summary>
        ///     手续费
        /// </summary>
        [Display(Name = "手续费")]
        [Field(ControlsType = ControlsType.NumberRang, TableDispalyStyle = TableDispalyStyle.Code,
            IsShowBaseSerach = true, IsShowAdvancedSerach = true, ListShow = true, GroupTabId = 1, Width = "80",
            SortOrder = 5)]
        public decimal ServiceFee { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Display(Name = "交易时间")]
        [Field(ControlsType = ControlsType.DateTimeRang, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            ListShow = true, Width = "100", SortOrder = 8)]
        public DateTime CreateTime { get; set; }

        ///// <summary>
        /////     支付时间
        ///// </summary>
        //[Display(Name = "支付时间")]
        //[Field(ControlsType = ControlsType.DateTimeRang, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
        //    ListShow = true, Width = "100", SortOrder = 9)]
        //public DateTime PayTime { get; set; }

        /// <summary>
        ///     视图s the links.
        /// </summary>
        public IEnumerable<ViewLink> ViewLinks() {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("编辑", "/Admin/Transfer/Edit?Id=[[Id]]", Icons.Edit, LinkType.ColumnLink)
            };
            return quickLinks;
        }
    }
}