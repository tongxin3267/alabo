using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Industry.Shop.Orders.Domain.Enums;
using Alabo.UI.Enum;
using Alabo.Users.Entities;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.Industry.Shop.Activitys.ViewModels
{
    /// <summary>
    /// </summary>
    [ClassProperty(Name = "活动订单统计", Icon = "fa fa-puzzle-piece", Description = "活动订单统计",
        SideBarType = SideBarType.OrderSideBar)]
    public class ViewActivityOrderPage : BaseViewModel
    {
        /// <summary>
        ///     Key
        /// </summary>

        [Display(Name = "ID")]
        public long Id { get; set; }

        /// <summary>
        ///     缩略图
        /// </summary>
        [Display(Name = "缩略图")]
        //[Field(ControlsType = ControlsType.ImagePreview,IsShowBaseSerach = true, SortOrder = 1)]
        public string ImgUrl { get; set; }

        /// <summary>
        ///     买家备注
        /// </summary>
        [Display(Name = "买家备注")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, ListShow = true, SortOrder = 20)]
        public string OrderRemark { get; set; }

        /// <summary>
        ///     根据Id自动生成12位序列号
        /// </summary>
        [Display(Name = "根据Id自动生成12位序列号")]
        public string Serial { get; set; }

        /// <summary>
        ///     下单用户ID
        /// </summary>
        [Display(Name = "下单用户ID")]
        public long UserId { get; set; }

        /// <summary>
        ///     User
        /// </summary>
        [Display(Name = "用户")]
        public User User { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Display(Name = "用户名")]
        [NotMapped]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            ListShow = true, SortOrder = 2)]
        public string UserName { get; set; }

        /// <summary>
        ///     所属店铺
        /// </summary>
        [Display(Name = "所属店铺")]
        public long StoreId { get; set; }

        /// <summary>
        ///     订单交易状态,OrderStatus等待付款WaitingBuyerPay = 0,等待发货WaitingSellerSendGoods = 1,已发货WaitingBuyerConfirm = 2,交易成功Success =
        ///     3,已取消Cancelled = 4,已作废Invalid = 5,
        /// </summary>
        [Display(Name = "订单状态")]
        [Field(ControlsType = ControlsType.DropdownList, DataSource = "Alabo.App.Shop.Order.Domain.Enums.OrderStatus",
            IsTabSearch = true, IsShowBaseSerach = true, IsShowAdvancedSerach = true, ListShow = true, SortOrder = 3)]
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        ///     订单类型
        /// </summary>
        [Display(Name = "订单类型")]
        [Field(ControlsType = ControlsType.DropdownList, DataSource = "Alabo.App.Shop.Order.Domain.Enums.OrderType",
            IsShowBaseSerach = true, IsShowAdvancedSerach = true, ListShow = false, SortOrder = 4)]
        public OrderType OrderType { get; set; }

        /// <summary>
        /// </summary>
        [Display(Name = "订单类型")]
        [Field(ControlsType = ControlsType.DropdownList, LabelColor = LabelColor.Success, IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, ListShow = true, SortOrder = 4)]
        public string OrderTypeName { get; set; }

        /// <summary>
        ///     订单总金额
        ///     订单总金额=商品总金额-优惠金额-（+）调整金额+税费金额+邮费金额 -其他账户支出
        /// </summary>
        [Display(Name = "订单总金额")]
        [Field(ControlsType = ControlsType.NumberRang, IsMain = true, TableDispalyStyle = TableDispalyStyle.Code,
            IsShowBaseSerach = true, IsShowAdvancedSerach = true, ListShow = true, SortOrder = 5)]
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     订单总数量
        /// </summary>
        [Display(Name = "订单总数量")]
        [Field(ControlsType = ControlsType.NumberRang, LabelColor = LabelColor.Info, IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, ListShow = true, SortOrder = 9)]
        public long TotalCount { get; set; }

        /// <summary>
        ///     活动的具体配置信息
        ///     显示活动配置中List=true的字段
        /// </summary>
        [Display(Name = "配置信息")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "80", ListShow = false,
            DataSource = "ConfigDictionaryType", Mark = "Type", SortOrder = 2)]
        public Dictionary<string, object> ConfigDictionary { get; set; } = new Dictionary<string, object>();

        /// <summary>
        ///     Gets or sets the full name of the configuration dictionary.
        ///     自动配置的全部命名空间
        /// </summary>
        [Display(Name = "自动配置的全部命名空间")]
        public Type ConfigDictionaryType { get; set; }

        /// <summary>
        ///     所属营销活动类型，如：Alabo.App.Shop.Activitys.Domain.Modules.PinTuanActivity
        ///     比如满就送，或者限量购
        /// </summary>
        [Display(Name = "所属营销活动类型")]
        public string Key { get; set; }

        /// <summary>
        ///     具体活动内容，活动类型的Json数据
        /// </summary>
        [Display(Name = "具体活动内容")]
        public string Value { get; set; }

        /// <summary>
        ///     订单实际支付的金额
        ///     订单实际支付的金额=商品总金额-优惠金额-（+）调整金额+邮费金额 -其他账户支出
        /// </summary>
        [Display(Name = "实际支付金额")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowBaseSerach = true,
            TableDispalyStyle = TableDispalyStyle.Code, IsShowAdvancedSerach = true, ListShow = true, SortOrder = 6)]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        ///     使用账户支付的部分
        /// </summary>
        [Display(Name = "使用账户支付的部分")]
        public string AccountPay { get; set; }

        /// <summary>
        ///     OrderId
        /// </summary>
        [Display(Name = "订单编号")]
        public long OrderId { get; set; }

        /// <summary>
        ///     支付方式Id
        /// </summary>
        [Display(Name = "支付方式")]
        public long PayId { get; set; } = 0;

        /// <summary>
        ///     订单扩展数据,业务逻辑非紧密的数据保存到该字段
        ///     包括评价信息、留言信息、商品快照、用户快照、价格详情等信息
        ///     OrderExtensions 的Json数据格式
        /// </summary>
        [Field(ExtensionJson = "Alabo.App.Shop.Order.Domain.Entities.Extensions.OrderExtension")]
        [Display(Name = "订单扩展数据")]
        public string Extension { get; set; }

        /// <summary>
        ///     选择的收货地址id
        /// </summary>
        [Display(Name = "选择的收货地址")]
        public Guid AddressId { get; set; }

        /// <summary>
        ///     下单时间，订单创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Field(ControlsType = ControlsType.DateTimePickerRank, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            ListShow = true, SortOrder = 30)]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     获取链接
        /// </summary>
        public IEnumerable<ViewLink> ViewLinks()
        {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("活动管理", "/Admin/Activitys/Index?Key=[[Key]]", Icons.List, LinkType.FormQuickLink),
                new ViewLink("活动订单统计", "/Admin/Activitys/Index?Method=GetOrderPageList&Key=[[Key]]",
                    FontAwesomeIcon.ShoppingCart.GetIcon(), LinkType.FormQuickLink),
                new ViewLink("活动商品统计", "/Admin/Activitys/Index?Method=GetProductPageList&Key=[[Key]]", Icons.List,
                    LinkType.TableQuickLink),
                new ViewLink("活动管理", "/Admin/Activitys/Index?Method=GetRecordPageList&Key=[[Key]]", Icons.List,
                    LinkType.TableQuickLink),
                new ViewLink("活动删除", "/Admin/Basic/Delete?id=[[Id]]&service=IActivityAdminService&method=Delete",
                    Icons.Delete, LinkType.ColumnLinkDelete)
            };
            return quickLinks;
        }
    }
}