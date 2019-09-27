using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Asset.Pays.Domain.Services;
using Alabo.App.Asset.Pays.Dtos;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.Address.Domain.Entities;
using Alabo.Framework.Basic.Address.Domain.Services;
using Alabo.Framework.Basic.Regions.Domain.Services;
using Alabo.Helpers;
using Alabo.Industry.Shop.Carts.Domain.Services;
using Alabo.Industry.Shop.OrderActions.Domain.Entities;
using Alabo.Industry.Shop.OrderActions.Domain.Enums;
using Alabo.Industry.Shop.OrderActions.Domain.Repositories;
using Alabo.Industry.Shop.OrderDeliveries.Domain.Entities;
using Alabo.Industry.Shop.OrderDeliveries.Domain.Entities.Extensions;
using Alabo.Industry.Shop.OrderDeliveries.Domain.Services;
using Alabo.Industry.Shop.Orders;
using Alabo.Industry.Shop.Orders.Domain.Entities;
using Alabo.Industry.Shop.Orders.Domain.Entities.Extensions;
using Alabo.Industry.Shop.Orders.Domain.Enums;
using Alabo.Industry.Shop.Orders.Domain.Repositories;
using Alabo.Industry.Shop.Orders.Domain.Services;
using Alabo.Industry.Shop.Orders.Dtos;
using Alabo.Industry.Shop.Orders.ViewModels;
using Alabo.Industry.Shop.Orders.ViewModels.OrderEdit;
using Alabo.Mapping;
using Alabo.Users.Entities;
using Microsoft.AspNetCore.Http;

namespace Alabo.Industry.Shop.OrderActions.Domain.Services
{
    /// <summary>
    ///     管理员操作
    /// </summary>
    public class OrderActionService : ServiceBase<OrderAction, long>, IOrderActionService
    {
        public OrderActionService(IUnitOfWork unitOfWork, IRepository<OrderAction, long> repository) : base(unitOfWork,
            repository)
        {
        }

        #region 管理员代付

        /// <summary>
        ///     管理员代付
        /// </summary>
        /// <param name="httpContext"></param>
        public ServiceResult Pay(DefaultHttpContext httpContext)
        {
            var orderEditPay = AutoMapping.SetValue<OrderEditPay>(httpContext);
            var user = Resolve<IUserService>().GetUserDetail(orderEditPay.UserId);
            if (user.Detail.PayPassword != orderEditPay.PayPassword.ToMd5HashString())
                return ServiceResult.FailedWithMessage("支付密码不正确");

            var order = Resolve<IOrderService>().GetSingle(r => r.Id == orderEditPay.OrderId);
            if (order == null) return ServiceResult.FailedWithMessage("订单不存在");

            if (order.OrderStatus != OrderStatus.WaitingBuyerPay)
                return ServiceResult.FailedWithMessage("订单已付款或关闭，请刷新");

            IList<Order> orderList = new List<Order>
            {
                order
            };

            var reduceMoneys = new List<OrderMoneyItem>();
            order.OrderExtension.ReduceAmounts.Foreach(r =>
            {
                reduceMoneys.Add(new OrderMoneyItem
                {
                    MoneyId = r.MoneyTypeId,
                    MaxPayPrice = r.ForCashAmount
                });
            });
            var singlePayInput = new SinglePayInput
            {
                Orders = orderList,
                User = user,
                ReduceMoneys = reduceMoneys,
                IsAdminPay = true
            };
            var payResult = Resolve<IOrderAdminService>().AddSinglePay(singlePayInput);
            if (!payResult.Item1.Succeeded) return ServiceResult.FailedWithMessage(payResult.Item1.ToString());

            var payInput = AutoMapping.SetValue<PayInput>(payResult.Item2);
            payInput.LoginUserId = user.Id;
            payInput.PayId = payResult.Item2.Id;

            var result = Resolve<IPayService>().Pay(payInput, httpContext);
            if (!result.Item1.Succeeded) return ServiceResult.FailedWithMessage(result.ToString());

            return ServiceResult.Success;
        }

        #endregion 管理员代付

        public OrderAction GetSingle(long id)
        {
            var find = Repository<IOrderActionRepository>().GetSingle(e => e.Id == id);
            return find;
        }

        public void AddOrUpdate(OrderAction model)
        {
            if (model.Id > 0)
                Repository<IOrderActionRepository>().UpdateSingle(model);
            else
                Repository<IOrderActionRepository>().AddSingle(model);
        }

