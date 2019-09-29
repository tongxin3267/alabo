using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Regexs;
using Alabo.UI;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Framework.Basic.Address.Domain.Entities
{
    /// <summary>
    ///     用户地址
    /// </summary>
    [ClassProperty(Name = "用户地址", PageType = ViewPageType.List, Icon = IconFontawesome.address_book,
        SideBarType = SideBarType.UserAddressSideBar)]
    [BsonIgnoreExtraElements]
    [AutoDelete(IsAuto = true)]
    [Table("Address_UserAddress")]
    public class UserAddress : AggregateMongodbUserRoot<UserAddress>
    {
        /// <summary>
        ///     收货人名称
        /// </summary>
        [Display(Name = "姓名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请输入姓名")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, Width = "100", ListShow = true,
            SortOrder = 2)]
        public string Name { get; set; }

        /// <summary>
        ///     区域Id
        /// </summary>
        [Display(Name = "区域Id")]
        [Field(ControlsType = ControlsType.CityDropList, ListShow = false, EditShow = true, SortOrder = 7)]
        [HelpBlock("请输入您的所在区域")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long RegionId { get; set; }

        /// <summary>
        ///     是否默认地址
        /// </summary>
        [Display(Name = "是否默认")]
        [Field(ControlsType = ControlsType.Switch, ListShow = false, EditShow = true, SortOrder = 7)]
        public bool IsDefault { get; set; } = false;

        /// <summary>
        ///     地址方式
        /// </summary>
        [Display(Name = "地址类型")]
        [Field(EditShow = false, Width = "100", ListShow = true, SortOrder = 5)]
        [HelpBlock("请选择您的地址类型")]
        public AddressLockType Type { get; set; }

        /// <summary>
        ///     详细街道地址，不需要重复填写省/市/区
        /// </summary>
        [Display(Name = "详细地址")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(40, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Field(ControlsType = ControlsType.TextArea, IsShowAdvancedSerach = true, Width = "150", ListShow = false,
            SortOrder = 3)]
        [HelpBlock("请输入您的街道地址信息")]
        public string Address { get; set; }

        /// <summary>
        ///     邮政编码
        /// </summary>
        [Display(Name = "邮政编码")]
        [RegularExpression(@"[0-9]{6}$", ErrorMessage = ErrorMessage.NotMatchFormat)]
        [Field(ControlsType = ControlsType.Numberic, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            Width = "100",
            ListShow = true, SortOrder = 4)]
        [HelpBlock("请输入所在地邮政编码")]
        public string ZipCode { get; set; }

        /// <summary>
        ///     手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        [RegularExpression(RegularExpressionHelper.ChinaMobile, ErrorMessage = ErrorMessage.NotMatchFormat)]
        [Field(ControlsType = ControlsType.Numberic, IsShowBaseSerach = true, IsShowAdvancedSerach = true, Width = "90",
            ListShow = true, SortOrder = 7)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请输入您的手机号码")]
        public string Mobile { get; set; }

        /// <summary>
        ///     详细地址描述，比如广东东莞南城太沙路121号
        /// </summary>
        [Display(Name = "详细地址")]
        [Field(ControlsType = ControlsType.TextArea, TableDispalyStyle = TableDispalyStyle.Code, Width = "250",
            EditShow = false,
            ListShow = true, SortOrder = 10)]
        [HelpBlock("请输入具体地址信息，如：广东东莞南城太沙路121号")]
        public string AddressDescription { get; set; }

        /// <summary>
        ///     省份编码
        /// </summary>
        public long Province { get; set; }

        /// <summary>
        ///     城市编码
        /// </summary>
        public long City { get; set; }
    }
}