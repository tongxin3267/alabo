using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Asset.Recharges.Domain.Enums;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Asset.Recharges.Dtos
{
    /// <summary>
    ///     Class ViewHomeRecharge.
    ///     会员中心充值记录
    /// </summary>
    [ClassProperty(Name = "充值管理", Icon = "fa fa-puzzle-piece")]
    public class ViewHomeRecharge : BaseViewModel
    {
        /// <summary>
        ///     id
        /// </summary>
        [Field(ListShow = false, EditShow = false)]
        public long Id { get; set; }

        /// <summary>
        ///     userid
        /// </summary>
        [Field(ListShow = false, EditShow = false)]
        public long UserId { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Display(Name = "用户名")]
        [NotMapped]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = false, Width = "110", SortOrder = 1)]
        public string UserName { get; set; }

        /// <summary>
        ///     moneyTypeId
        /// </summary>
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     货币类型
        /// </summary>
        [Display(Name = "申请账户")]
        [Field(ControlsType = ControlsType.Label, LabelColor = LabelColor.Info, ListShow = true, EditShow = true,
            Width = "100",
            SortOrder = 2)]
        public string MoneyTypeName { get; set; } = "现金账户";

        /// <summary>
        ///     银行名称
        /// </summary>
        [Display(Name = "银行名称")]
        [HelpBlock("如果为线下充值 请填写银行信息")]
        [Field(ControlsType = ControlsType.DropdownList, DataSource = "Alabo.Framework.Core.Enums.Enum.BankType",
            LabelColor = LabelColor.Info, ListShow = false, EditShow = true, Width = "100", SortOrder = 4)]
        public BankType BankType { get; set; }

        /// <summary>
        ///     持卡人姓名
        /// </summary>
        [Display(Name = "持卡人姓名")]
        [HelpBlock("如果为线下充值 请填写银行卡持卡人姓名")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Info, ListShow = false, EditShow = true,
            Width = "100", SortOrder = 4)]
        public string BankName { get; set; }

        /// <summary>
        ///     银行卡号
        /// </summary>
        [Display(Name = "银行卡号")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Info, ListShow = false, EditShow = true,
            Width = "100", SortOrder = 3)]
        [HelpBlock("如果为线下充值 请填写银行卡卡号")]
        [StringLength(20, ErrorMessage = "银行卡号不能超过20位")]
        public string BankNumber { get; set; }

        /// <summary>
        ///     申请金额
        /// </summary>
        [Display(Name = "充值金额")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsMain = true,
            LabelColor = LabelColor.Warning, ListShow = true, EditShow = true, GroupTabId = 1, Width = "120",
            SortOrder = 2)]
        [Range(1, 99999999, ErrorMessage = "金额必须大于等于0！")]
        public decimal Amount { get; set; }

        /// <summary>
        ///     交易类型
        /// </summary>
        [Display(Name = "交易类型")]
        [Field(ControlsType = ControlsType.DropdownList,
            DataSource = "Alabo.App.Core.Finance.Domain.Enums.RechargeType", ListShow = false, EditShow = false,
            Width = "80",
            SortOrder = 6)]
        public RechargeType Type { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Field(ControlsType = ControlsType.DateTimeRang, IsShowAdvancedSerach = true, IsShowBaseSerach = true,
            EditShow = false, ListShow = true, Width = "120", SortOrder = 8)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     支付时间
        /// </summary>
        [Display(Name = "充值时间")]
        [Field(ControlsType = ControlsType.DateTimeRang, EditShow = false, IsShowAdvancedSerach = true,
            IsShowBaseSerach = true, ListShow = true, Width = "120", SortOrder = 9)]
        public DateTime PayTime { get; set; }
    }
}