using System.Collections.Generic;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Framework.Basic.Address.Domain.Entities;
using Alabo.Industry.Shop.OrderActions.Domain.Entities;
using Alabo.Industry.Shop.OrderActions.Domain.Enums;
using Alabo.Industry.Shop.Orders;
using Alabo.Industry.Shop.Orders.Domain.Entities;
using Alabo.Industry.Shop.Orders.Domain.Entities.Extensions;
using Alabo.Industry.Shop.Orders.Dtos;
using Alabo.Industry.Shop.Orders.ViewModels;
using Alabo.Industry.Shop.Orders.ViewModels.OrderEdit;
using Alabo.Users.Entities;
using Microsoft.AspNetCore.Http;

namespace Alabo.Industry.Shop.OrderActions.Domain.Services
{
    public interface IOrderActionService : IService<OrderAction, long>
    {
        /// <summary>
        ///     添加卖家评价
        /// </summary>
        /// <param name="httpContext"></param>
        ServiceResult Rate(DefaultHttpContext httpContext);

        /// <summary>
        ///     添加卖家评价 (2019.03.31 新用途)
        /// </summary>
        /// <param name="modelIn"></param>
        /// <returns></returns>
        ServiceResult Rate(OrderRateInfo modelIn);

        /// <summary>
        ///     取消订单
        /// </summary>
        /// <param name="httpContext"></param>
        ServiceResult Cancle(DefaultHttpContext httpContext);

        /// <summary>
        ///     取消订单 (2019.03.31 新用途)
        /// </summary>
        /// <param name="modelIn"></param>
        ServiceResult Cancel(OrderActionViewIn modelIn);

        /// <summary>
        ///     删除订单
        /// </summary>
        /// <param name="httpContext"></param>
        ServiceResult Delete(DefaultHttpContext httpContext);

        /// <summary>
        ///     删除订单(03.31 新用途)
        /// </summary>
        /// <param name="modelIn"></param>
        /// <returns></returns>
        ServiceResult Delete(OrderActionViewIn modelIn);

        /// <summary>
        ///     Adds the specified order.
        ///     添加订单操作记录
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="user">The user.</param>
        /// <param name="actionType">Type of the action.</param>
        void Add(Order order, User user, OrderActionType actionType);

        /// <summary>
        ///     获取所有订单操作方式的特性
        ///     通过缓存方式获取
        /// </summary>
        IList<OrderActionTypeAttribute> GetAllOrderActionTypeAttribute();

        OrderAction GetSingle(long id);

        void AddOrUpdate(OrderAction model);

        /// <summary>
        ///     Gets all action intro.
        ///     获取所有的操作特性
        /// </summary>
        Dictionary<OrderActionType, string> GetAllActionIntro();

        /// <summary>
        ///     从购物车中删除商品
        /// </summary>
        /// <param name="orderBuyInput"></param>
        void DeleteCartBuyOrder(BuyInput orderBuyInput);

        /// <summary>
        ///     发货
        /// </summary>
        /// <param name="httpContext"></param>
        ServiceResult Delivery(DefaultHttpContext httpContext);

        /// <summary>
        ///     发货
        /// </summary>
        /// <param name="orderEditDelivery"></param>
        ServiceResult Delivery(OrderEditDelivery orderEditDelivery);

        /// <summary>
        ///     管理员代付
        /// </summary>
        /// <param name="httpContext"></param>
        ServiceResult Pay(DefaultHttpContext httpContext);

        /// <summary>
        ///     备忘
        /// </summary>
        /// <param name="httpContext"></param>
        ServiceResult Remark(DefaultHttpContext httpContext);

        /// <summary>
        ///     留言
        /// </summary>
        /// <param name="httpContext"></param>
        ServiceResult Message(DefaultHttpContext httpContext);

        /// <summary>
        ///     修改地址
        /// </summary>
        /// <param name="httpContext"></param>
        ServiceResult Address(DefaultHttpContext httpContext);

        /// <summary>
        ///     修改地址 (2019.03.31 新用途)
        /// </summary>
        /// <param name="modelIn"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        ServiceResult Address(UserAddress modelIn, long orderId);

        /// <summary>
        ///     导出表格
        /// </summary>
        /// <returns></returns>
        PagedList<OrderToExcel> GetOrdersToExcel(object query);
    }
}