using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Offline.Merchants.Domain.Services;
using Alabo.App.Offline.Order.Domain.Dtos;
using Alabo.App.Offline.Order.Domain.Entities;
using Alabo.App.Offline.Order.Domain.Entities.Extensions;
using Alabo.App.Offline.Order.Domain.Enums;
using Alabo.App.Offline.Order.Domain.Repositories;
using Alabo.App.Offline.Order.ViewModels;
using Alabo.App.Offline.Product.Domain.Services;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Linq.Dynamic;
using Alabo.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Core.WebApis.Service;

namespace Alabo.App.Offline.Order.Domain.Services {

    /// <summary>
    /// MerchantOrderService
    /// </summary>
    public class MerchantOrderService : ServiceBase<MerchantOrder, long>, IMerchantOrderService {

        public MerchantOrderService(IUnitOfWork unitOfWork, IRepository<MerchantOrder, long> repository)
            : base(unitOfWork, repository) {
        }

        /// <summary>
        /// Builder order info and calculator price
        /// </summary>
        /// <param name="buyInfoInput"></param>
        public Tuple<ServiceResult, MerchantCartOutput> BuyInfo(MerchantBuyInfoInput buyInfo) {
            //user
            var result = new MerchantCartOutput();
            var user = Resolve<IUserService>().GetSingle(buyInfo.UserId);
            if (user == null || user.Status != Status.Normal) {
                return Tuple.Create(ServiceResult.FailedWithMessage("您的输入的账户不存在，或者状态不正常"), result);
            }
            //get merchant store
            //var merchantStore = Resolve<IMerchantStoreService>().GetSingle(s => s.Id == buyInfo.MerchantStoreId.ToObjectId());
            //if (merchantStore == null)
            //{
            //    return Tuple.Create(ServiceResult.FailedWithMessage("当前店铺不存在"), result);
            //}
            //get cart from database
            var cartIds = buyInfo.ProductItems.Select(c => c.Id.ToObjectId()).ToList();
            var carts = Resolve<IMerchantCartService>().GetCart(cartIds);
            if (carts.Count <= 0) {
                return Tuple.Create(ServiceResult.FailedWithMessage("购物车为空"), result);
            }

            return CountPrice(carts);
        }

