using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Alabo.App.Asset.Pays.Domain.Entities.Extension;
using Alabo.App.Asset.Pays.Domain.Services;
using Alabo.Data.People.Stores.Domain.Entities.Extensions;
using Alabo.Data.People.Stores.Domain.Services;
using Alabo.Data.People.Users.Domain.Repositories;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Data.People.Users.Dtos;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.Address.Domain.Services;
using Alabo.Framework.Basic.Regions.Domain.Services;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Helpers;
using Alabo.Industry.Shop.AfterSales.Domain.Enums;

using Alabo.Industry.Shop.Deliveries.Domain.Services;
using Alabo.Industry.Shop.OrderActions.Domain.Entities;
using Alabo.Industry.Shop.OrderActions.Domain.Enums;
using Alabo.Industry.Shop.OrderActions.Domain.Services;
using Alabo.Industry.Shop.OrderDeliveries.Domain.Services;
using Alabo.Industry.Shop.Orders.Domain.Entities;
using Alabo.Industry.Shop.Orders.Domain.Entities.Extensions;
using Alabo.Industry.Shop.Orders.Domain.Enums;
using Alabo.Industry.Shop.Orders.Domain.Repositories;
using Alabo.Industry.Shop.Orders.Dtos;
using Alabo.Industry.Shop.Orders.PcDtos;
using Alabo.Industry.Shop.Orders.ViewModels;
using Alabo.Industry.Shop.Products.Domain.Enums;
using Alabo.Industry.Shop.Products.Domain.Services;
using Alabo.Mapping;
using Alabo.Runtime;
using Alabo.Tenants;
using Alabo.Tenants.Domain.Services;
using File = System.IO.File;

namespace Alabo.Industry.Shop.Orders.Domain.Services
{
    /// <summary>
    /// </summary>
    public class OrderService : ServiceBase<Order, long>, IOrderService
    {
        private static readonly string _OrderCacheKey = "OrderCacheKey_";

        public OrderService(IUnitOfWork unitOfWork, IRepository<Order, long> repository) : base(unitOfWork, repository)
        {
        }

        /// <summary>
        ///     更加订单状态获取
        ///     订单可操作的方法，和方法名称
        ///     比如订单状态为：WaitingBuyerPay 则，可根据特性OrderActionTypeAttribute 获取出 Name = "支付", Method = "OrderPay"
        /// </summary>
        /// <param name="orderStatus"></param>
        public List<OrderActionTypeAttribute> GetMethodByStatus(OrderStatus orderStatus)
        {
            var cacheKey = _OrderCacheKey + orderStatus;
            if (!ObjectCache.TryGet(cacheKey, out List<OrderActionTypeAttribute> result))
            {
                result = new List<OrderActionTypeAttribute>();
                foreach (OrderActionType item in Enum.GetValues(typeof(OrderActionType)))
                {
                    var actionTypeAttribute = item.GetCustomAttr<OrderActionTypeAttribute>();
                    if (actionTypeAttribute != null && actionTypeAttribute.AllowStatus != null &&
                        actionTypeAttribute.AllowStatus.Contains(orderStatus.ToString())) {
                        result.Add(actionTypeAttribute);
                    }
                }
            }

            return result;
        }

        /// <summary>
        ///     Gets the single.
        /// </summary>
        /// <param name="id">Id标识</param>
        /// <param name="UserdId">The userd identifier.</param>
        public OrderShowOutput GetSingle(long id, long UserdId)
        {
            var order = GetSingle(r => r.Id == id && r.UserId == UserdId);
            if (order == null) {
                return null;
            }

            var pay = Resolve<IPayService>().GetSingle(order.PayId);
            var orderShow = AutoMapping.SetValue<OrderShowOutput>(order);
            orderShow.CreateTime = order.CreateTime.ToDateTimeString();
            if (pay != null)
            {
                orderShow.PayTime = pay.ResponseTime.ToDateTimeString();
                orderShow.PaymentType = pay.PayType.GetDisplayName();
            }

            orderShow.ExpressAmount = order.OrderExtension.OrderAmount.ExpressAmount;
            orderShow.IsGroupBuy = order.OrderExtension.IsGroupBuy;
            orderShow.StoreName = order.OrderExtension.Store.Name;
            orderShow.ProductSkuItems = order.OrderExtension.ProductSkuItems;
            orderShow.ProductSkuItems.ToList().ForEach(c =>
            {
                c.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(c.ThumbnailUrl);
            });
            orderShow.Order = order;
            orderShow.Serial = order.Serial;
            //订单物流信息
            var orderDeliveries = Resolve<IOrderDeliveryService>().GetList(e => e.OrderId == order.Id);
            foreach (var item in orderDeliveries)
            {
                var store = Resolve<IStoreService>().GetSingle(item.StoreId);
                //  store.StoreExtension = store.Extension.DeserializeJson<StoreExtension>();
                //item.Name = store.StoreExtension.DeliveryTemplate
                //    .SingleOrDefault(e => e.ExpressId == item.ExpressGuid)?.TemplateName;
                orderShow.OrderDeliverys.Add(item);
            }

            //会员订单操作方法，根据状态显示
            var isAdmin = Resolve<IUserService>().IsAdmin(UserdId);
            var allMethods = Resolve<IOrderService>().GetMethodByStatus(order.OrderStatus);

            // 当操作用户IsAdmin时, 返回所有操作方法; 其他的返回 User 的方法
            orderShow.Methods = isAdmin
                ? allMethods.Where(r => r.AllowUser == 2).ToList()
                : allMethods.Where(r => r.AllowUser == 0).ToList();

            var user = Resolve<IUserService>().GetSingle(order.UserId);
            orderShow.BuyUser = AutoMapping.SetValue<UserOutput>(user);

            var deliverUser = Resolve<IUserService>().GetSingle(order.DeliverUserId);
            if (deliverUser != null) {
                orderShow.DeliverUser = AutoMapping.SetValue<UserOutput>(deliverUser);
            }

            return orderShow;
        }

