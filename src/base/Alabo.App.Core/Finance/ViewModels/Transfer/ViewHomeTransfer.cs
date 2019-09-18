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

namespace Alabo.App.Core.Finance.ViewModels.Transfer {

    /// <summary>
    ///     Class ViewHomeRecharge.
    ///     会员中心充值记录
    /// </summary>
    [ClassProperty(Name = "转账明细", Icon = "fa fa-puzzle-piece")]
    public class ViewHomeTransfer : BaseViewModel {

        /// <summary>
        ///     id
        /// </summary>
        [Field(EditShow = false)]
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
        // [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsShowBaseSerach = true, Width = "140", Link = "/Admin/User/Edit?id=[[UserId]]", IsShowAdvancedSerach = true, SortOrder = 1)]
        public string UserName { get; set; }

        /// <summary>
        ///     moneyTypeId
        /// </summary>
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     对方用户名
        /// </summary>
        [Display(Name = "对方用户名")]
        [Field(ControlsType = ControlsType.TextBox, EditShow = true, ListShow = false, Width = "100", SortOrder = 2)]
        public string OtherUserName { get; set; }

        /// <summary>
        ///     对方用户名Id
        /// </summary>
        public long OtherUserId { get; set; }

        /// <summary>
        ///     货币类型
        /// </summary>
        [Display(Name = "申请账户")]
        [Field(ControlsType = ControlsType.TextBox,
            LabelColor = LabelColor.Info, EditShow = false, ListShow = true, Width = "100", SortOrder = 2)]
        public string MoneyTypeName { get; set; }

        /// <summary>
        ///     账户余额
        /// </summary>
        [Display(Name = "账户余额")]
        [Field(ControlsType = ControlsType.Label, EditShow = true, ListShow = false, Width = "100", SortOrder = 4)]
        public decimal Balance { get; set; }

        /// <summary>
        ///     申请金额
        /// </summary>
        [Display(Name = "转账金额")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsMain = true,
            LabelColor = LabelColor.Warning, ListShow = true, EditShow = true, GroupTabId = 1, Width = "120",
            SortOrder = 4)]
        [Range(1, 99999999, ErrorMessage = "金额必须大于等于0！")]
        public decimal Amount { get; set; }

        /// <summary>
        ///     支付密码
        /// </summary>
        [Display(Name = "支付密码")]
        [Field(ControlsType = ControlsType.Password, EditShow = true, ListShow = false, Width = "100", SortOrder = 4)]
        public string PayPassWord { get; set; }

        /// <summary>
        ///     申请金额
        /// </summary>
        [Display(Name = "手续费")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            TableDispalyStyle = TableDispalyStyle.Code, ListShow = true, EditShow = false, GroupTabId = 1,
            Width = "100", SortOrder = 4)]
        public decimal Fee { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "转账状态")]
        [Field(ControlsType = ControlsType.DropdownList,
            DataSource = "Alabo.App.Core.Finance.Domain.Enums.TradeStatus", ListShow = true, EditShow = false,
            Width = "80", SortOrder = 7)]
        public TradeStatus Status { get; set; } = TradeStatus.Pending;

        /// <summary>
        ///     备注
        /// </summary>
        [Display(Name = "备注")]
        [Field(ControlsType = ControlsType.TextArea, EditShow = true, ListShow = false, Width = "100", SortOrder = 999)]
        public string Remark { get; set; }

        /// <summary>
        ///     交易类型
        /// </summary>
        [Display(Name = "交易类型")]
        [Field(ControlsType = ControlsType.DropdownList,
            DataSource = "Alabo.App.Core.Finance.Domain.Enums.RechargeType", ListShow = true, EditShow = false,
            Width = "80", SortOrder = 6)]
        public RechargeType Type { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Field(ControlsType = ControlsType.DateTimeRang,
            EditShow = false, ListShow = true, Width = "120", SortOrder = 8)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     支付时间
        /// </summary>
        [Display(Name = "支付时间")]
        [Field(ControlsType = ControlsType.DateTimeRang,
            EditShow = false, ListShow = true, Width = "120", SortOrder = 9)]
        public DateTime PayTime { get; set; }

        /// <summary>
        ///     Gets or sets the with draw 扩展.
        /// </summary>
        public TradeExtension TradeExtension { get; set; } = new TradeExtension();

        #region 银行卡信息

        /// <summary>
        ///     银行名称
        /// </summary>
        [Display(Name = "银行名称")]
        [Field(ControlsType = ControlsType.DropdownList, DataSource = "Alabo.Core.Enums.Enum.BankType",
            ListShow = false,
            EditShow = true, Width = "80", SortOrder = 6)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public BankType BankType { get; set; }

        /// <summary>
        ///     银行名称
        /// </summary>
        public string BankTypeName { get; set; }

        /// <summary>
        ///     持卡人姓名
        /// </summary>
        [Display(Name = "持卡人姓名")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = false, EditShow = true, Width = "80", SortOrder = 9)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string BankName { get; set; }

        /// <summary>
        ///     银行卡号
        /// </summary>
        [Display(Name = "银行卡号")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = false, EditShow = true, Width = "80", SortOrder = 8)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(20, ErrorMessage = "银行卡号不能超过20位")]
        public string BankNumber { get; set; }

        #endregion 银行卡信息
    }
}