        /// <summary>
        /// Calculator price
        /// </summary>
        /// <param name="cartProducts"></param>
        /// <returns></returns>
        public Tuple<ServiceResult, MerchantCartOutput> CountPrice(List<MerchantCartViewModel> cartProducts) {
            //check
            var output = new MerchantCartOutput();
            if (cartProducts.Count <= 0) {
                return Tuple.Create(ServiceResult.FailedWithMessage("购物车数据异常"), output);
            }
            //product
            var result = ServiceResult.Success;
            var productService = Resolve<IMerchantProductService>();
            foreach (var cart in cartProducts) {
                if (cart.Count <= 0) {
                    result = ServiceResult.FailedWithMessage($"商品:{cart.ProductName}，商品数据异常");
                    break;
                }
                var product = productService.GetSingle(p => p.Id == cart.MerchantProductId.ToObjectId());
                if (product == null) {
                    result = ServiceResult.FailedWithMessage($"商品:{cart.ProductName}，不存在");
                    break;
                }
                //stock
                var sku = product.Skus?.Find(s => s.SkuId == cart.SkuId);
                if (sku == null) {
                    result = ServiceResult.FailedWithMessage($"商品:{cart.ProductName}，Sku不存在");
                    break;
                }
                if (cart.Count > sku.Stock) {
                    result = ServiceResult.FailedWithMessage($"商品:{cart.ProductName}，Sku:{sku.Name}，购买数量大于商品库存数量");
                    break;
                }
                cart.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(product.ThumbnailUrl);
                cart.ProductAmount = sku.Price * cart.Count;
                output.TotalCount += cart.Count;
                output.TotalAmount += cart.ProductAmount;
                output.Products.Add(cart);
            }

            return Tuple.Create(result, output);
        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="orderBuyInput"></param>
        public Tuple<ServiceResult, MerchantOrderBuyOutput> Buy(MerchantOrderBuyInput orderBuyInput) {
            //check
            var orderBuyOutput = new MerchantOrderBuyOutput();
            var user = Resolve<IUserService>().GetNomarlUser(orderBuyInput.UserId);
            if (user == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("用户不存在，或状态不正常"), orderBuyOutput);
            }
            if (orderBuyInput.Products.Count <= 0) {
                return Tuple.Create(ServiceResult.FailedWithMessage("提交商品数据异常"), orderBuyOutput);
            }
            //get cart from database
            var cartIds = orderBuyInput.Products.Select(c => c.Id.ToObjectId()).ToList();
            var carts = Resolve<IMerchantCartService>().GetCart(cartIds);
            if (carts.Count <= 0) {
                return Tuple.Create(ServiceResult.FailedWithMessage("提交商品数据异常"), orderBuyOutput);
            }

            //Calculator price
            var priceResult = CountPrice(carts);
            if (!priceResult.Item1.Succeeded) {
                return Tuple.Create(priceResult.Item1, orderBuyOutput);
            }
            var orderPrice = priceResult.Item2;

            //check input data
            if (orderBuyInput.TotalAmount != orderPrice.TotalAmount) {
                return Tuple.Create(ServiceResult.FailedWithMessage("商品价格计算有误"), orderBuyOutput);
            }

            if (orderBuyInput.TotalCount != orderPrice.TotalCount) {
                return Tuple.Create(ServiceResult.FailedWithMessage("订单商品计算有误"), orderBuyOutput);
            }

            //save
            var result = ServiceResult.Success;
            var context = Repository<IMerchantOrderRepository>().RepositoryContext;
            try {
                context.BeginTransaction();

                //order
                var order = new MerchantOrder {
                    UserId = user.Id,
                    MerchantStoreId = orderBuyInput.MerchantStoreId,
                    OrderStatus = MerchantOrderStatus.WaitingBuyerPay,
                    OrderType = MerchantOrderType.Normal,
                    TotalAmount = orderPrice.TotalAmount,
                    TotalCount = orderPrice.TotalCount,
                    PaymentAmount = orderPrice.TotalAmount
                };
                order.MerchantOrderExtension = new MerchantOrderExtension {
                    OrderAmount = new MerchantOrderAmount {
                        ReceivedAmount = order.PaymentAmount,
                        TotalAmount = orderPrice.TotalAmount,
                        FeeAmount = orderPrice.FeeAmount,
                    },
                    MerchantProducts = orderPrice.Products
                };
                order.Extension = order.MerchantOrderExtension.ToJson();
                Resolve<IMerchantOrderService>().Add(order);

                //order product
                var productService = Resolve<IMerchantProductService>();
                var orderProducts = new List<MerchantOrderProduct>();
                carts.ForEach(product => {
                    //add order product
                    var orderProduct = new MerchantOrderProduct {
                        MerchantStoreId = orderBuyInput.MerchantStoreId,
                        OrderId = order.Id,
                        MerchantProductId = product.MerchantProductId,
                        SkuId = product.SkuId,
                        Count = product.Count,
                        Amount = product.Price,
                        PaymentAmount = product.ProductAmount
                    };
                    orderProducts.Add(orderProduct);

                    //stock
                    var productData = productService.GetSingle(p => p.Id == product.MerchantProductId.ToObjectId());
                    if (productData != null) {
                        var sku = productData.Skus?.Find(s => s.SkuId == product.SkuId);
                        if (sku != null) {
                            sku.Stock -= product.Count;
                        }
                        productData.SoldCount += product.Count;
                        productService.Update(productData);
                    }

                    //delete from cart
                    Resolve<IMerchantCartService>().Delete(product.Id.ToObjectId());
                });
                Resolve<IMerchantOrderProductService>().AddMany(orderProducts);
                //TODO 2019年9月24日 重构 线下订单交易
                //order for pay
                //var payOrder = new Shop.Order.Domain.Entities.Order {
                //    Id = order.Id,
                //    PaymentAmount = order.PaymentAmount,
                //    TotalAmount = order.TotalAmount
                //};
                ////pay record
                //var singlePayInput = new SinglePayInput {
                //    Orders = new List<Shop.Order.Domain.Entities.Order> { payOrder },
                //    User = user,
                //    ExcecuteSqlList = new BaseServiceMethod {
                //        Method = "ExcecuteSqlList",
                //        ServiceName = typeof(IMerchantOrderService).Name,
                //        Parameter = order.Id
                //    },
                //    BuyerCount = 1,
                //    CheckoutType = CheckoutType.Offline
                //};
                //var payResult = Resolve<IOrderAdminService>().AddSinglePay(singlePayInput);
                //if (!payResult.Item1.Succeeded) {
                //    context.RollbackTransaction();
                //    return Tuple.Create(payResult.Item1, orderBuyOutput);
                //}

                ////update order pay id
                //Resolve<IMerchantOrderService>().Update(r => {
                //    r.PayId = payResult.Item2.Id;
                //}, e => e.Id == order.Id);

                ////add order action
                //Resolve<IOrderActionService>().Add(payOrder, user, OrderActionType.OfflineUserCreateOrder);

                //output
                //  orderBuyOutput.PayAmount = payResult.Item2.Amount;
                //  orderBuyOutput.PayId = payResult.Item2.Id;
                orderBuyOutput.OrderIds = new List<long> { order.Id };

                context.SaveChanges();
                context.CommitTransaction();
            } catch (Exception ex) {
                context.RollbackTransaction();
                result = ServiceResult.FailedWithMessage(ex.Message);
            } finally {
                context.DisposeTransaction();
            }

            return Tuple.Create(result, orderBuyOutput);
        }

