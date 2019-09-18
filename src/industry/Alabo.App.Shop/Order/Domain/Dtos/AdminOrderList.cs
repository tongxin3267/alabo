using System;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Order.Domain.Dtos {

    /// <summary>
    /// </summary>
    [ClassProperty(Name = "订单管理", Icon = "fa fa-puzzle-piece", Description = "订单管理",
        PageType = ViewPageType.List)]
    public class AdminOrderList {

        /// <summary>
        ///     Id
        /// </summary>
        [Display(Name = "序号")]
        [Field(SupportSearchOrder = true)]
        public long Id { get; set; }

        /// <summary>
        ///     下单用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Display(Name = "用户名")]
        [Field(IsShowBaseSerach = true, ControlsType = ControlsType.TextBox, PlaceHolder = "请输入用户名", ListShow = true,
            DataField = "UserId", SortOrder = 2)]
        public string UserName { get; set; }

        /// <summary>
        ///     所属店铺
        /// </summary>
        public long StoreId { get; set; }

        /// <summary>
        ///     店铺名称
        /// </summary>
        [Display(Name = "店铺名称")]
        [Field(ControlsType = ControlsType.TextBox, PlaceHolder = "请输入店铺名称", DataField = "StoreName", ListShow = true)]
        public string StoreName { get; set; }

        /// <summary>
        ///     订单交易状态,OrderStatus等待付款WaitingBuyerPay = 0,等待发货WaitingSellerSendGoods = 1,已发货WaitingBuyerConfirm = 2,交易成功Success =
        ///     3,已取消Cancelled = 4,已作废Invalid = 5,
        /// </summary>
        [Display(Name = "订单状态")]
        [Field(IsTabSearch = true, IsShowAdvancedSerach = true, ControlsType = ControlsType.DropdownList, ListShow = true,
            SupportSearchOrder = true, DataSource = "Alabo.App.Shop.Order.Domain.Enums.OrderStatus")]
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        ///     订单类型
        /// </summary>
        [Display(Name = "订单类型")]
        [Field(IsShowAdvancedSerach = true, ControlsType = ControlsType.DropdownList, ListShow = true,
            DataSource = "Alabo.App.Shop.Order.Domain.Enums.OrderType")]
        public OrderType OrderType { get; set; }

        /// <summary>
        ///     订单总金额
        ///     订单总金额=商品总金额-优惠金额-（+）调整金额+税费金额+邮费金额 -其他账户支出
        /// </summary>
        [Display(Name = "订单总金额")]
        [Field(IsShowAdvancedSerach = true, ControlsType = ControlsType.NumberRang, SupportSearchOrder = true, ListShow = true)]
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     订单总数量
        /// </summary>
        [Display(Name = "订单总数量")]
        [Field(IsShowAdvancedSerach = true, ControlsType = ControlsType.NumberRang, SupportSearchOrder = true, ListShow = true)]
        public long TotalCount { get; set; }

        /// <summary>
        ///     订单实际支付的金额
        ///     订单实际支付的金额=商品总金额-优惠金额-（+）调整金额+邮费金额 -其他账户支出
        /// </summary>
        [Display(Name = "总支付现金")]
        [Field(IsShowAdvancedSerach = true, ControlsType = ControlsType.NumberRang, SupportSearchOrder = true, ListShow = true)]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        ///     下单时间，订单创建时间
        /// </summary>
        [Display(Name = "下单时间")]
        [Field(IsShowAdvancedSerach = true, ControlsType = ControlsType.DateTimeRang, SupportSearchOrder = true, ListShow = true)]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     根据Id自动生成12位序列号
        /// </summary>
        [Display(Name = "序号")]
        [Field(IsShowBaseSerach = true, ControlsType = ControlsType.TextBox, PlaceHolder = "请输入订单号", DataField = "Id", ListShow = true,
            SortOrder = 1)]
        public string Serial { get; set; }

        /// <summary>
        ///     订单详情
        /// </summary>
        public Entities.Order Order { get; set; }

        /// <summary>
        ///     下单用户
        /// </summary>
        [Display(Name = "下单用户")]
        [Field(IsShowBaseSerach = true, ControlsType = ControlsType.TextBox, DataField = "Id", ListShow = true,
            SortOrder = 3)]
        public User User { get; set; }

        /// <summary>
        ///     所属商城
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, PlaceHolder = "请输入订单号", DataField = "PriceStyleName", ListShow = true,
            SortOrder = 1)]
        public string PriceStyleName { get; set; }

        /// <summary>
        ///     抵现字符串
        /// </summary>
        public string ForCash { get; set; }
    }

    public class OrderExcelOutPut {

        /// <summary>
        ///     Id
        /// </summary>
        [Field(SupportSearchOrder = true)]
        public long Id { get; set; }

        /// <summary>
        ///     根据Id自动生成12位序列号
        /// </summary>
        [Display(Name = "订单编号")]
        [Field(IsShowBaseSerach = true, ListShow = true, ControlsType = ControlsType.TextBox, PlaceHolder = "请输入订单号", DataField = "Id",
            SortOrder = 1)]
        public string Serial { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Display(Name = "用户名")]
        [Field(IsShowBaseSerach = true, ListShow = true, ControlsType = ControlsType.TextBox, PlaceHolder = "请输入用户名",
            DataField = "UserId", SortOrder = 2)]
        public string UserName { get; set; }

        /// <summary>
        ///手机号
        /// </summary>
        [Display(Name = "手机号")]
        [Field(ListShow = true, ControlsType = ControlsType.TextBox, SortOrder = 3)]
        public string Mobile { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        [Display(Name = "收货地址")]
        [Field(ListShow = true, ControlsType = ControlsType.TextBox, SortOrder = 4)]
        public string Address { get; set; }

        /// <summary>
        ///     订单总金额
        ///     订单总金额=商品总金额-优惠金额-（+）调整金额+税费金额+邮费金额 -其他账户支出
        /// </summary>
        [Display(Name = "订单总金额")]
        [Field(ListShow = true, ControlsType = ControlsType.NumberRang, SortOrder = 60)]
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     订单总数量
        /// </summary>
        [Display(Name = "订单总数量")]
        [Field(ListShow = true, ControlsType = ControlsType.NumberRang, SortOrder = 50)]
        public long TotalCount { get; set; }

        /// <summary>
        ///     订单实际支付的金额
        ///     订单实际支付的金额=商品总金额-优惠金额-（+）调整金额+邮费金额 -其他账户支出
        /// </summary>
        [Display(Name = "总支付现金")]
        [Field(ListShow = true, ControlsType = ControlsType.NumberRang, SortOrder = 70)]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        ///     订单状态
        /// </summary>
        [Field(DataSource = "Alabo.App.Shop.Order.Domain.Enums.OrderStatus")]
        public OrderStatus Status { get; set; }

        /// <summary>
        ///     订单状态
        /// </summary>
        [Display(Name = "订单状态")]
        [Field(ListShow = true, SortOrder = 99)]
        public string OrderStatusName { get; set; }

        /// <summary>
        ///     留言
        /// </summary>
        [Display(Name = "留言")]
        [Field(ListShow = true, SortOrder = 100)]
        public string Message { get; set; }

        /// <summary>
        ///     下单时间，订单创建时间
        /// </summary>
        [Display(Name = "下单时间")]
        [Field(ListShow = true, ControlsType = ControlsType.DateTimeRang, SupportSearchOrder = true, SortOrder = 1000)]
        public DateTime CreateTime { get; set; }

        public Entities.Order Order { get; set; }
    }
}