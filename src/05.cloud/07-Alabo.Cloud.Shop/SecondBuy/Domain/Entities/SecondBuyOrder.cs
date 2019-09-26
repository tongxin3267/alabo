using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Market.SecondBuy.Domain.Enums;
using Alabo.Core.WebUis;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Market.SecondBuy.Domain.Entities {

    /// <summary>
    /// 订单管理
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("SecondBuy_SecondBuyOrder")]
    [ClassProperty(Name = "订单管理", Description = "订单管理", Icon = IconFlaticon.route,
        SideBarType = SideBarType.SchoolSideBar)]
    public class SecondBuyOrder : AggregateMongodbRoot<SecondBuyOrder> {

        /// <summary>
        ///     姓名
        /// </summary>
        [Display(Name = "姓名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsMain = true, ListShow = true, IsShowBaseSerach = true, EditShow = true, SortOrder = 0)]
        public string Name { get; set; }

        /// <summary>
        ///     手机号
        /// </summary>
        [Display(Name = "手机号")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsMain = true, ListShow = true, IsShowBaseSerach = true, EditShow = true, SortOrder = 0)]
        public string Mobile { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [Display(Name = "详细地址")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Address { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        [Display(Name = "商品")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long ProductId { get; set; }

        /// <summary>
        /// SkuId
        /// </summary>
        [Display(Name = "商品Sku")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long ProductSkuId { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        [Display(Name = "购买数量")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long BuyCount { get; set; }

        /// <summary>
        /// 留言
        /// </summary>
        [Display(Name = "留言")]
        public string Message { get; set; }

        /// <summary>
        /// 区域ID
        /// </summary>
        [Display(Name = "区域ID")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long RegionId { get; set; }

        /// <summary>
        /// 订单总价
        /// </summary>
        [Display(Name = "订单总价")]
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// 区域全名称
        /// </summary>
        [Display(Name = "详细地址")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.Label, ListShow = true, IsShowAdvancedSerach = true, EditShow = false, SortOrder = 0)]
        public string RegionFullName { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [Display(Name = "商品名称")]
        [Field(ControlsType = ControlsType.Label, ListShow = true, IsShowAdvancedSerach = true, EditShow = false, SortOrder = 0)]
        public string ProductName { get; set; }

        [Display(Name = "商品")]
        [Field(ControlsType = ControlsType.RadioButton, ListShow = true, IsTabSearch = true, EditShow = true, SortOrder = 0)]
        public SecondBuyOrderStatus Status { get; set; }

        [Display(Name = "管理员备注")]
        [Field(ControlsType = ControlsType.RadioButton, ListShow = true, IsTabSearch = true, EditShow = true, SortOrder = 0)]
        public string Remark { get; set; }
    }
}