        /// <summary>
        ///     Gets the single.
        /// </summary>
        /// <param name="id">Id标识</param>
        /// <param name="UserdId">The userd identifier.</param>
        public OrderShowOutput GetSingleAdmin(long id, long UserdId)
        {
            var order = GetSingle(r => r.Id == id);
            if (order == null) {
                return null;
            }

            var pay = Resolve<IPayService>().GetSingle(order.PayId);
            var orderShow = AutoMapping.SetValue<OrderShowOutput>(order);
            orderShow.CreateTime = order.CreateTime.ToDateTimeString();
            if (pay != null)
            {
                orderShow.PayTime = pay.ResponseTime.ToDateTimeString();
                orderShow.PaymentType = pay.PayType.GetDisplayName();
            }

            var regionId = order.OrderExtension.UserAddress.RegionId;
            //获取区域
            if (regionId > 0)
            {
                var address = Resolve<IRegionService>().GetFullName(regionId);
                order.OrderExtension.UserAddress.Address = address + order.OrderExtension.UserAddress.Address;
                order.OrderExtension.UserAddress.AddressDescription =
                    address + order.OrderExtension.UserAddress.AddressDescription;
            }

            //var address = Resolve<IUserAddressService>().get(order.AddressId);

            orderShow.ExpressAmount = order.OrderExtension.OrderAmount.ExpressAmount;
            orderShow.IsGroupBuy = order.OrderExtension.IsGroupBuy;
            orderShow.StoreName = order.OrderExtension.Store.Name;
            orderShow.ProductSkuItems = order.OrderExtension.ProductSkuItems;
            orderShow.ProductSkuItems.ToList().ForEach(c =>
            {
                c.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(c.ThumbnailUrl);
            });
            orderShow.Order = order;
            orderShow.Serial = order.Serial;
            //订单物流信息
            var orderDeliveries = Resolve<IDeliverService>().GetSingle(e => e.OrderId == order.Id);
            orderShow.Deliver = orderDeliveries;
            //foreach (var item in orderDeliveries) {
            //    var store = Resolve<IStoreService>().GetSingle(e => e.Id == item.StoreId);
            //    store.StoreExtension = store.Extension.DeserializeJson<StoreExtension>();
            //    //item.Name = store.StoreExtension.DeliveryTemplate
            //    //    .SingleOrDefault(e => e.ExpressId == item.ExpressGuid)?.TemplateName;
            //    orderShow.OrderDeliverys.Add(item);
            //}

            //会员订单操作方法，根据状态显示
            var isAdmin = Resolve<IUserService>().IsAdmin(UserdId);

            var allMethods = Resolve<IOrderService>().GetMethodByStatus(order.OrderStatus).Where(r => r.AllowUser == 2);

            // 当操作用户IsAdmin时, 返回所有操作方法; 其他的返回 User 的方法
            if (isAdmin)
            {
                //如果已经申请过退货或者退款的只能查看
                if (order.OrderExtension.RefundInfo != null)
                {
                    //关闭 已完成 拒绝退款 不能显示退款按钮
                    //取消通过和拒绝按钮
                    if (order.OrderExtension.RefundInfo.Process == RefundStatus.Closed ||
                        order.OrderExtension.RefundInfo.Process == RefundStatus.Sucess ||
                        order.OrderExtension.RefundInfo.Process == RefundStatus.WaitSaleRefuse)
                    {
                        // 同意 拒绝  退款  都移除
                        orderShow.Methods = allMethods.Where(s => !s.Type.Equal("AdminAllowRefund"))
                            .Where(s => !s.Type.Equal("AdminRefuseRefund"))
                            .Where(s => !s.Type.Equal("AdminRefund"))
                            .Where(s => !s.Type.Equal("AdminRefundInfo"))
                            .ToList();
                    }
                    else if (order.OrderExtension.RefundInfo.Process == RefundStatus.WaitSaleAllow)
                    {
                        //  同意 都移除
                        orderShow.Methods = allMethods.Where(s => !s.Type.Equal("AdminAllowRefund"))
                            .Where(s => !s.Type.Equal("AdminRefuseRefund"))
                            .Where(s => !s.Type.Equal("AdminRefundInfo"))
                            .ToList();
                    }
                    else
                    {
                        if (order.OrderStatus == OrderStatus.AfterSale) {
                            orderShow.Methods = allMethods.Where(s => !s.Type.Equal("AdminRefundInfo"))
                                .Where(s => !s.Type.Equal("AdminRefund")).ToList();
                        } else {
                            orderShow.Methods = allMethods.Where(s => !s.Type.Equal("AdminRefundInfo")).ToList();
                        }
                    }

                    //增加售后详情
                    var after = Resolve<IOrderService>().GetMethodByStatus(OrderStatus.AfterSale)
                        .Where(r => r.AllowUser == 2).FirstOrDefault(s => s.Type.Equal("UserRefundInfo"));
                    if (after != null) {
                        orderShow.Methods.Add(after);
                    }
                    //orderShow.Methods.Add(allMethods.FirstOrDefault(s => s.Type == "AdminRefundInfo"));
                }
                else
                {
                    orderShow.Methods = allMethods.ToList();
                }
            }
            else
            {
                //申请过退款的不能再申请第二次
                if (order.OrderExtension.RefundInfo != null)
                {
                    //申请按钮 移除
                    orderShow.Methods = allMethods.Where(r => r.AllowUser == 0).Where(s => !s.Type.Equal("Refund"))
                        .Where(s => !s.Type.Equal("AfterSale")).ToList();
                    //增加售后详情
                    orderShow.Methods.AddRange(allMethods.Where(s => s.Type == "UserRefundInfo").ToList());
                }
                else
                {
                    orderShow.Methods = allMethods.Where(r => r.AllowUser == 0).ToList();
                }
            }

            if (orderShow.OrderStatus == OrderStatus.Remited) {
                orderShow.Methods = Resolve<IOrderService>().GetMethodByStatus(order.OrderStatus);
            }

            //if (order.OrderStatus == OrderStatus.WaitingReceiptProduct && order.OrderExtension.RefundInfo != null)
            //{
            //    var sendMethod = allMethods.FirstOrDefault(x => x.Name == "查看物流");

            //    if (sendMethod != null)
            //    {
            //        orderShow.Methods.Add(sendMethod);
            //    }
            //}
            var user = Resolve<IUserService>().GetSingle(order.UserId);
            orderShow.BuyUser = AutoMapping.SetValue<UserOutput>(user);

            var deliverUser = Resolve<IUserService>().GetSingle(order.DeliverUserId);
            if (deliverUser != null) {
                orderShow.DeliverUser = AutoMapping.SetValue<UserOutput>(deliverUser);
            }

            return orderShow;
        }

