using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.App.Shop.Product.Domain.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.UI;
using Alabo.UI.AutoLists;
using Alabo.UI.AutoTables;
using Alabo.Users.Entities;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Order.Domain.Dtos
{

    [ClassProperty(Name = "订单管理", Icon = "fa fa-puzzle-piece")]
    public class OrderListOutput : UIBase, IAutoTable<OrderListOutput>, IAutoList
    {

        /// <value>
        ///     Id标识
        /// </value>
        public long Id { get; set; }

        /// <summary>
        ///     订单ID
        /// </summary>
        [Display(Name = "订单编号")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsShowBaseSerach = true, IsShowAdvancedSerach = true, Width = "120", SortOrder = 1)]
        public string Serial
        {
            get
            {
                var searSerial = $"9{Id.ToString().PadLeft(9, '0')}";
                if (Id.ToString().Length == 10)
                {
                    searSerial = $"{Id.ToString()}";
                }

                return searSerial;
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
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, ListShow = false, SortOrder = 3)]
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

        public List<TableAction> Actions()
        {
            var list = new List<TableAction>
            {
                ToLinkAction("订单", "/Order/Index")
            };
            return list;
        }

        public PageResult<AutoListItem> PageList(object query,AutoBaseModel autoModel)
        {
            throw new System.NotImplementedException();
        }

        #endregion 高级搜索

        /// <summary>
        /// 构建自动表单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PageResult<OrderListOutput> PageTable(object query, AutoBaseModel autoModel)
        {
            var httpContent = HttpWeb.HttpContext.ToDictionary();
            var model = query.ToObject<OrderListInput>();
            model.OrderType = OrderType.Normal;
            var list = Resolve<IOrderService>().GetPageList(model);
            return ToPageResult(list);
        }

        public Type SearchType() {
            throw new NotImplementedException();
        }
    }
}