        /// <summary>
        ///     Adds the specified order.
        ///     添加订单操作记录
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="user">The user.操作用户</param>
        /// <param name="actionType">Type of the action.</param>
        public void Add(Order order, User user, OrderActionType actionType)
        {
            var orderAction = new OrderAction
            {
                OrderId = order.Id,
                OrderActionType = actionType,
                ActionUserId = user.Id
            };
            var allActionIntro = GetAllActionIntro();
            var actionTypeIntro = allActionIntro.FirstOrDefault(r => r.Key == actionType).Value;
            orderAction.Intro = actionTypeIntro.Replace("{OrderUserName}", user.GetUserName())
                .Replace("{AdminUserName}", user.GetUserName())
                .Replace("{OrderAmount}", order.TotalAmount.ToString())
                .Replace("{SellerUserName}", user.GetUserName())
                .Replace("{ExpressAmount}", order.OrderExtension.OrderAmount.ExpressAmount.ToString());
            Add(orderAction);
        }

        public IList<OrderActionTypeAttribute> GetAllOrderActionTypeAttribute()
        {
            var cacheKey = "AllOrderActionTypeAttribute";
            if (!ObjectCache.TryGet(cacheKey, out IList<OrderActionTypeAttribute> orderActionTypeAttributeList))
            {
                orderActionTypeAttributeList = new List<OrderActionTypeAttribute>();
                foreach (OrderActionType item in Enum.GetValues(typeof(OrderActionType)))
                {
                    var orderActionType = item.GetCustomAttr<OrderActionTypeAttribute>();
                    orderActionTypeAttributeList.Add(orderActionType);
                }

                ObjectCache.Set(cacheKey, orderActionTypeAttributeList);
            }

            return orderActionTypeAttributeList;
        }

        public PagedList<OrderToExcel> GetOrdersToExcel(object query)
        {
            var orderList = Ioc.Resolve<IOrderActionRepository>().GetOrderListToExcel();
            var model = PagedList<OrderToExcel>.Create(orderList, orderList.Count(), orderList.Count(), 1);
            return model;
        }

        /// <summary>
        ///     Gets all action intro.
        ///     获取所有的操作特性
        /// </summary>
        public Dictionary<OrderActionType, string> GetAllActionIntro()
        {
            var cacheKey = "AllOrderActionTypeIntro";
            if (!ObjectCache.TryGet(cacheKey, out Dictionary<OrderActionType, string> orderActionTypeIntro))
            {
                orderActionTypeIntro = new Dictionary<OrderActionType, string>();
                foreach (OrderActionType item in Enum.GetValues(typeof(OrderActionType)))
                {
                    var orderActionType = item.GetCustomAttr<OrderActionTypeAttribute>();
                    orderActionTypeIntro.Add(item, orderActionType.Intro);
                }

                ObjectCache.Set(cacheKey, orderActionTypeIntro);
            }

            return orderActionTypeIntro;
        }

        /// <summary>
        ///     删除购物车数据
        /// </summary>
        /// <param name="orderBuyInput"></param>
        public void DeleteCartBuyOrder(BuyInput orderBuyInput)
        {
            var storeList = new List<long>();
            var productSkus = new List<long>();
            orderBuyInput.StoreOrders = orderBuyInput.StoreOrderJson.Deserialize<StoreOrderItem>();
            orderBuyInput.StoreOrders.ToList().ForEach(e =>
            {
                storeList.Add(e.StoreId);
                productSkus.AddRange(e.ProductSkuItems.Select(a => a.ProductSkuId));
            });
            //find
            var cars = Resolve<ICartService>().GetList(e =>
                e.UserId == orderBuyInput.UserId && storeList.Contains(e.StoreId) &&
                productSkus.Contains(e.ProductSkuId)).ToList();
            cars.ForEach(item =>
            {
                item.Status = Status.Deleted;
                Resolve<ICartService>().Update(item);
            });
        }

        #region 发货