        /// <summary>
        ///     Gets the single.
        /// </summary>
        /// <param name="id">Id标识</param>
        /// <param name="UserdId">The userd identifier.</param>
        public OrderShowOutput GetOrderSingle(long id, long UserdId)
        {
            var order = GetSingle(r => r.Id == id && r.UserId == UserdId);

            if (order == null) {
                return null;
            }

            var pay = Resolve<IPayService>().GetSingle(order.PayId);
            var orderShow = AutoMapping.SetValue<OrderShowOutput>(order);
            orderShow.CreateTime = order.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            if (pay != null)
            {
                orderShow.PayTime = pay.ResponseTime.ToString("yyyy-MM-dd HH:mm:ss");
                orderShow.PaymentType = pay.PayType.GetDisplayName();
            }

            orderShow.ExpressAmount = order.OrderExtension.OrderAmount.ExpressAmount;
            orderShow.IsGroupBuy = order.OrderExtension.IsGroupBuy;
            orderShow.StoreName = order.OrderExtension.Store.Name;
            orderShow.ProductSkuItems = order.OrderExtension.ProductSkuItems;
            orderShow.ProductSkuItems.ToList().ForEach(c =>
            {
                c.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(c.ThumbnailUrl);
            });
            orderShow.Order = order;
            orderShow.Serial = order.Serial;
            //订单物流信息
            var orderDeliveries = Resolve<IOrderDeliveryService>().GetList(e => e.OrderId == order.Id);
            foreach (var item in orderDeliveries)
            {
                var store = Resolve<IStoreService>().GetSingle(item.StoreId);
                //  store.StoreExtension = store.Extension.DeserializeJson<StoreExtension>();
                //item.Name = store.StoreExtension.DeliveryTemplate
                //    .SingleOrDefault(e => e.ExpressId == item.ExpressGuid)?.TemplateName;
                orderShow.OrderDeliverys.Add(item);
            }

            //会员订单操作方法，根据状态显示

            var allMethods = Resolve<IOrderService>().GetMethodByStatus(order.OrderStatus).Where(r => r.AllowUser == 0)
                .ToList();
            //申请过退款的不能再申请第二次
            if (order.OrderExtension.RefundInfo != null)
            {
                //申请按钮 移除
                orderShow.Methods = allMethods.Where(s => !s.Type.Equal("Refund"))
                    .Where(s => !s.Type.Equal("AfterSale")).Where(s => !s.Type.Equal("UserRefundInfo")).ToList();
                //增加售后详情这里有可能为空记得判断
                var after = Resolve<IOrderService>().GetMethodByStatus(OrderStatus.AfterSale)
                    .Where(r => r.AllowUser == 0).FirstOrDefault(s => s.Type.Equal("UserRefundInfo"));
                if (after != null) {
                    orderShow.Methods.Add(after);
                }
            }
            else
            {
                orderShow.Methods = allMethods.Where(r => r.AllowUser == 0).ToList();
            }

            if (orderShow.OrderStatus == OrderStatus.WaitingBuyerPay)
            {
                var proids = orderShow.ProductSkuItems.Select(s => s.ProductId);
                var productNotOnline = Resolve<IProductService>().GetList(s => proids.Contains(s.Id));
                foreach (var product in productNotOnline)
                {
                    var ext = orderShow.ProductSkuItems.SingleOrDefault(s => s.ProductId == product.Id);
                    if (ext != null) {
                        if (product.ProductStatus != ProductStatus.Online)
                        {
                            ext.ProductStatus = product.ProductStatus;
                            orderShow.Methods = orderShow.Methods.Where(s => !s.Type.Equal("Pay")).ToList();
                            orderShow.OrderStatus = OrderStatus.UnderShelf; //标识为下架订单
                        }
                    }
                }
            }

            //orderShow.Methods = orderShow.Methods.Where(e => e.Method == e.Method.Replace("User", "")).ToList();

            var user = Resolve<IUserService>().GetSingle(order.UserId);
            orderShow.BuyUser = AutoMapping.SetValue<UserOutput>(user);

            var deliverUser = Resolve<IUserService>().GetSingle(order.DeliverUserId);
            if (deliverUser != null) {
                orderShow.DeliverUser = AutoMapping.SetValue<UserOutput>(deliverUser);
            }

            return orderShow;
        }

