using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Industry.Shop.OrderActions.Domain.Entities;
using Alabo.Industry.Shop.Orders.Domain.Entities.Extensions;
using Alabo.Industry.Shop.Orders.Domain.Enums;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alabo.Industry.Shop.Orders.Domain.Entities
{
    /// <summary>
    ///     订单表
    /// </summary>
    [ClassProperty(Name = "订单表", Description = "店铺管理", PageType = ViewPageType.List, ListApi = "Api/Order/Index")]
    public class Order : AggregateDefaultUserRoot<Order>
    {
        /// <summary>
        ///     发货用户Id
        /// </summary>
        [Display(Name = "发货用户Id")]
        public long DeliverUserId { get; set; }

        /// <summary>
        ///     所属店铺
        /// </summary>
        [Display(Name = "所属店铺")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true,
            IsShowBaseSerach = true, IsShowAdvancedSerach = true, SortOrder = 1)]
        public long StoreId { get; set; }

        /// <summary>
        ///     订单交易状态,OrderStatus等待付款WaitingBuyerPay = 0,等待发货WaitingSellerSendGoods = 1,已发货WaitingBuyerConfirm = 2,交易成功Success =
        ///     3,已取消Cancelled = 4,已作废Invalid = 5,
        /// </summary>
        [Display(Name = "订单状态")]
        [Field(ControlsType = ControlsType.DropdownList, GroupTabId = 1, Width = "150", ListShow = true,
            IsShowBaseSerach = true, IsShowAdvancedSerach = true, SortOrder = 3)]
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        ///     订单类型
        /// </summary>
        [Display(Name = "订单类型")]
        [Field(ControlsType = ControlsType.DropdownList, GroupTabId = 1, Width = "150", ListShow = true,
            IsShowBaseSerach = true, IsShowAdvancedSerach = true, SortOrder = 2)]
        public OrderType OrderType { get; set; }

        /// <summary>
        ///     订单总金额
        ///     订单总金额=商品总金额-优惠金额-（+）调整金额+税费金额+邮费金额 -其他账户支出
        /// </summary>
        [Display(Name = "订单总金额")]
        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1, Width = "150", ListShow = true, SortOrder = 4)]
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     订单总数量
        /// </summary>
        [Display(Name = "订单总数量")]
        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1, Width = "150", ListShow = true, SortOrder = 5)]
        public long TotalCount { get; set; }

        /// <summary>
        ///     订单实际支付的金额
        ///     订单实际支付的金额=商品总金额-优惠金额-（+）调整金额+邮费金额 -其他账户支出
        /// </summary>
        [Display(Name = "订单实际支付的金额")]
        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1, Width = "150", ListShow = true, SortOrder = 6)]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        ///     使用账户支付的部分
        /// </summary>
        [Display(Name = "使用账户支付的部分")]
        public string AccountPay { get; set; }

        /// <summary>
        ///     支付方式Id
        /// </summary>
        [Display(Name = "支付方式Id")]
        public long PayId { get; set; } = 0;

        /// <summary>
        ///     订单扩展数据,业务逻辑非紧密的数据保存到该字段
        ///     包括评价信息、留言信息、商品快照、用户快照、价格详情等信息
        ///     OrderExtensions 的Json数据格式
        /// </summary>
        [Field(ExtensionJson = "OrderExtension")]
        [Display(Name = "订单扩展数据")]
        public string Extension { get; set; }

        /// <summary>
        ///     选择的收货地址id
        /// </summary>
        [Display(Name = "选择的收货地址id")]
        public string AddressId { get; set; }

        #region 以下为非数据库字段

        /// <summary>
        ///     订单扩展数据，需要附加的时候，可扩展到此字段
        /// </summary>
        [Display(Name = "订单扩展数据")]
        public OrderExtension OrderExtension { get; set; } = new OrderExtension();

        /// <summary>
        ///     使用账户支付的部分
        ///     键值对
        /// </summary>

        [Display(Name = "使用账户支付的部分")]
        public IList<KeyValuePair<Guid, decimal>> AccountPayPair { get; set; } =
            new List<KeyValuePair<Guid, decimal>>();

        /// <summary>
        ///     订单操作类型，包括发货，等等
        /// </summary>
        /// <value>
        ///     The actions.
        /// </value>
        [Display(Name = "订单操作类型")]
        public IList<OrderAction> Actions { get; set; } = new List<OrderAction>();

        /// <summary>
        ///     订单所包含的所有商品
        /// </summary>
        [Display(Name = "订单所包含的所有商品")]
        public IList<OrderProduct> Products { get; set; }

        /// <summary>
        ///     根据Id自动生成12位序列号
        /// </summary>
        [Display(Name = "根据Id自动生成12位序列号")]
        public string Serial
        {
            get
            {
                var searSerial = $"9{Id.ToString().PadLeft(9, '0')}";
                if (Id.ToString().Length == 10) searSerial = $"{Id.ToString()}";

                return searSerial;
            }
        }

        #endregion 以下为非数据库字段
    }

    public class OrderTableMap : MsSqlAggregateRootMap<Order>
    {
        protected override void MapTable(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Shop_Order");
        }

        protected override void MapProperties(EntityTypeBuilder<Order> builder)
        {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.Actions);
            builder.Ignore(e => e.Products);
            builder.Ignore(e => e.Serial);
            builder.Ignore(e => e.AccountPayPair);
            builder.Ignore(e => e.OrderExtension);
         
            if (TenantContext.IsTenant)
            {
                // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
            }
        }
    }
}