        /// <summary>
        ///     发货
        /// </summary>
        /// <param name="model"></param>
        public ServiceResult Delivery(OrderEditDelivery model)
        {
            var order = Resolve<IOrderService>().GetSingle(model.OrderId);
            if (order == null) return ServiceResult.FailedWithMessage("订单不存在");
            // 发货数量
            if (!model.DeliveryProducts.IsNullOrEmpty())
                model.DeliveryProductSkus = model.DeliveryProducts.Deserialize<OrderEditDeliveryProduct>();
            else
                return ServiceResult.FailedWithMessage("发货数据异常，请重新操作！");

            if (model.DeliveryProductSkus.Sum(e => e.Count) <= 0) return ServiceResult.FailedWithMessage("发货总数量不能为0");

            var orderDelivery = new OrderDelivery
            {
                TotalCount = model.DeliveryProductSkus.Sum(e => e.Count),
                StoreId = order.StoreId,
                UserId = model.AdminUserId,
                ExpressGuid = model.ExpressGuid,
                ExpressNumber = model.ExpressNumber,
                OrderId = model.OrderId
            };
            //  发货快照
            var AdminUser = Resolve<IUserService>().GetSingle(model.AdminUserId);
            orderDelivery.OrderDeliveryExtension = new OrderDeliveryExtension
            {
                Order = order,
                AdminUser = AdminUser
            };
            // 订单状态判定
            var Judgement = true;

            //查看订单历史发货记录
            var Deliverys = Resolve<IOrderDeliveryService>().GetOrderDeliveries(order.Id);
            IList<ProductDeliveryInfo> productDeliveryInfos = new List<ProductDeliveryInfo>();
            if (Deliverys.Count > 0)
            {
                foreach (var item in Deliverys)
                    productDeliveryInfos.AddRange(item.OrderDeliveryExtension.ProductDeliveryInfo);
                // 已发货数量
                var Records = from ProductDeliveryInfo p in productDeliveryInfos
                    group p by p.ProductSkuId
                    into g
                    select new {skuId = g.Key, Count = g.Sum(e => e.Count)};

                foreach (var sku in model.DeliveryProductSkus)
                {
                    var item = order.OrderExtension.ProductSkuItems.SingleOrDefault(e => e.ProductSkuId == sku.SkuId);
                    var record = Records.SingleOrDefault(e => e.skuId == sku.SkuId);
                    if (record == null) record = new {skuId = sku.SkuId, Count = 0L};

                    var productDeliveryInfo = AutoMapping.SetValue<ProductDeliveryInfo>(item);
                    productDeliveryInfo.Count = sku.Count;
                    if (sku.Count + record.Count > item.BuyCount)
                        return ServiceResult.FailedWithMessage($"{item.Bn}{item.PropertyValueDesc}的发货数量不能超过购买数量");

                    if (sku.Count + record.Count != item.BuyCount) Judgement = false;

                    orderDelivery.OrderDeliveryExtension.ProductDeliveryInfo.Add(productDeliveryInfo);
                }
            }
            else
            {
                foreach (var sku in model.DeliveryProductSkus)
                {
                    var item = order.OrderExtension.ProductSkuItems.SingleOrDefault(e => e.ProductSkuId == sku.SkuId);
                    var productDeliveryInfo = AutoMapping.SetValue<ProductDeliveryInfo>(item);
                    productDeliveryInfo.Count = sku.Count;
                    if (sku.Count > item.BuyCount)
                        return ServiceResult.FailedWithMessage($"{item.Bn}{item.PropertyValueDesc}的发货数量不能超过购买数量");

                    if (sku.Count != item.BuyCount) Judgement = false;

                    orderDelivery.OrderDeliveryExtension.ProductDeliveryInfo.Add(productDeliveryInfo);
                }
            }

            orderDelivery.Extension = orderDelivery.OrderDeliveryExtension.ToJson();
            if (Judgement)
                order.OrderStatus = OrderStatus.WaitingReceiptProduct;
            else
                order.OrderStatus = OrderStatus.WaitingSellerSendGoods;

            var context = Repository<IOrderRepository>().RepositoryContext;
            context.BeginTransaction();
            try
            {
                Resolve<IOrderDeliveryService>().Add(orderDelivery);
                //添加操作记录
                Add(order, AdminUser, OrderActionType.AdminDelivery);
                //修改订单状态
                Resolve<IOrderService>().Update(order);
                context.SaveChanges();
                context.CommitTransaction();
                return ServiceResult.Success;
            }
            catch (Exception ex)
            {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
            }
            finally
            {
                context.DisposeTransaction();
            }
        }

        /// <summary>
        ///     发货
        /// </summary>
        /// <param name="httpContext"></param>
        public ServiceResult Delivery(DefaultHttpContext httpContext)
        {
            var model = AutoMapping.SetValue<OrderEditDelivery>(httpContext);
            return Delivery(model);
        }

        #endregion 发货

        #region 管理员备忘

        /// <summary>
        ///     备注 管理员备忘
        /// </summary>
        /// <param name="httpContext"></param>
        public ServiceResult Remark(DefaultHttpContext httpContext)
        {
            var orderRemark = AutoMapping.SetValue<OrderRemark>(httpContext);
            var orderId = httpContext.Request.Form["OrderId"].ConvertToLong();
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == orderId);

            if (order == null) return ServiceResult.FailedWithMessage("订单不存在");

