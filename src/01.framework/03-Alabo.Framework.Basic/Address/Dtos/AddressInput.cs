using System.ComponentModel.DataAnnotations;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Regexs;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.User.Domain.Dtos {

    /// <summary>
    /// 地址输入
    /// </summary>
    public class AddressInput : EntityDto {

        /// <summary>
        /// 地址ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     收货人名称
        /// </summary>
        [Display(Name = "姓名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, Width = "100", ListShow = true,
            SortOrder = 2)]
        public string Name { get; set; }

        /// <summary>
        ///     用户Id
        /// </summary>
        [Display(Name = "用户Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long UserId { get; set; }

        /// <summary>
        ///     区域Id
        /// </summary>
        [Display(Name = "区域Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long RegionId { get; set; }

        /// <summary>
        ///     是否默认地址
        /// </summary>
        [Display(Name = "是否默认")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = false, EditShow = true, SortOrder = 7)]
        public bool IsDefault { get; set; } = false;

        /// <summary>
        ///     地址方式
        /// </summary>
        [Display(Name = "地址类型")]
        [Field(EditShow = false, Width = "100", ListShow = true, SortOrder = 5)]
        public AddressLockType Type { get; set; } = AddressLockType.OrderAddress;

        /// <summary>
        ///     详细街道地址，不需要重复填写省/市/区
        /// </summary>
        [Display(Name = "详细地址")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(30, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, Width = "150", ListShow = false,
            SortOrder = 3)]
        public string Address { get; set; }

        /// <summary>
        ///     手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        [RegularExpression(RegularExpressionHelper.ChinaMobile, ErrorMessage = ErrorMessage.NotMatchFormat)]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, IsShowAdvancedSerach = true, Width = "90",
            ListShow = true, SortOrder = 7)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Mobile { get; set; }
    }

    /// <summary>
    /// 用户备案地址修改
    /// </summary>
    public class UserInfoAddressInput : EntityDto {

        /// <summary>
        /// 地址ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     用户Id
        /// </summary>
        [Display(Name = "用户Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long UserId { get; set; }

        /// <summary>
        ///     区域Id
        /// </summary>
        [Display(Name = "区域Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long RegionId { get; set; }

        /// <summary>
        ///     详细街道地址，不需要重复填写省/市/区
        /// </summary>
        [Display(Name = "详细地址")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(40, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, Width = "150", ListShow = false,
            SortOrder = 3)]
        public string Address { get; set; }

        /// <summary>
        /// 地址方式
        /// </summary>
        public AddressLockType Type { get; set; } = AddressLockType.OrderAddress;
    }
}