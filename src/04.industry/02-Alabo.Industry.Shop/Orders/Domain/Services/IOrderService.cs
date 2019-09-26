using System;
using System.Collections.Generic;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.Orders.Domain.Enums;
using Alabo.Industry.Shop.Orders.Dtos;
using Alabo.Industry.Shop.Orders.ViewModels;

namespace Alabo.Industry.Shop.Orders.Domain.Services {

    public interface IOrderService : IService<Entities.Order, long> {

        /// <summary>
        ///     确认收货
        /// </summary>
        /// <param name="parameter"></param>
        ServiceResult Confirm(ConfirmInput parameter);

        /// <summary>
        ///     确认收货, 使用于租户和主库订单同步
        /// </summary>
        /// <param name="parameter"></param>
        ServiceResult ConfirmToMaster(ConfirmInput parameter);

        /// <summary>
        /// 获取快递信息
        /// </summary>
        /// <param name="expressId"></param>
        string GetExpressInfo(string expressId);

        /// <summary>
        /// 获取快递列表
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        IList<Deliver> GetExpressList(long orderId);
        /// <summary>
        /// 发货
        /// </summary>
        ServiceResult Deliver(OrderInput model);

        /// <summary>
        /// 发货 支持一个快递
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResult Deliver(Deliver model);
        /// <summary>
        /// 增加快递
        /// </summary>
        ServiceResult AddExpress(Deliver model);
        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OrderShowOutput Show(long id);

        /// <summary>
        /// 获取快递列表
        /// </summary>
        /// <returns></returns>
        string GetExpress();

        /// <summary>
        ///     评价 订单
        /// </summary>
        /// <param name="parameter"></param>
        ServiceResult Rate(RateInput parameter);

        /// <summary>
        ///     获取单条订单记录
        /// </summary>
        /// <param name="orderId"></param>
        Entities.Order GetSingle(long orderId);

        /// <summary>
        ///     获取前端单条订单记录
        /// </summary>
        OrderShowOutput GetOrderSingle(long id, long UserdId);

        /// <summary>
        ///     获取订单和订单商品
        /// </summary>
        /// <param name="orderId"></param>
        Entities.Order GetSingleWithProducts(long orderId);

        /// <summary>
        ///     获取订单信息
        /// </summary>
        /// <param name="id">主键ID</param>
        OrderShowOutput GetSingle(long id, long UserId);

        /// <summary>
        ///     获取订单信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="UserId">用户Id</param>
        OrderShowOutput GetSingleAdmin(long id, long UserId);

        /// <summary>
        ///     获取订单列表，包括供应商订单，会员订单，后台订单
        /// </summary>
        /// <param name="orderInput"></param>
        PagedList<OrderListOutput> GetPageList(OrderListInput orderInput);

        /// <summary>
        ///     更加订单状态获取
        ///     订单可操作的方法，和方法名称
        ///     比如订单状态为：WaitingBuyerPay 则，可根据特性OrderActionTypeAttribute 获取出 Name = "支付", Method = "OrderPay"
        /// </summary>
        /// <param name="orderStatus"></param>
        List<OrderActionTypeAttribute> GetMethodByStatus(OrderStatus orderStatus);

        /// <summary>
        ///     删除订单
        /// </summary>
        Tuple<ServiceResult, Entities.Order> Delete(long id);

        /// <summary>
        /// GetExpressAmount
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Tuple<ServiceResult, OrderExpressViewModel> GetExpressAmount(long orderId);

        /// <summary>
        /// UpdateExpressAmount
        /// </summary>
        /// <param name="orderExpressInput"></param>
        /// <returns></returns>
        ServiceResult UpdateExpressAmount(OrderExpressViewModel orderExpressInput);
    }
}