            if (order.OrderExtension.OrderRemark == null) order.OrderExtension.OrderRemark = new OrderRemark();

            order.OrderExtension.OrderRemark.PlatplatformRemark = orderRemark.PlatplatformRemark;
            order.Extension = order.OrderExtension.ToJson();
            var result = Resolve<IOrderService>().Update(order);
            if (result) return ServiceResult.Success;

            return ServiceResult.FailedWithMessage("失败");
        }

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="httpContext"></param>
        public ServiceResult Delete(DefaultHttpContext httpContext)
        {
            var orderId = httpContext.Request.Form["OrderId"].ConvertToLong();
            var payPassword = httpContext.Request.Form["PayPassword"].ToString();
            var UserId = httpContext.Request.Form["UserId"].ConvertToLong();
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == orderId);
            if (order == null) return ServiceResult.FailedWithMessage("订单不存在");

            var user = Resolve<IUserService>().GetUserDetail(UserId);
            if (user.Detail.PayPassword != payPassword.ToMd5HashString())
                return ServiceResult.FailedWithMessage("支付密码不正确");

            var context = Repository<IOrderRepository>().RepositoryContext;
            context.BeginTransaction();
            try
            {
                Resolve<IOrderService>().Delete(order.Id);
                //添加操作记录
                Add(order, user, OrderActionType.AdminDelete);
                context.SaveChanges();
                context.CommitTransaction();
                return ServiceResult.Success;
            }
            catch (Exception ex)
            {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
            }
            finally
            {
                context.DisposeTransaction();
            }
        }

        /// <summary>
        ///     删除订单(03.31 新用途)
        /// </summary>
        /// <param name="modelIn"></param>
        /// <returns></returns>
        public ServiceResult Delete(OrderActionViewIn modelIn)
        {
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == modelIn.OrderId);
            if (order == null) return ServiceResult.FailedWithMessage("订单不存在");

            var user = Resolve<IUserService>().GetUserDetail(modelIn.UserId);
            if (user.Detail.PayPassword != modelIn.PayPassword.ToMd5HashString())
                return ServiceResult.FailedWithMessage("支付密码不正确");

            var context = Repository<IOrderRepository>().RepositoryContext;
            context.BeginTransaction();
            try
            {
                Resolve<IOrderService>().Delete(order.Id);
                //添加操作记录
                Add(order, user, OrderActionType.AdminDelete);
                context.SaveChanges();
                context.CommitTransaction();
                return ServiceResult.Success;
            }
            catch (Exception ex)
            {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
            }
            finally
            {
                context.DisposeTransaction();
            }
        }

        /// <summary>
        ///     关闭
        /// </summary>
        /// <param name="httpContext"></param>
        public ServiceResult Cancle(DefaultHttpContext httpContext)
        {
            var orderId = httpContext.Request.Form["OrderId"].ConvertToLong();
            var payPassword = httpContext.Request.Form["PayPassword"].ToString();
            var UserId = httpContext.Request.Form["UserId"].ConvertToLong();
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == orderId);
            if (order == null) return ServiceResult.FailedWithMessage("订单不存在");

            var user = Resolve<IUserService>().GetUserDetail(UserId);
            if (user.Detail.PayPassword != payPassword.ToMd5HashString())
                return ServiceResult.FailedWithMessage("支付密码不正确");

            order.OrderStatus = OrderStatus.Closed;
            var context = Repository<IOrderRepository>().RepositoryContext;
            context.BeginTransaction();
            try
            {
                Resolve<IOrderService>().Update(order);
                //添加操作记录
                Add(order, user, OrderActionType.AdminClose);
                context.SaveChanges();
                context.CommitTransaction();
                return ServiceResult.Success;
            }
            catch (Exception ex)
            {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
            }
            finally
            {
                context.DisposeTransaction();
            }
        }

        /// <summary>
        ///     关闭 (2019.03.31 新用途)
        /// </summary>
        /// <param name="modelIn"></param>
        /// <returns></returns>
        public ServiceResult Cancel(OrderActionViewIn modelIn)
        {
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == modelIn.OrderId);
            if (order == null) return ServiceResult.FailedWithMessage("订单不存在");

            var user = Resolve<IUserService>().GetUserDetail(modelIn.UserId);
            if (user.Detail.PayPassword != modelIn.PayPassword.ToMd5HashString())
                return ServiceResult.FailedWithMessage("支付密码不正确");

            order.OrderStatus = OrderStatus.Closed;
            var context = Repository<IOrderRepository>().RepositoryContext;
            context.BeginTransaction();
            try
            {
                Resolve<IOrderService>().Update(order);
                //添加操作记录
                Add(order, user, OrderActionType.AdminClose);
                context.SaveChanges();
                context.CommitTransaction();
                return ServiceResult.Success;
            }
            catch (Exception ex)
            {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
            }
            finally
            {
                context.DisposeTransaction();
            }
        }

        /// <summary>
        ///     卖家评价
        /// </summary>
        /// <param name="httpContext"></param>
        public ServiceResult Rate(DefaultHttpContext httpContext)
        {
            var orderRate = AutoMapping.SetValue<OrderRateInfo>(httpContext);
            var orderId = httpContext.Request.Form["OrderId"].ConvertToLong();
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == orderId);
            if (orderRate.Intro.IsNullOrEmpty()) return ServiceResult.FailedWithMessage("详情不能为空");

            if (order == null) return ServiceResult.FailedWithMessage("订单不存在");

            order.OrderExtension.OrderRate.SellerRate = orderRate;
            order.Extension = order.OrderExtension.ToJson();
            var result = Resolve<IOrderService>().Update(order);
            if (result) return ServiceResult.Success;

            return ServiceResult.FailedWithMessage("失败");
        }

        /// <summary>
        ///     卖家评价 (2019.03.31 新作用)
        /// </summary>
        /// <param name="orderRate"></param>
        public ServiceResult Rate(OrderRateInfo orderRate)
        {
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == orderRate.OrderId);
            if (orderRate.Intro.IsNullOrEmpty()) return ServiceResult.FailedWithMessage("详情不能为空");

            if (order == null) return ServiceResult.FailedWithMessage("订单不存在");

            order.OrderExtension.OrderRate.SellerRate = orderRate;
            order.Extension = order.OrderExtension.ToJson();
            var result = Resolve<IOrderService>().Update(order);
            if (result) return ServiceResult.Success;

            return ServiceResult.FailedWithMessage("失败");
        }

        /// <summary>
        ///     修改收货地址(2019.03.31 新用途)
        /// </summary>
        /// <param name="address"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public ServiceResult Address(UserAddress address, long orderId)
        {
            return AddressEdit(address, orderId);
        }

        /// <summary>
        ///     修改收货地址
        /// </summary>
        /// <param name="httpContext"></param>
        public ServiceResult Address(DefaultHttpContext httpContext)
        {
            var orderId = httpContext.Request.Form["OrderId"].ConvertToLong();
            var address = AutoMapping.SetValue<UserAddress>(httpContext);
            return AddressEdit(address, orderId);
        }

        private ServiceResult AddressEdit(UserAddress address, long orderId)
        {
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == orderId);

            if (order == null) return ServiceResult.FailedWithMessage("订单不存在");

            if (address.RegionId == 0) return ServiceResult.FailedWithMessage("县级不能为空！");

            if (address.Mobile.IsNullOrEmpty()) return ServiceResult.FailedWithMessage("收件人手机号不能为空！");

            if (address.Name.IsNullOrEmpty()) return ServiceResult.FailedWithMessage("收件人姓名不能为空！");

            if (order.OrderExtension.UserAddress == null)
                order.OrderExtension.OrderRemark = new OrderRemark();
            else address.Id = order.OrderExtension.UserAddress.Id;
            //address.LoginUserId = order.OrderExtension.UserAddress.LoginUserId;

            address.AddressDescription =
                $"{Resolve<IRegionService>().GetFullName(address.RegionId)} {address.Address} ";
            order.OrderExtension.UserAddress = address;
            order.Extension = order.OrderExtension.ToJson();
            var result = Resolve<IOrderService>().Update(order);
            if (result) return ServiceResult.Success;

            return ServiceResult.FailedWithMessage("失败");
        }

        /// <summary>
        /// </summary>
        /// <param name="httpContext"></param>
        public ServiceResult Message(DefaultHttpContext httpContext)
        {
            var orderMessage = AutoMapping.SetValue<OrderMessage>(httpContext);
            var orderId = httpContext.Request.Form["OrderId"].ConvertToLong();
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == orderId);

            if (order == null) return ServiceResult.FailedWithMessage("订单不存在");

            if (order.OrderExtension.OrderRemark == null) order.OrderExtension.OrderRemark = new OrderRemark();

            order.OrderExtension.Message.PlatplatformMessage = orderMessage.PlatplatformMessage;
            order.Extension = order.OrderExtension.ToJson();
            var result = Resolve<IOrderService>().Update(order);
            if (result) return ServiceResult.Success;

            return ServiceResult.FailedWithMessage("失败");
        }

        #endregion 管理员备忘
    }
}