using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.Rewards.Dtos
{
    /// <summary>
    ///     Class RewardInput.
    /// </summary>
    /// <seealso cref="Alabo.Domains.Query.Dto.PagedInputDto" />
    public class RewardInput : PagedInputDto
    {
        /// <summary>
        ///     序号
        /// </summary>
        /// <value>The serial.</value>
        [Display(Name = "序号")]
        [Field(ControlsType = ControlsType.TextBox, PlaceHolder = "输入交易精确查询", IsShowAdvancedSerach = true,
            IsShowBaseSerach = true, SortOrder = 100)]
        public string Serial { get; set; }

        /// <summary>
        ///     收益用户
        /// </summary>
        /// <value>The name of the share user.</value>
        [Field(ControlsType = ControlsType.TextBox, PlaceHolder = "输入分润、收益用户名精确查询", IsShowAdvancedSerach = true,
            IsShowBaseSerach = true, SortOrder = 1)]
        [Display(Name = "收益用户")]
        public string ShareUserName { get; set; }

        /// <summary>
        ///     分润用户ID
        /// </summary>
        /// <value>The user identifier.</value>
        public long UserId { get; set; }

        /// <summary>
        ///     交易用户
        /// </summary>
        /// <value>The name of the user.</value>
        [Field(ControlsType = ControlsType.TextBox, PlaceHolder = "输入触发、订单用户名精确查询", IsShowAdvancedSerach = true,
            IsShowBaseSerach = true, SortOrder = 2)]
        [Display(Name = "序号")]
        public string UserName { get; set; }

        /// <summary>
        ///     分润订单Id
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        ///     Gets or sets the money type identifier.
        /// </summary>
        /// <value>The money type identifier.</value>
        [Field(ControlsType = ControlsType.DropdownList, IsShowAdvancedSerach = true,
            DataSource = "Alabo.App.Core.Finance.Domain.CallBacks.MoneyTypeConfig", IsShowBaseSerach = false,
            SortOrder = 200)]
        [Display(Name = "货币类型")]
        public Guid? MoneyTypeId { get; set; }

        /// <summary>
        ///     Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public Guid? ModuleId { get; set; }

        /// <summary>
        ///     Gets or sets the module configuration identifier.
        /// </summary>
        /// <value>The module configuration identifier.</value>
        public long ModuleConfigId { get; set; }

        /// <summary>
        ///     开始金额
        /// </summary>
        [Field(ControlsType = ControlsType.NumberRang, IsShowAdvancedSerach = true, IsShowBaseSerach = false,
            SortOrder = 200)]
        public decimal? Amount { get; set; }

        /// <summary>
        ///     开始时间
        /// </summary>
        [Field(ControlsType = ControlsType.DateTimeRang, IsShowAdvancedSerach = true, IsShowBaseSerach = false,
            SortOrder = 200)]
        [Display(Name = "开始时间")]
        public DateTime? CreateTime { get; set; } = null;
    }
}