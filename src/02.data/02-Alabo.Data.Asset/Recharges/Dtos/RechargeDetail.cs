using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Asset.Recharges.Dtos
{
    [ClassProperty(Name = "充值", Icon = "fa fa-puzzle-piece", ListApi = "Api/RechargeApi/GetList",
        PageType = ViewPageType.List, PostApi = "Api/RechargeApi/GetList")]
    public class RechargeDetail
    {
        /// <summary>
        ///     id
        /// </summary>
        public long Id { get; set; }

        [Display(Name = "用户ID")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true, Width = "100",
            ListShow = true, SortOrder = 3)]
        public long UserId { get; set; }

        public string Serial { get; set; }

        [Display(Name = "数量")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true, Width = "100",
            ListShow = true, SortOrder = 3)]
        public decimal Account { get; set; }

        /// <summary>
        ///     手续费
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        ///     交易时间
        /// </summary>
        [Display(Name = "交易时间")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true, Width = "100",
            ListShow = true, SortOrder = 3)]
        public string DateTime { get; set; }

        public string moneyTypeName { get; set; }

        [Display(Name = "支付时间")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true, Width = "100",
            ListShow = true, SortOrder = 3)]
        public string PayTime { get; set; }

        [Display(Name = "充值类型")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true, Width = "100",
            ListShow = true, SortOrder = 3)]
        public string ChargeType { get; set; }

        /// <summary>
        ///     充值状态
        /// </summary>
        [Display(Name = "充值状态")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true, Width = "100",
            ListShow = true, SortOrder = 3)]
        public string Status { get; set; }

        public string Message { get; set; }

        public string Description { get; set; }
    }
}