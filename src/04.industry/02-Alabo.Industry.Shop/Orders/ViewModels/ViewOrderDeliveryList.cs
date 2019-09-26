using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Users.Entities;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.Industry.Shop.Orders.ViewModels {

    /// <summary>
    ///     发货记录表
    /// </summary>
    [ClassProperty(Name = "发货记录", Icon = "fa fa-puzzle-piece", Description = "发货记录",PageType = ViewPageType.List,ListApi = "Api/OrderDelivery/OrderDeliveryList",
        SideBarType = SideBarType.OrderSideBar, PostApi = "Api/OrderDelivery/OrderDeliveryList")]
    public class ViewOrderDeliveryList : BaseViewModel {

        /// <summary>
        ///     序号    ///
        /// </summary>
        [Field(SupportSearchOrder = true, EditShow = false, ListShow = false)]
        public long Id { get; set; }

        /// <summary>
        ///     Gets or sets the user.
        /// </summary>
        /// <value>
        ///     The user.
        /// </value>
        public User User { get; set; }

        /// <summary>
        ///     下单用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     下单用户
        /// </summary>
        [Display(Name = "用户名")]
        [Field(IsShowBaseSerach = true, ListShow = true, IsMain = true, Width = "80",
            ControlsType = ControlsType.TextBox, PlaceHolder = "请输入用户名", DataField = "UserId",
            Link = "/Admin/User/Edit?id=[[UserId]]", SortOrder = 2)]
        public string UserName { get; set; }

        /// <summary>
        ///     所属店铺
        /// </summary>
        public long StoreId { get; set; }

        /// <summary>
        ///     店铺名称
        /// </summary>
        [Display(Name = "店铺名称")]
        [Field(IsShowBaseSerach = true, LabelColor = LabelColor.Success, ListShow = true, Width = "100",
            EditShow = true,
            ControlsType = ControlsType.TextBox, PlaceHolder = "请输入店铺名称", DataField = "StoreId")]
        public string StoreName { get; set; }

        /// <summary>
        ///     订单总金额
        ///     订单总金额=商品总金额-优惠金额-（+）调整金额+税费金额+邮费金额 -其他账户支出
        /// </summary>
        [Display(Name = "订单总金额")]
        [Field(IsShowAdvancedSerach = true, Width = "90", LabelColor = LabelColor.Brand, ListShow = true,
            EditShow = true,
            ControlsType = ControlsType.Numberic, SupportSearchOrder = true)]
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     根据Id自动生成12位序列号
        /// </summary>
        [Display(Name = "订单号")]
        [Field(IsShowBaseSerach = true, Width = "100", LabelColor = LabelColor.Info, ListShow = true, EditShow = true,
            ControlsType = ControlsType.TextBox, PlaceHolder = "请输入订单号", DataField = "Id", SortOrder = 1)]
        public string Serial { get; set; }

        /// <summary>
        ///     Gets or sets the order identifier.
        /// </summary>
        /// <value>
        ///     The order identifier.
        /// </value>
        public long OrderId { get; set; }

        /// <summary>
        ///     快递公司guid
        /// </summary>
        [Display(Name = "快递公司")]
        [Field(IsShowBaseSerach = true, DataSource = "ExpressConfig", ListShow = false, EditShow = true,
            ControlsType = ControlsType.DropdownList, PlaceHolder = "请输入订单号", DataField = "Id", SortOrder = 1)]
        public Guid ExpressGuid { get; set; }

        /// <summary>
        ///     快递公司guid
        /// </summary>
        /// <value>
        ///     The name of the express.
        /// </value>
        [Display(Name = "快递公司")]
        [Field(IsShowBaseSerach = false, ListShow = true, EditShow = true, SortOrder = 1)]
        public string ExpressName { get; set; }

        /// <summary>
        ///     Gets or sets the product.
        /// </summary>
        /// <value>
        ///     The product.
        /// </value>
        public string ProductName { get; set; }

        /// <summary>
        ///     Gets or sets the remark.
        /// </summary>
        /// <value>
        ///     The remark.
        /// </value>
        public string Remark { get; set; }

        /// <summary>
        ///     Gets or sets the address.
        /// </summary>
        /// <value>
        ///     The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        ///     物流单号
        /// </summary>
        [Display(Name = "快递单号")]
        [Field(IsShowBaseSerach = true, ListShow = true, SortOrder = 1, ControlsType = ControlsType.TextBox,
            EditShow = true)]
        public string ExpressNumber { get; set; }

        /// <summary>
        ///     总共发货数量
        /// </summary>
        [Display(Name = "发货总数")]
        [Field(IsShowAdvancedSerach = true, ListShow = true, ControlsType = ControlsType.Numberic, EditShow = true,
            SupportSearchOrder = true)]
        public long TotalCount { get; set; }

        /// <summary>
        ///     发货时间
        /// </summary>
        [Display(Name = "发货时间")]
        [Field(IsShowAdvancedSerach = true, ListShow = true, ControlsType = ControlsType.DateTimeRang, EditShow = true,
            SupportSearchOrder = true)]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     Views the links.
        /// </summary>

        public IEnumerable<ViewLink> ViewLinks() {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("详情", "/Admin/Order/DeliveryEdit?id=[[Id]]", Icons.List, LinkType.ColumnLink)
            };
            return quickLinks;
        }
    }
}