        /// <summary>
        ///     店铺查看订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderShowOutput Show(long id)
        {
            var order = Resolve<IOrderService>().GetSingle(id);
            if (order == null) {
                return null;
            }

            var pay = Resolve<IPayService>().GetSingle(order.PayId);
            var orderShow = AutoMapping.SetValue<OrderShowOutput>(order);
            orderShow.CreateTime = order.CreateTime.ToDateTimeString();
            if (pay != null)
            {
                orderShow.PayTime = pay.ResponseTime.ToDateTimeString();
                orderShow.PaymentType = pay.PayType.GetDisplayName();
            }

            orderShow.ExpressAmount = order.OrderExtension.OrderAmount.ExpressAmount;
            orderShow.IsGroupBuy = order.OrderExtension.IsGroupBuy;
            orderShow.StoreName = order.OrderExtension.Store.Name;
            orderShow.ProductSkuItems = order.OrderExtension.ProductSkuItems;
            orderShow.ProductSkuItems.ToList().ForEach(c =>
            {
                c.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(c.ThumbnailUrl);
            });
            orderShow.Order = order;
            orderShow.Serial = order.Serial;
            //订单物流信息
            var orderDeliveries = Resolve<IOrderDeliveryService>().GetList(e => e.OrderId == order.Id);
            foreach (var item in orderDeliveries)
            {
                //TODO 2019年9月27日 订单修改
                var store = Resolve<IStoreService>().GetSingle(item.StoreId);
                //   store.StoreExtension = store.Extension.DeserializeJson<StoreExtension>();
                //item.Name = store.StoreExtension.DeliveryTemplate
                //    .SingleOrDefault(e => e.ExpressId == item.ExpressGuid)?.TemplateName;
                orderShow.OrderDeliverys.Add(item);
            }

            //会员订单操作方法，根据状态显示

            orderShow.Methods = Resolve<IOrderService>().GetMethodByStatus(order.OrderStatus)
                .Where(r => r.AllowUser == 1).ToList();
            //orderShow.Methods = orderShow.Methods.Where(e => e.Method == e.Method.Replace("User", "")).ToList();

            var user = Resolve<IUserService>().GetSingle(order.UserId);
            orderShow.BuyUser = AutoMapping.SetValue<UserOutput>(user);

            return orderShow;
        }

