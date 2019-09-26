using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Asset.Recharges.Dtos
{
    /// <summary>
    ///     Class RechargeApiInput.
    /// </summary>
    public class RechargeApiInput : PagedInputDto
    {
        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        public long? Id { get; set; } = null;

        /// <summary>
        ///     序号
        /// </summary>
        [Display(Name = "序号")]
        [Field(ControlsType = ControlsType.TextBox, PlaceHolder = "输入交易精确查询",
            IsShowAdvancedSerach = true, IsShowBaseSerach = true, SortOrder = 100)]
        public string Serial { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, PlaceHolder = "输入用户名精确查询",
            IsShowAdvancedSerach = true, IsShowBaseSerach = true, SortOrder = 1)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        ///     对方用户名
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, PlaceHolder = "输入对方用户名精确查询",
            IsShowAdvancedSerach = true, IsShowBaseSerach = true, SortOrder = 1)]
        [Display(Name = "对方用户名")]
        public string OtherUserName { get; set; }

        /// <summary>
        ///     Gets or sets 会员Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        ///     Gets or sets the other 会员 identifier.
        /// </summary>
        public long? OtherUserId { get; set; }

        /// <summary>
        ///     货币类型
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, IsShowAdvancedSerach = true,
            DataSource = "Alabo.App.Core.Finance.Domain.CallBacks.MoneyTypeConfig", IsShowBaseSerach = false,
            SortOrder = 200)]
        [Display(Name = "货币类型")]
        public Guid? MoneyTypeId { get; set; }

        /// <summary>
        ///     开始金额
        /// </summary>
        [Field(ControlsType = ControlsType.NumberRang, IsShowAdvancedSerach = true, IsShowBaseSerach = false,
            SortOrder = 200)]
        [Display(Name = "开始金额")]
        public decimal? Amount { get; set; }

        /// <summary>
        ///     开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        [Field(ControlsType = ControlsType.DateTimeRang, IsShowAdvancedSerach = true,
            IsShowBaseSerach = false, SortOrder = 200)]
        public DateTime? CreateTime { get; set; } = null;
    }
}