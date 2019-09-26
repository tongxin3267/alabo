using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Asset.Bills.Dtos
{
    public class BillInput : PagedInputDto
    {
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
        ///     操作类型
        /// </summary>
        //[Field(ControlsType = ControlsType.DropdownList, IsShowAdvancedSerach = true,
        //    DataSource = "Alabo.Framework.Core.Enums.Enum.BillActionType", IsShowBaseSerach = false, SortOrder = 200)]
        //[Display(Name = "货币类型")]
        //public BillActionType Type { get; set; }

        public long? UserId { get; set; }

        public long? OtherUserId { get; set; }

        /// <summary>
        ///     货币类型
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, IsShowAdvancedSerach = true,
            DataSource = "Alabo.App.Core.Finance.Domain.CallBacks.MoneyTypeConfig", IsShowBaseSerach = false,
            SortOrder = 200)]
        [Display(Name = "货币类型")]
        public Guid? MoneyTypeId { get; set; }

        [Field(ControlsType = ControlsType.DropdownList, IsShowAdvancedSerach = true,
            DataSource = "Alabo.Framework.Core.Enums.Enum.AccountFlow", IsShowBaseSerach = false, SortOrder = 200)]
        [Display(Name = "货币类型")]
        public AccountFlow? Flow { get; set; }

        /// <summary>
        ///     开始金额
        /// </summary>
        [Field(ControlsType = ControlsType.NumberRang, IsShowAdvancedSerach = true, IsShowBaseSerach = false,
            SortOrder = 200)]
        public decimal? Amount { get; set; }

        /// <summary>
        ///     开始时间
        /// </summary>
        [Field(ControlsType = ControlsType.DateTimeRang, IsShowAdvancedSerach = true,
            IsShowBaseSerach = false, SortOrder = 200)]
        [Display(Name = "开始时间")]
        public DateTime? CreateTime { get; set; } = null;
    }
}