        /// <summary>
        ///     获取订单列表，包括供应商订单，会员订单，后台订单
        /// </summary>
        /// <param name="orderInput"></param>
        public PagedList<OrderListOutput> GetPageList(OrderListInput orderInput)
        {
            var query = new ExpressionQuery<Order>();
            if (orderInput.OrderStatus > 0) {
                query.And(e => e.OrderStatus == orderInput.OrderStatus);
            }

            if (orderInput.OrderType.HasValue) {
                query.And(e => e.OrderType == orderInput.OrderType);
            }

            if (!orderInput.StoreId.IsObjectIdNullOrEmpty()) {
                query.And(e => e.StoreId == orderInput.StoreId.ToString());
            }

            if (orderInput.DeliverUserId > 0) // 发货记录
{
                query.And(e => e.DeliverUserId == orderInput.DeliverUserId);
            }

            if (orderInput.UserId > 0) //订单记录
{
                query.And(e => e.UserId == orderInput.UserId);
            } else if (orderInput.LoginUserId > 0) //订单记录
{
                query.And(e => e.UserId == orderInput.LoginUserId);
            }

            if (orderInput.OrderType > 0) {
                query.And(e => e.OrderType == orderInput.OrderType);
            }

            query.PageIndex = (int)orderInput.PageIndex;
            query.PageSize = (int)orderInput.PageSize;
            query.EnablePaging = true;
            query.OrderByDescending(e => e.Id);
            var orders = Resolve<IOrderService>().GetPagedList(query);
            if (orders.Count < 0) {
                return new PagedList<OrderListOutput>();
            }

            var model = new List<OrderListOutput>();

            foreach (var item in orders)
            {
                var listOutput = new OrderListOutput
                {
                    CreateTime = item?.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    Id = item.Id,
                    OrderStatuName = item?.OrderStatus.GetDisplayName(),
                    User = item?.OrderExtension?.User,
                    Address = item?.OrderExtension?.UserAddress?.Address
                };
                if (item.OrderExtension != null)
                {
                    if (item.OrderExtension.UserAddress != null) {
                        listOutput.RegionName = Resolve<IRegionService>()
                            .GetRegionNameById(item.OrderExtension.UserAddress.RegionId);
                    }
                }
                else
                {
                    listOutput.RegionName = null;
                }

                listOutput = AutoMapping.SetValue(item, listOutput);
                var orderExtension = item.Extension.DeserializeJson<OrderExtension>();
                if (orderExtension != null && orderExtension.Store != null)
                {
                    listOutput.StoreName = orderExtension.Store.Name;
                    listOutput.ExpressAmount = orderExtension.OrderAmount.ExpressAmount;
                }

                if (item.OrderStatus == OrderStatus.WaitingBuyerPay)
                {
                    var proids = orderExtension.ProductSkuItems.Select(s => s.ProductId);

                    var productNotOnline = Resolve<IProductService>().GetList(s =>
                        s.ProductStatus != ProductStatus.Online && proids.Contains(s.Id));
                    foreach (var product in productNotOnline)
                    {
                        var ext = orderExtension.ProductSkuItems.SingleOrDefault(s => s.ProductId == product.Id);
                        if (ext != null)
                        {
                            ext.ProductStatus = product.ProductStatus;
                            listOutput.OrderStatus = OrderStatus.UnderShelf; //标识为下架订单
                        }
                    }
                }

                listOutput.OutOrderProducts = orderExtension.ProductSkuItems;
                listOutput.OutOrderProducts?.ToList().ForEach(c =>
                {
                    c.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(c.ThumbnailUrl);
                });
                model.Add(listOutput);
            }

            return PagedList<OrderListOutput>.Create(model, orders.RecordCount, orders.PageSize, orders.PageIndex);
        }

        /// <summary>
        ///     订单删除
        /// </summary>
        /// <param name="id">主键ID</param>
        public Tuple<ServiceResult, Order> Delete(long id)
        {
            var result = ServiceResult.Success;
            var model = Resolve<IOrderService>().GetSingle(u => u.Id == id);
            if (model == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("删除失败"), new Order());
            }

            var context = Repository<IOrderRepository>().RepositoryContext;
            context.BeginTransaction();
            Resolve<IOrderService>().Delete(u => u.Id == id);
            context.CommitTransaction();
            return Tuple.Create(result, model);
        }

        public Order GetSingle(long orderId)
        {
            var order = GetSingle(r => r.Id == orderId);
            if (order == null) {
                return null;
            }

            order.OrderExtension = order.Extension.DeserializeJson<OrderExtension>();
            return order;
        }

        /// <summary>
        ///     获取订单和订单商品
        /// </summary>
        /// <param name="orderId"></param>
        public Order GetSingleWithProducts(long orderId)
        {
            var order = GetSingle(orderId);
            order.Products = Resolve<IOrderProductService>().GetList(r => r.OrderId == order.Id).ToList();
            order.Products.Foreach(r =>
            {
                r.OrderProductExtension = r.Extension.DeserializeJson<OrderProductExtension>();
            });
            return order;
        }

        /// <summary>
        ///     订单评价
        /// </summary>
        /// <param name="parameter"></param>
        public ServiceResult Rate(RateInput parameter)
        {
            var result = ServiceResult.Success;
            var orderId = parameter.EntityId.ConvertToLong();
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == orderId);
            if (order == null) {
                return ServiceResult.FailedWithMessage("订单不存在");
            }

            var user = Resolve<IUserService>().GetSingle(parameter.LoginUserId);
            if (user == null) {
                return ServiceResult.FailedWithMessage("当前用户不存在！");
            }

            if (order.OrderExtension.OrderRemark == null) {
                order.OrderExtension.OrderRemark = new OrderRemark();
            }

            var rateInfo = AutoMapping.SetValue<OrderRateInfo>(parameter);
            if (order.OrderExtension.OrderRate == null) {
                order.OrderExtension.OrderRate = new OrderRate();
            }