        /// <summary>
        /// Excecute sql
        /// </summary>
        /// <param name="entityIdList"></param>
        /// <returns></returns>
        public List<string> ExcecuteSqlList(List<object> entityIdList) {
            var sqlList = new List<string>();
            var orders = Resolve<IMerchantOrderService>().GetList(o => entityIdList.Contains(o.Id)).ToList();
            var payIds = orders.Select(o => o.PayId).ToList();
            var pays = Resolve<IPayService>().GetList(p => payIds.Contains(p.Id)).ToList();
            orders.ForEach(order => {
                var pay = pays.Find(o => o.Id == order.PayId);
                if (pay == null) {
                    return;
                }
                sqlList.Add($"update Offline_MerchantOrder set OrderStatus={(int)MerchantOrderStatus.Success},PayId='{pay.Id}'  where OrderStatus={(int)MerchantOrderStatus.WaitingBuyerPay} and id in  ({entityIdList.ToSqlString()})");
                // TODO 线下订单支付关闭
                // sqlList.Add($"INSERT INTO [dbo].[Shop_OrderAction] ([OrderId] ,[ActionUserId] ,[Intro]  ,[Extensions]  ,[CreateTime],[OrderActionType]) VALUES({order.Id},{pay.UserId},'会员支付订单，支付方式为{pay.PayType.GetDisplayName()},支付现金金额为{pay.Amount}','','{DateTime.Now}',{(int)OrderActionType.OfflineUserPayOrder})");
            });

            return sqlList;
        }

        /// <summary>
        /// Get order list for mobile
        /// </summary>
        /// <param name="orderInput"></param>
        public PagedList<MerchantOrderList> GetOrderList(MerchantOrderListInput orderInput) {
            //query express
            var query = new ExpressionQuery<MerchantOrder>();
            if (orderInput.OrderStatus > 0) {
                query.And(e => e.OrderStatus == orderInput.OrderStatus);
            }

            if (!string.IsNullOrWhiteSpace(orderInput.MerchantStoreId)) {
                query.And(e => e.MerchantStoreId == orderInput.MerchantStoreId);
            }
            if (orderInput.UserId > 0) {
                query.And(e => e.UserId == orderInput.UserId);
            }
            query.PageIndex = (int)orderInput.PageIndex;
            query.PageSize = (int)orderInput.PageSize;
            query.EnablePaging = true;
            query.OrderByDescending(e => e.Id);
            var orders = Resolve<IMerchantOrderService>().GetPagedList(query);
            if (orders.Count < 0) {
                return new PagedList<MerchantOrderList>();
            }
            //all stores
            var merchantStores = Resolve<IMerchantStoreService>().GetList().ToList();
            //query
            var result = new List<MerchantOrderList>();
            var apiService = Resolve<IApiService>();
            orders.ForEach(item => {
                var store = merchantStores.Find(s => s.Id == item.MerchantStoreId.ToObjectId());
                if (store == null) {
                    return;
                }
                var extension = item.Extension.DeserializeJson<MerchantOrderExtension>();
                var temp = new MerchantOrderList();
                temp = AutoMapping.SetValue(item, temp);
                temp.MerchantOrderStatus = item.OrderStatus;
                temp.MerchantStoreName = store.Name;
                temp.ThumbnailUrl = apiService.ApiImageUrl(store.Logo);
                temp.Products = extension?.MerchantProducts;
                temp.Products?.ForEach(product => {
                    product.ThumbnailUrl = apiService.ApiImageUrl(product.ThumbnailUrl);
                });
                result.Add(temp);
            });

            return PagedList<MerchantOrderList>.Create(result, orders.RecordCount, orders.PageSize, orders.PageIndex);
        }

