using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Core.Finance.Domain.Entities.Extension;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Finance.ViewModels.WithDraw {

    /// <summary>
    ///     提现输出模型
    /// </summary>
    [ClassProperty(Name = "提现管理", Icon = "fa fa-puzzle-piece", Description = "提现管理")]
    public class ViewHomeWithDraw : BaseViewModel {

        /// <summary>
        ///     id
        /// </summary>
        [Field(ListShow = false)]
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
        //[Field(ControlsType = ControlsType.TextBox, ListShow = true, IsShowBaseSerach = true, Width = "80", Link = "/Admin/User/Edit?id=[[UserId]]", IsShowAdvancedSerach = true, SortOrder = 1)]
        public string UserName { get; set; }

        /// <summary>
        ///     支付密码
        /// </summary>
        [Display(Name = "支付密码")]
        [Field(ControlsType = ControlsType.Password, EditShow = true, ListShow = false, Width = "100", SortOrder = 4)]
        public string PayPassWord { get; set; }

        /// <summary>
        ///     银行名称
        /// </summary>
        [Display(Name = "银行名称")]
        [Field(ControlsType = ControlsType.DropdownList, DataSource = "Alabo.Core.Enums.Enum.BankType",
            LabelColor = LabelColor.Info, ListShow = false, EditShow = true, Width = "100", SortOrder = 4)]
        public BankType BankType { get; set; }

        /// <summary>
        ///     持卡人姓名
        /// </summary>
        [Display(Name = "持卡人姓名")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Info, ListShow = false, EditShow = true,
            Width = "100", SortOrder = 4)]
        public string BankName { get; set; }

        /// <summary>
        ///     银行卡号
        /// </summary>
        [Display(Name = "银行卡号")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Info, ListShow = false, EditShow = true,
            Width = "100", SortOrder = 4)]
        [StringLength(20, ErrorMessage = "银行卡号不能超过20位")]
        public string BankNumber { get; set; }

        /// <summary>
        ///     moneyTypeId
        /// </summary>
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     账户余额
        /// </summary>
        [Display(Name = "账户余额")]
        [Field(ControlsType = ControlsType.Label, EditShow = true, ListShow = false, Width = "100", SortOrder = 1)]
        public decimal Balance { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        [Display(Name = "备注")]
        [Field(ControlsType = ControlsType.TextArea, EditShow = true, ListShow = false, Width = "100", SortOrder = 999)]
        public string Remark { get; set; }

        /// <summary>
        ///     货币类型
        /// </summary>
        [Display(Name = "申请账户")]
        [Field(ControlsType = ControlsType.TextBox, EditShow = false, LabelColor = LabelColor.Info, ListShow = true,
            Width = "80", SortOrder = 2)]
        public string MoneyTypeName { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.DropdownList, IsTabSearch = true,
            DataSource = "Alabo.App.Core.Finance.Domain.Enums.TradeStatus", ListShow = true, EditShow = false,
            Width = "80", SortOrder = 7)]
        public TradeStatus Status { get; set; }

        /// <summary>
        ///     申请金额
        /// </summary>
        [Display(Name = "申请金额")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsMain = true, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            LabelColor = LabelColor.Warning, ListShow = true, GroupTabId = 1, Width = "80", SortOrder = 4)]
        [Range(1, 99999999, ErrorMessage = "提现额度必须大于等于0！")]
        public decimal Amount { get; set; }

        /// <summary>
        ///     交易类型
        /// </summary>
        [Display(Name = "交易类型")]
        [Field(ControlsType = ControlsType.DropdownList, EditShow = false,
            DataSource = "Alabo.App.Core.Finance.Domain.Enums.TradeType", ListShow = true, Width = "80",
            SortOrder = 6)]
        public TradeType Type { get; set; }

        /// <summary>
        ///     手续费
        /// </summary>
        [Display(Name = "手续费")]
        [Field(ControlsType = ControlsType.NumberRang, EditShow = false, TableDispalyStyle = TableDispalyStyle.Code,
            IsShowBaseSerach = true, IsShowAdvancedSerach = true, ListShow = true, GroupTabId = 1, Width = "80",
            SortOrder = 5)]
        public decimal Fee { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Display(Name = "申请时间")]
        [Field(ControlsType = ControlsType.DateTimeRang, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            EditShow = false, ListShow = true, Width = "120", SortOrder = 8)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     支付时间
        /// </summary>
        [Display(Name = "支付时间")]
        [Field(ControlsType = ControlsType.DateTimeRang, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            EditShow = false, ListShow = true, Width = "120", SortOrder = 9)]
        public DateTime PayTime { get; set; }

        /// <summary>
        ///     Gets or sets the with draw 扩展.
        /// </summary>
        public TradeExtension TradeExtension { get; set; } = new TradeExtension();
    }
}