            order.OrderExtension.OrderRate.BuyerRate = rateInfo;
            order.Extension = order.OrderExtension.ToJson();
            order.OrderStatus = OrderStatus.Success;
            var context = Repository<IOrderRepository>().RepositoryContext;
            context.BeginTransaction();
            Resolve<IOrderService>().Update(order);
            Resolve<IOrderActionService>().Add(order, user, OrderActionType.UserEvaluate);
            context.CommitTransaction();
            return ServiceResult.Success;
        }

        /// <summary>
        ///     确认收货
        /// </summary>
        /// <param name="parameter"></param>
        public ServiceResult Confirm(ConfirmInput parameter)
        {
            var user = Resolve<IUserService>().GetUserDetail(parameter.LoginUserId);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户不存在！");
            }

            if (user.Detail.PayPassword != parameter.PayPassword.ToMd5HashString()) {
                return ServiceResult.FailedWithMessage("支付密码不正确！");
            }

            var orderId = parameter.EntityId.ConvertToLong();
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == orderId);
            if (order == null) {
                return ServiceResult.FailedWithMessage("订单不存在！");
            }

            if (order.OrderStatus == OrderStatus.WaitingReceiptProduct) {
                order.OrderStatus = OrderStatus.WaitingEvaluated;
            } else {
                return ServiceResult.FailedWithMessage("商品状态不支持收货确认操作！");
            }

            var result = Resolve<IOrderService>().Update(order);
            if (result)
            {
                var orderAction = new OrderAction
                {
                    Intro = "会员已确认收货",
                    OrderId = order.Id,
                    ActionUserId = order.UserId,
                    OrderActionType = OrderActionType.UserConfirmOrder
                };
                Resolve<IOrderActionService>().Add(orderAction);

                order.OrderExtension = order.Extension.ToObject<OrderExtension>();
                // 带有TenantOrderInfo的是租户订单, 在此执行主库订单确认收货
                if (TenantContext.CurrentTenant != "master" && order?.OrderExtension?.TenantOrderInfo?.OrderId > 0)
                {
                    var host = RuntimeContext.Current.WebsiteConfig.ClientHost;
                    var url = $"{host}/Api/Order/ConfirmToMaster";
                    var paras = new ConfirmInput
                    {
                        EntityId = order.OrderExtension.TenantOrderInfo.OrderId.ToString(),
                        LoginUserId = 1
                    };

                    var rsComfirmMasterOrder = url.Post(paras.ToJsons());
                }

                // 收货后如果是租户订单, 同步到租户的自提
                if (TenantContext.CurrentTenant == "master" && order?.OrderExtension?.TenantOrderInfo?.OrderId > 0)
                {
                    var tenant = Resolve<ITenantService>().GetSingle(x => x.UserId == order.UserId);
                    if (tenant != null)
                    {
                        var sql =
                            $@"SELECT o.id OrderId ,p.* FROM Shop_Order o JOIN Shop_OrderProduct od ON o.Id = od.OrderId JOIN Shop_Product p ON p.Id = od.ProductId
                             WHERE o.Id = {order.Id} AND o.OrderStatus = 4 AND o.UserId = {order.UserId}";
                        var orderProductList = Ioc.Resolve<IUserRepository>().RepositoryContext
                            .Fetch<PickUpProductView>(sql);
                        if (orderProductList.Count > 0)
                        {
                            var apiUrl = tenant.ServiceUrl + "/Api/PickUpProduct/AddPickUpProduct";
                            var dicHeader = new Dictionary<string, string>
                            {
                                {"zk-tenant", tenant.Sign}
                            };

                            apiUrl.Post(orderProductList.ToJsons(), dicHeader);
                        }
                    }
                }
            }

            return ServiceResult.Success;
        }

        /// <summary>
        ///     主库订单确认收货
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public ServiceResult ConfirmToMaster(ConfirmInput parameter)
        {
            if (TenantContext.CurrentTenant != "master") {
                return ServiceResult.Failed;
            }

            var user = Resolve<IUserService>().GetUserDetail(parameter.LoginUserId);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户不存在！");
            }

            //if (user.Detail.PayPassword != parameter.PayPassword.ToMd5HashString())
            //{
            //    return ServiceResult.FailedWithMessage("支付密码不正确！");
            //}

            var orderId = parameter.EntityId.ConvertToLong();
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == orderId);
            if (order == null) {
                return ServiceResult.FailedWithMessage("订单不存在！");
            }

            if (order.OrderStatus == OrderStatus.WaitingReceiptProduct) {
                order.OrderStatus = OrderStatus.WaitingEvaluated;
            } else {
                return ServiceResult.FailedWithMessage("商品状态不支持收货确认操作！");
            }

            var result = Resolve<IOrderService>().Update(order);
            if (result)
            {
                var orderAction = new OrderAction
                {
                    Intro = "会员已确认收货",
                    OrderId = order.Id,
                    ActionUserId = order.UserId,
                    OrderActionType = OrderActionType.UserConfirmOrder
                };
                Resolve<IOrderActionService>().Add(orderAction);

                order.OrderExtension = order.Extension.ToObject<OrderExtension>();
                if (TenantContext.CurrentTenant == "master" && order?.OrderExtension?.TenantOrderInfo?.OrderId > 0)
                {
                    // 收货后如果是租户订单, 同步到租户的自提
                    var tenant = Resolve<ITenantService>().GetSingle(x => x.UserId == order.UserId);
                    if (tenant != null)
                    {
                        var sql =
                            $@"SELECT o.id OrderId ,p.* FROM Shop_Order o JOIN Shop_OrderProduct od ON o.Id = od.OrderId JOIN Shop_Product p ON p.Id = od.ProductId
                             WHERE o.Id = {order.Id} AND o.OrderStatus = 4 AND o.UserId = {order.UserId}";
                        var orderProductList = Ioc.Resolve<IUserRepository>().RepositoryContext
                            .Fetch<PickUpProductView>(sql);
                        if (orderProductList.Count > 0)
                        {
                            var apiUrl = tenant.ServiceUrl + "/Api/PickUpProduct/AddPickUpProduct";
                            var dicHeader = new Dictionary<string, string>
                            {
                                {"zk-tenant", tenant.Sign}
                            };

                            apiUrl.Post(orderProductList.ToJsons(), dicHeader);
                        }
                    }
                }
            }

            return ServiceResult.Success;
        }

        /// <summary>
        ///     获取订单邮费
        /// </summary>
        public Tuple<ServiceResult, OrderExpressViewModel> GetExpressAmount(long orderId)
        {
            var result = new OrderExpressViewModel();
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == orderId);
            if (order == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("订单不存在"), result);
            }

            var orderExpress = order.Extension.ToObject<OrderExtension>();
            if (orderExpress == null || orderExpress.OrderAmount == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("订单数据异常"), result);
            }

            result.OrderId = orderId;
            result.ExpressAmount = orderExpress.OrderAmount.ExpressAmount;
            result.CalculateExpressAmount = orderExpress.OrderAmount.CalculateExpressAmount;
            result.ExpressDescription = orderExpress.OrderAmount.ExpressDescription;

            return Tuple.Create(ServiceResult.Success, result);
        }

        /// <summary>
        ///     获取订单邮费
        /// </summary>
        public ServiceResult UpdateExpressAmount(OrderExpressViewModel orderExpressInput)
        {
            var result = new OrderExpressViewModel();
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == orderExpressInput.OrderId);
            if (order == null) {
                return ServiceResult.FailedWithMessage("订单不存在");
            }

            var user = Resolve<IUserService>().GetSingle(orderExpressInput.LoginUserId);
            if (user == null) {
                return ServiceResult.FailedWithMessage("当前用户不存在！");
            }

            var pay = Resolve<IPayService>().GetSingle(p => p.Id == order.PayId);
            if (pay == null) {
                return ServiceResult.FailedWithMessage("订单支付信息不存在");
            }
            //order info
            order.OrderExtension.OrderAmount.ExpressAmount = orderExpressInput.ExpressAmount;
            order.OrderExtension.OrderAmount.ExpressDescription = orderExpressInput.ExpressDescription;
            order.Extension = order.OrderExtension.ToJson();
            order.PaymentAmount = order.OrderExtension.OrderAmount.TotalProductAmount + orderExpressInput.ExpressAmount;
            order.TotalAmount = order.PaymentAmount;
            //pay info
            pay.Amount = order.PaymentAmount;
            pay.PayExtension = pay.Extensions.ToObject<PayExtension>();
            pay.PayExtension.TotalAmount = pay.Amount;
            pay.Extensions = pay.PayExtension.ToJson();

            var context = Repository<IOrderRepository>().RepositoryContext;
            try
            {
                context.BeginTransaction();

                //update order amount
                Resolve<IOrderService>().Update(order);

                //add order action
                Resolve<IOrderActionService>().Add(order, user, OrderActionType.AdminEditExpressAmount);

                //pay
                Resolve<IPayService>().Update(pay);

                context.CommitTransaction();
            }
            catch (Exception ex)
            {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage($"修改邮费异常，{ex.Message}");
            }
            finally
            {
                context.DisposeTransaction();
            }

            return ServiceResult.Success;
        }

        #region 快递相关

        /// <summary>
        ///     发货 旧版 ,只支持一个快递号
        ///     只更改订单状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ServiceResult Deliver(Deliver model)
        {
            var order = Resolve<IOrderService>().GetSingle(u => u.Id == model.OrderId);
            if (order == null) {
                return ServiceResult.FailedMessage("订单有误");
            }

            if (order.OrderStatus != OrderStatus.WaitingSellerSendGoods) {
                return ServiceResult.FailedWithMessage("只有待发货的商品可以发货");
            }

            try
            {
                order.OrderStatus = OrderStatus.WaitingReceiptProduct;
                Resolve<IOrderService>().Update(order);
                Resolve<IDeliverService>().Add(model);
            }
            catch (Exception e)
            {
                return ServiceResult.FailedWithMessage(e.ToString());
            }

            return ServiceResult.Success;
        }

        /// <summary>
        ///     发货
        ///     只更改订单状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ServiceResult Deliver(OrderInput model)
        {
            var order = Resolve<IOrderService>().GetSingle(u => u.Id == model.OrderID);
            if (order == null) {
                return ServiceResult.FailedMessage("订单有误");
            }

            if (order.OrderStatus != OrderStatus.WaitingSellerSendGoods) {
                if (order.OrderStatus != OrderStatus.Remited) {
                    return ServiceResult.FailedWithMessage("只有待发货的商品可以发货");
                }
            }

            try
            {
                order.OrderStatus = OrderStatus.WaitingReceiptProduct;
                var deliverRes = Resolve<IOrderService>().Update(order);

                if (deliverRes)
                {
                    var store = Resolve<IStoreService>().GetSingle(order.StoreId);

                    //添加订单操作记录
                    var orderAction = new OrderAction
                    {
                        OrderId = order.Id,
                        OrderActionType = OrderActionType.AdminDelivery,
                        ActionUserId = order.UserId,
                        Intro = $"{store.Name}已发货"
                    };
                    var rsAddAction = Resolve<IOrderActionService>().Add(orderAction);

                    order.OrderExtension = order.Extension.ToObject<OrderExtension>();
                    // 存在关联TenantOrder才执行.
                    if (rsAddAction && order?.OrderExtension?.TenantOrderInfo?.OrderId > 0) {
                        DeliveryToTenant(model);
                    }
                }
            }
            catch (Exception e)
            {
                return ServiceResult.FailedWithMessage(e.ToString());
            }

            return ServiceResult.Success;
        }

        /// <summary>
        ///     将发货信息同步到租户上
        ///     发货成功时调用，每发货调用一次
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ServiceResult DeliveryToTenant(OrderInput model)
        {
            if (TenantContext.CurrentTenant == "master")
            {
                var order = Resolve<IOrderService>().GetSingle(model.OrderID);
                if (order != null)
                {
                    order.OrderExtension = order.Extension.ToObject<OrderExtension>();

                    var tenant = Resolve<ITenantService>().GetSingle(r => r.UserId == order.UserId);
                    if (tenant != null)
                    {
                        var apiUrl = tenant.ServiceUrl + "/Api/Order/Delivery";
                        model.OrderID = order.OrderExtension.TenantOrderInfo.OrderId;
                        // 通过Api请求

                        var dicHeader = new Dictionary<string, string>
                        {
                            {"zk-tenant", tenant.Sign}
                        };

                        apiUrl.Post(model.ToJsons(), dicHeader);
                    }
                }
            }

            return ServiceResult.Success;
        }

        /// <summary>
        ///     增加收货地址
        ///     只增加收货地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ServiceResult AddExpress(Deliver model)
        {
            var order = Resolve<IOrderService>().GetSingle(u => u.Id == model.OrderId);
            if (order == null) {
                return ServiceResult.FailedMessage("订单有误");
            }
            //if (order.OrderStatus != OrderStatus.WaitingSellerSendGoods)
            //{
            //    return ServiceResult.FailedWithMessage("只有待发货的商品可以发货");
            //}
            try
            {
                Resolve<IDeliverService>().Add(model);
            }
            catch (Exception e)
            {
                return ServiceResult.FailedWithMessage(e.ToString());
            }

            return ServiceResult.Success;
        }

        /// <summary>
        ///     获取快递公司
        /// </summary>
        /// <returns></returns>
        public string GetExpress()
        {
            var fileDir = Environment.CurrentDirectory;
            var result = File.ReadAllText(fileDir + @"\wwwroot\static\js\express.js");
            return result;
        }

        /// <summary>
        ///     获取快递列表
        /// </summary>
        /// <param name="orderId">订单id</param>
        /// <returns></returns>
        public IList<Deliver> GetExpressList(long orderId)
        {
            var model = Resolve<IDeliverService>().GetList(u => u.OrderId == orderId);
            return model;
        }

        /// <summary>
        ///     获取物流信息
        /// </summary>
        /// <param name="expressId">快递Id</param>
        /// <returns></returns>
        public string GetExpressInfo(string expressId)
        {
            var model = Resolve<IDeliverService>().GetSingle(u => u.Id == expressId.ToObjectId());
            //如果是其他物流则直接返回查询失败
            if (model.ExpressNo.Equal("other")) {
                return "{\"reason\":\"其他物流不提供查询!\",\"result\":{},\"error_code\":1}";
            }
            //e825b7404098dbae15c164a7ab7ced2f 李红建
            //1e2e7e4a7afef7f2cc1dbb3795fc3d33 贾恒
            var url =
                $"http://v.juhe.cn/exp/index?key=1e2e7e4a7afef7f2cc1dbb3795fc3d33&com={model.ExpressNo}&no={model.ExpressNum}";
            var httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            HttpWebResponse httpResponse = null;

            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }

            var st = httpResponse.GetResponseStream();
            var reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            var result = reader.ReadToEnd();

            return result;
        }

        #endregion 快递相关
    }
}