        /// <summary>
        /// Get order single
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Tuple<ServiceResult, MerchantOrderList> GetOrderSingle(long id, long userId) {
            var order = GetSingle(r => r.Id == id && r.UserId == userId);
            if (order == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("订单不存在"), new MerchantOrderList());
            }
            var orderDetail = AutoMapping.SetValue<MerchantOrderList>(order);
            //user
            var user = Resolve<IUserService>().GetSingle(order.UserId);
            if (user == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("用户不存在"), orderDetail);
            }
            orderDetail.MerchantOrderStatus = order.OrderStatus;
            orderDetail.CreateTime = order.CreateTime.ToDateTimeString();
            orderDetail.BuyUser = AutoMapping.SetValue<UserOutput>(user);

            var pay = Resolve<IPayService>().GetSingle(order.PayId);
            if (pay != null) {
                orderDetail.PayTime = pay.ResponseTime.ToDateTimeString();
                orderDetail.PaymentType = pay.PayType.GetDisplayName();
            }
            order.MerchantOrderExtension = order.Extension.DeserializeJson<MerchantOrderExtension>();
            if (order.MerchantOrderExtension != null) {
                orderDetail.Products = order.MerchantOrderExtension.MerchantProducts;
                orderDetail.Products.ForEach(product => {
                    product.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(product.ThumbnailUrl);
                });
            }
            var store = Resolve<IMerchantStoreService>().GetSingle(s => s.Id == order.MerchantStoreId.ToObjectId());
            if (store != null) {
                orderDetail.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(orderDetail.ThumbnailUrl);
                orderDetail.MerchantStoreName = store.Name;
            }

            return Tuple.Create(ServiceResult.Success, orderDetail);
        }

        /// <summary>
        /// Get order single
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Tuple<ServiceResult, MerchantOrderList> GetOrderSingle(long id) {
            var order = GetSingle(r => r.Id == id);
            if (order == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("订单不存在"), new MerchantOrderList());
            }
            var orderDetail = AutoMapping.SetValue<MerchantOrderList>(order);
            //user
            var user = Resolve<IUserService>().GetSingle(order.UserId);
            if (user == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("用户不存在"), orderDetail);
            }
            orderDetail.MerchantOrderStatus = order.OrderStatus;
            orderDetail.CreateTime = order.CreateTime.ToDateTimeString();
            orderDetail.BuyUser = AutoMapping.SetValue<UserOutput>(user);

            var pay = Resolve<IPayService>().GetSingle(order.PayId);
            if (pay != null) {
                orderDetail.PayTime = pay.ResponseTime.ToDateTimeString();
                orderDetail.PaymentType = pay.PayType.GetDisplayName();
            }
            order.MerchantOrderExtension = order.Extension.DeserializeJson<MerchantOrderExtension>();
            if (order.MerchantOrderExtension != null) {
                orderDetail.Products = order.MerchantOrderExtension.MerchantProducts;
                orderDetail.Products.ForEach(product => {
                    product.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(product.ThumbnailUrl);
                });
            }
            var store = Resolve<IMerchantStoreService>().GetSingle(s => s.Id == order.MerchantStoreId.ToObjectId());
            if (store != null) {
                orderDetail.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(orderDetail.ThumbnailUrl);
                orderDetail.MerchantStoreName = store.Name;
            }

            return Tuple.Create(ServiceResult.Success, orderDetail);
        }
    }
}