using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Industry.Shop.Orders.Domain.Entities.Extensions;
using Alabo.Industry.Shop.Orders.Domain.Enums;
using Alabo.Industry.Shop.Products.Dtos;
using Alabo.Users.Entities;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.Orders.PcDtos {

    /// <summary>
    /// 订单统一输出模型
    /// </summary>
    public class ApiOrderListOutput : BaseApiOrderList {
    }

    /// <summary>
    /// PC端前端通用列表输出模型
    /// </summary>
    public abstract class BaseApiOrderList : UIBase {
        public string AddressId { get; set; }

        public string Extension { get; set; }

        public OrderExtension OrderExtension { get; set; } = new OrderExtension();

        /// <value>
        ///     Id标识
        /// </value>
        public long Id { get; set; }

        /// <summary>
        ///     订单ID
        /// </summary>
        [Display(Name = "订单编号")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsShowBaseSerach = true, IsShowAdvancedSerach = true, Width = "120", SortOrder = 1)]
        public string Serial {
            get {
                var searSerial = $"9{Id.ToString().PadLeft(9, '0')}";
                if (Id.ToString().Length == 10) {
                    searSerial = $"{Id.ToString()}";
                }

                return searSerial;
            }
            set {
            }
        }

        public User User { get; set; }

        public string Address { get; set; }

        public string RegionName { get; set; }

        /// <summary>
        ///     所属店铺
        /// </summary>
        public long StoreId { get; set; }

        /// <summary>
        ///     店铺名称
        /// </summary>
        [Display(Name = "店铺名称")]
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, Width = "120", SortOrder = 4)]
        public string StoreName { get; set; }

        /// <summary>
        ///     订单交易状态,OrderStatus等待付款WaitingBuyerPay = 0,等待发货WaitingSellerSendGoods = 1,已发货WaitingBuyerConfirm = 2,交易成功Success =
        ///     3,已取消Cancelled = 4,已作废Invalid = 5,
        /// </summary>
        [Display(Name = "订单状态")]
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, IsTabSearch = true, IsShowAdvancedSerach = true, Width = "120", SortOrder = 4)]
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        ///     供Api调用显示
        /// </summary>
        [Display(Name = "订单名称")]
        public string OrderStatuName { get; set; }

        /// <summary>
        ///     订单总数量
        /// </summary>
        [Display(Name = "商品总数量")]
        [Field(ControlsType = ControlsType.NumberRang, ListShow = true, IsShowAdvancedSerach = true, Width = "120", SortOrder = 6)]
        public long TotalCount { get; set; }

        /// <summary>
        ///     运费
        /// </summary>
        [Display(Name = "运费")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "120", SortOrder = 4)]
        public decimal ExpressAmount { get; set; }

        /// <summary>
        ///     订单实际支付的金额
        ///     订单实际支付的金额=商品总金额-优惠金额-（+）调整金额+邮费金额 -其他账户支出
        /// </summary>
        [Display(Name = "实付的金额")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowAdvancedSerach = true, ListShow = true, Width = "120", SortOrder = 7)]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        ///     下单时间，订单创建时间
        /// </summary>
        [Display(Name = "下单时间")]
        [Field(ControlsType = ControlsType.DateTimeRang, IsShowAdvancedSerach = true, ListShow = true, Width = "120", SortOrder = 8)]
        public string CreateTime { get; set; }

        ///// <summary>
        ///// 店铺商品合计
        ///// </summary>
        public IList<ProductSkuItem> OutOrderProducts { get; set; } = new List<ProductSkuItem>();

        #region 高级搜索

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, IsShowAdvancedSerach = true, ListShow = false, SortOrder = 2)]
        public string UserName { get; set; }

        /// <summary>
        /// 下单用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = false, ListShow = false, SortOrder = 3)]
        public long UserId { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        [Display(Name = "订单类型")]
        [Field(ControlsType = ControlsType.DropdownList, IsShowAdvancedSerach = true, ListShow = false, DataSource = "Alabo.App.Shop.Order.Domain.Enums.OrderType", SortOrder = 4)]
        public OrderType OrderType { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        [Display(Name = "订单总金额")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowAdvancedSerach = true, SupportSearchOrder = true, ListShow = false, SortOrder = 5)]
        public decimal TotalAmount { get; set; }

        #endregion 高级搜索

        /// <summary>
        /// 权限类型
        /// </summary>
        public FilterType Filter { get; set; }
    }
}