using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Asset.Accounts.Domain.Services;
using Alabo.App.Asset.Coupons.Domain.Enums;
using Alabo.App.Asset.Coupons.Domain.Services;
using Alabo.Data.People.Stores.Domain.Entities;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.Address.Domain.Entities;
using Alabo.Framework.Basic.Address.Domain.Services;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Helpers;
using Alabo.Industry.Shop.Activitys.Domain.Entities;
using Alabo.Industry.Shop.Activitys.Domain.Entities.Extension;
using Alabo.Industry.Shop.Activitys.Domain.Enum;
using Alabo.Industry.Shop.Activitys.Domain.Services;
using Alabo.Industry.Shop.Activitys.Modules.GroupBuy.Model;
using Alabo.Industry.Shop.Deliveries.Domain.Entities;
using Alabo.Industry.Shop.Deliveries.Domain.Services;
using Alabo.Industry.Shop.Deliveries.Dtos;
using Alabo.Industry.Shop.OrderActions.Domain.Enums;
using Alabo.Industry.Shop.OrderActions.Domain.Services;
using Alabo.Industry.Shop.Orders.Domain.Configs;
using Alabo.Industry.Shop.Orders.Domain.Entities;
using Alabo.Industry.Shop.Orders.Domain.Entities.Extensions;
using Alabo.Industry.Shop.Orders.Domain.Enums;
using Alabo.Industry.Shop.Orders.Domain.Repositories;
using Alabo.Industry.Shop.Orders.Dtos;
using Alabo.Industry.Shop.Products.Domain.Configs;
using Alabo.Industry.Shop.Products.Domain.Services;
using Alabo.Industry.Shop.Products.Dtos;
using Alabo.Users.Entities;
using MongoDB.Bson;
using ZKCloud.Open.DynamicExpression;

namespace Alabo.Industry.Shop.Orders.Domain.Services
{
    /// <summary>
    ///     订单购物、订单价格计算相关的订单服务
    ///     会员订单业务，管理员订单业务，请不要写到此处
    /// </summary>
    public class OrderBuyServcie : ServiceBase, IOrderBuyServcie
    {
        public OrderBuyServcie(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region 订单购物

        /// <summary>
        ///     订单购买，包括立即购买、购物车购买
        ///     支持多种货币类型
        ///     整个系统使用该服务即可，是系统非常核心的业务服务，请谨慎维护
        ///     如果传入的商品有多个店铺时候，按店铺生成不同的订单
        ///     比如购物车中，选择了20个商家的商品，则生成20个订单
        ///     选择的商品中，有多少个店铺，就生成多少个订单
        /// </summary>
        /// <param name="orderBuyInput"></param>
        public Tuple<ServiceResult, BuyOutput> Buy(BuyInput orderBuyInput)
        {
            #region 安全验证

            var result = ServiceResult.Success;
            var BookingBuyOutput = new BuyOutput();
            var user = Resolve<IUserService>().GetNomarlUser(orderBuyInput.UserId);
            if (user == null) return Tuple.Create(ServiceResult.FailedWithMessage("用户不存在，或状态不正常"), BookingBuyOutput);
            user.Detail = null; // 减少体积保存
            user.Map = null;
            //实名认证判断
            var orderConfig = Resolve<IAutoConfigService>().GetValue<OrderConfig>();
            if (orderConfig.IsIdentity)
                if (!Resolve<IUserDetailService>().IsIdentity(user.Id))
                    return Tuple.Create(ServiceResult.FailedWithMessage("请先实名认证"), new BuyOutput());
            // 验证前台数据传入是否正确
            try
            {
                orderBuyInput.StoreOrders = orderBuyInput.StoreOrderJson.DeserializeJson<List<StoreOrderItem>>();
                if (orderBuyInput.StoreOrders == null)
                    return Tuple.Create(ServiceResult.FailedWithMessage("店铺数据传入不正确"), new BuyOutput());

                orderBuyInput.StoreOrders.ToList().ForEach(r =>
                {
                    if (r.ProductSkuItems == null || r.ProductSkuItems.Count == 0)
                        result = ServiceResult.FailedWithMessage("商品数据有误");
                });
            }
            catch
            {
                return Tuple.Create(ServiceResult.FailedWithMessage("商品数据序列化出错"), new BuyOutput());
            }

            // 验证前台数据传入是否正确 验证币种类型
            try
            {
                if (!orderBuyInput.ReduceMoneysJson.IsNullOrEmpty())
                    orderBuyInput.ReduceMoneys =
                        orderBuyInput.ReduceMoneysJson.DeserializeJson<List<KeyValuePair<Guid, decimal>>>();
            }
            catch
            {
                return Tuple.Create(ServiceResult.FailedWithMessage("使用资产序列化出错，使用资产传入错误"), new BuyOutput());
            }

            // 验证签名是否有数据，如果没有则表示订单已经不存在，或者被串改
            var cacheKey = $"OrderPrice_{orderBuyInput.Sign}";
            if (!ObjectCache.TryGet(cacheKey, out OrderPriceCache orderPriceCache))
                return Tuple.Create(ServiceResult.FailedWithMessage("签名错误，请刷新"), new BuyOutput());

            if (orderPriceCache.StoreProductSku.StoreItems.Count != orderBuyInput.StoreOrders.ToList().Count())
                return Tuple.Create(ServiceResult.FailedWithMessage("店铺数与配送方式数不比配"), new BuyOutput());
            // 缓存中的地址，与数据库中的真实地址对不，验证是否被串改
            var cacheAddress =
                orderPriceCache.UserAddresses.FirstOrDefault(r => r.Id == orderBuyInput.AddressId.ToObjectId());
            if (cacheAddress == null)
            {
                // 如果缓存不存在地址，则从数据库在读取一次，解决购物时，地址新增的情况
                cacheAddress = Resolve<IUserAddressService>()
                    .GetUserAddress(orderBuyInput.AddressId.ToObjectId(), orderBuyInput.UserId);
                if (cacheAddress == null)
                    return Tuple.Create(ServiceResult.FailedWithMessage("您选择的地址不存在"), new BuyOutput());
            }

            var address = Resolve<IUserAddressService>()
                .GetUserAddress(orderBuyInput.AddressId.ToObjectId(), orderBuyInput.UserId);
            if (address == null) return Tuple.Create(ServiceResult.FailedWithMessage("地址已被删掉"), new BuyOutput());

            if (cacheAddress.Province != address.Province || cacheAddress.City != address.City ||
                cacheAddress.RegionId != address.RegionId)
                return Tuple.Create(ServiceResult.FailedWithMessage("地址已被更改，请重新选择地址"), new BuyOutput());

            #endregion 安全验证

            // 店铺商品数据，重新构建 确保数据是最新的，防止后台管理员修改商品价格，或者库存不足的情况
            var latestStoreProductSku = BuyInputToStoreProductSku(orderBuyInput); // 最新的商品购物数据
            var userOrderInput = BuyInputToUserOrderInput(orderBuyInput);
            var userAddress = Resolve<IUserAddressService>().GetList(r => r.UserId == orderBuyInput.UserId);
            var priceSytles =
                Resolve<IAutoConfigService>().GetList<PriceStyleConfig>(r => r.Status == Status.Normal); // 所有商城模式
            var priceResult = CountPrice(ref latestStoreProductSku, userOrderInput, user, userAddress,
                latestStoreProductSku.AllowMoneys); // 重新计算价格
            var latestStoreOrderPrice = priceResult.Item2; // 最新的价格数据

            //set sku amount
            var skuItems = new List<ProductSkuItem>();
            latestStoreProductSku.StoreItems.ForEach(store => { skuItems.AddRange(store.ProductSkuItems); });
            orderBuyInput.StoreOrders.Foreach(order =>
            {
                order.ProductSkuItems.Foreach(sku =>
                {
                    var tempSku = skuItems.Find(s => s.ProductSkuId == sku.ProductSkuId);
                    if (tempSku != null) sku.Amount = tempSku.Price;
                });
            });

            #region 如果虚拟资产不为空

            var reduceMoney = 0.0m;
            if (orderBuyInput.ReduceMoneys != null)
            {
                if (latestStoreOrderPrice.ReduceMoneys.Count == orderBuyInput.ReduceMoneys.Count)
                {
                    orderBuyInput.ReduceMoneys.Foreach(t =>
                    {
                        var moneyTypeItem = Resolve<IAutoConfigService>().MoneyTypes()
                            .FirstOrDefault(e => e.Id == t.Key);
                        var orderMoneyItem = latestStoreOrderPrice.ReduceMoneys.FirstOrDefault(r => r.MoneyId == t.Key);
                        if (orderMoneyItem.MaxPayPrice == t.Value)
                            reduceMoney += t.Value * moneyTypeItem.RateFee; // 减少的人民币

                        if (t.Value != orderMoneyItem.MaxPayPrice) result = ServiceResult.FailedWithMessage("虚拟资产计算有误");
                    });
                    if (latestStoreOrderPrice.TotalAmount != latestStoreOrderPrice.ExpressAmount +
                        latestStoreOrderPrice.ProductAmount - reduceMoney +
                        latestStoreOrderPrice.FeeAmount)
                        return Tuple.Create(ServiceResult.FailedWithMessage("总价不登录商品总非+运费+非人民币资产+服务费"),
                            new BuyOutput());
                }
                else
                {
                    return Tuple.Create(ServiceResult.FailedWithMessage("非人民币资产扣除有误"), new BuyOutput());
                }
            }
            else
            {
                if (latestStoreOrderPrice.TotalAmount != latestStoreOrderPrice.ExpressAmount +
                    latestStoreOrderPrice.ProductAmount + latestStoreOrderPrice.FeeAmount)
                    return Tuple.Create(ServiceResult.FailedWithMessage("总价不登录商品总非+运费+服务费"), new BuyOutput());
            }

            #endregion 如果虚拟资产不为空

            #region 安全验证 客户端购买信息，价格等和服务器计算数据对比

            if (orderPriceCache.StoreProductSku.TotalAmount != latestStoreProductSku.TotalAmount)
                return Tuple.Create(ServiceResult.FailedWithMessage("商品价格计算有误"), new BuyOutput());

            if (orderPriceCache.StoreProductSku.TotalCount != latestStoreProductSku.TotalCount)
                return Tuple.Create(ServiceResult.FailedWithMessage("订单商品计算有误"), new BuyOutput());

            if (latestStoreOrderPrice.TotalAmount != orderBuyInput.TotalAmount)
                return Tuple.Create(ServiceResult.FailedWithMessage("订单总价格计算有误"), new BuyOutput());

            if (latestStoreOrderPrice.TotalAmount < latestStoreOrderPrice.ExpressAmount)
                return Tuple.Create(ServiceResult.FailedWithMessage("订单总价格不能小于运费"), new BuyOutput());

            if (latestStoreProductSku.TotalCount != orderBuyInput.TotalCount)
                return Tuple.Create(ServiceResult.FailedWithMessage("订单总数量计算有误"), new BuyOutput());

            foreach (var storeOrder in orderBuyInput.StoreOrders)
            {
                var storePriceItem =
                    latestStoreOrderPrice.StorePrices.FirstOrDefault(r => r.StoreId == storeOrder.StoreId);
                if (storePriceItem == null)
                    return Tuple.Create(ServiceResult.FailedWithMessage("订单数据传入有误"), new BuyOutput());

                if (storePriceItem.TotalAmount != storeOrder.TotalAmount)
                    return Tuple.Create(ServiceResult.FailedWithMessage("店铺总价格计算有误"), new BuyOutput());

                if (storePriceItem.ExpressAmount != storeOrder.ExpressAmount)
                    return Tuple.Create(ServiceResult.FailedWithMessage("店铺总运费计算有误"), new BuyOutput());

                var storeItem = latestStoreProductSku.StoreItems.FirstOrDefault(r => r.StoreId == storeOrder.StoreId);
                if (storeItem == null)
                    return Tuple.Create(ServiceResult.FailedWithMessage("订单数据传入有误"), new BuyOutput());

                if (storeItem.TotalCount != storeOrder.TotalCount)
                    return Tuple.Create(ServiceResult.FailedWithMessage("店铺数量计算有误"), new BuyOutput());

                if (storeItem.TotalAmount != storeOrder.ProductAmount)
                    return Tuple.Create(ServiceResult.FailedWithMessage("店铺商品总费用计算有误"), new BuyOutput());

                foreach (var skuItem in storeOrder.ProductSkuItems)
                {
                    // 逐一验证所有商品传递的价格模式
                    var priceConfig = priceSytles.FirstOrDefault(r => r.Id == skuItem.PriceStyleId);
                    if (priceConfig == null)
                        return Tuple.Create(ServiceResult.FailedWithMessage("商品Sku商品模式错误"), new BuyOutput());
                    // 数据库中的sku信息
                    // 逐一验证所有商品Sku的价格
                    var lasetSku = latestStoreProductSku.StoreItems.FirstOrDefault(r => r.StoreId == skuItem.StoreId)
                        .ProductSkuItems.FirstOrDefault(r => r.ProductSkuId == skuItem.ProductSkuId);
                    if (lasetSku.Price * skuItem.Count != skuItem.Amount * skuItem.Count)
                        return Tuple.Create(ServiceResult.FailedWithMessage("商品Sku价格计算错误"), new BuyOutput());
                }
            }

            #endregion 安全验证 客户端购买信息，价格等和服务器计算数据对比

            #region 验证人民币金额

            if (!orderBuyInput.PaymentAmount.EqualsDigits(latestStoreOrderPrice.TotalAmount))
                return Tuple.Create(ServiceResult.FailedWithMessage("人民币支付金额计算错误"), new BuyOutput());

            if (!orderBuyInput.PaymentAmount.EqualsDigits(
                latestStoreProductSku.TotalAmount - reduceMoney + latestStoreOrderPrice.ExpressAmount +
                latestStoreOrderPrice.FeeAmount))
                return Tuple.Create(ServiceResult.FailedWithMessage("人民币支付金额计算错误"), new BuyOutput());

            if (!orderBuyInput.PaymentAmount.EqualsDigits(orderBuyInput.StoreOrders.Sum(r => r.ProductAmount) +
                                                          orderBuyInput.StoreOrders.Sum(r => r.ExpressAmount) -
                                                          reduceMoney + latestStoreOrderPrice.FeeAmount))
                return Tuple.Create(ServiceResult.FailedWithMessage("人民币支付金额计算错误"), new BuyOutput());
            //如果验证不成功，则直接返回错误
            if (result != ServiceResult.Success) return Tuple.Create(result, new BuyOutput());

            #endregion 验证人民币金额

            #region 活动限购

            var activityService = Resolve<IActivityApiService>();
            //预售
            var checkResult = activityService.CheckPreSellActivity(orderBuyInput.StoreOrders);
            if (!checkResult.Succeeded) return Tuple.Create(checkResult, new BuyOutput());
            //限时购
            checkResult = activityService.CheckTimeLimitBuyActivity(orderBuyInput.StoreOrders);
            if (!checkResult.Succeeded) return Tuple.Create(checkResult, new BuyOutput());
            var timeLimitActivities = new List<Activity>();
            if (checkResult.ReturnObject != null) timeLimitActivities = (List<Activity>)checkResult.ReturnObject;

            //购买权限
            checkResult = activityService.CheckBuyPermissionActivity(orderBuyInput.StoreOrders, user);
            if (!checkResult.Succeeded) return Tuple.Create(checkResult, new BuyOutput());

            #endregion 活动限购

            #region 拼团验证

            var activity = new Activity();
            var groupBuyActivity = new GroupBuyActivity(); // 拼团信息
            if (orderBuyInput.IsGroupBuy)
            {
                var productId = orderBuyInput.StoreOrders.FirstOrDefault().ProductSkuItems.FirstOrDefault().ProductId;
                var product = Resolve<IProductService>().GetSingle(r => r.Id == productId);
                if (product == null) return Tuple.Create(ServiceResult.FailedWithMessage("拼团商品已不存在"), new BuyOutput());

                var productActivity =
                    product.ProductActivityExtension?.Activitys?.FirstOrDefault(r =>
                        r.Key == typeof(GroupBuyActivity).FullName);
                activity = Resolve<IActivityService>().GetSingle(r => r.Id == productActivity.Id);
                if (activity == null) return Tuple.Create(ServiceResult.FailedWithMessage("拼团活动不存在"), new BuyOutput());

                groupBuyActivity = productActivity.Value.ToObject<GroupBuyActivity>();
                if (groupBuyActivity == null)
                    return Tuple.Create(ServiceResult.FailedWithMessage("商品拼团数据出错"), new BuyOutput());

                if (orderBuyInput.ActivityRecordId > 0)
                {
                    var activityRecord = Resolve<IActivityRecordService>()
                        .GetSingle(r => r.Id == orderBuyInput.ActivityRecordId);
                    if (activityRecord == null)
                        return Tuple.Create(ServiceResult.FailedWithMessage("参与的拼团记录已不存在"), new BuyOutput());

                    if (activityRecord.Status != ActivityRecordStatus.IsPay)
                        return Tuple.Create(ServiceResult.FailedWithMessage("参与的拼团记录未支付或已结束"), new BuyOutput());

                    var count = Resolve<IActivityRecordService>()
                        .Count(r => r.ParentId == orderBuyInput.ActivityRecordId);
                    if (count + 1 >= groupBuyActivity.BuyerCount)
                        return Tuple.Create(ServiceResult.FailedWithMessage("改拼团人数已达标"), new BuyOutput());
                }

                //当前用户记录
                var userActivityRecord = Resolve<IActivityRecordService>().GetSingle(r =>
                    r.ActivityId == activity.Id && r.UserId == orderBuyInput.UserId &&
                    r.Status == ActivityRecordStatus.IsPay);
                if (userActivityRecord != null)
                    return Tuple.Create(ServiceResult.FailedWithMessage("您已参与的拼团未结束，不能重复参加"), new BuyOutput());
            }

            #endregion 拼团验证

            #region 订购模式指定发货人

            // 如果是订购方式，指定发货人
            User deliverUser = null;
            if (orderBuyInput.IsFromOrder)
            {
                var dynamicInstance =
                    Activator.CreateInstance("Alabo.App.Erp.Stock.Domain.Services.ProductBuyStockService"
                        .GetTypeByFullName());
                var target = new Interpreter().SetVariable("dynamicInstance", dynamicInstance);
                var parameters = new[]
                {
                    new Parameter("buyInput", orderBuyInput)
                };
                deliverUser = (User)target.Eval("dynamicInstance.GetDeliverUser(buyInput)", parameters);
            }

            #endregion 订购模式指定发货人

            var context = Repository<IOrderRepository>().RepositoryContext;

            try
            {
                context.BeginTransaction();

                // 店铺Sku 分类   //orderBuyInput.MoneyItems  amount 判断是否足够支付所有的积分
                var keys = new Dictionary<ObjectId, string>();
                orderBuyInput.StoreOrders.ToList().ForEach(e =>
                {
                    keys.Add(e.StoreId, e.ProductSkuItems.Select(a => a.ProductSkuId).ToList().ToString());
                });
                // 计算最多可以使用的非现金商品数量   Sku表里面的    Price-MinpayCash    如果牵扯授信/积分/现金 多家店铺如何考虑  积分/授信不足如何考虑？
                //根据店铺，每个店铺生成一个订单
                var orderList = new List<Order>();

                // 验证是否有优惠券
                var couponAmount = 0M;
                var couponList = orderBuyInput.CouponJson.ToObject<List<string>>();

                foreach (var storeOrderItem in orderBuyInput.StoreOrders)
                {
                    #region 插入订单数据

                    var storeItem =
                        latestStoreProductSku.StoreItems.FirstOrDefault(r => r.StoreId == storeOrderItem.StoreId);
                    var storePriceItem =
                        latestStoreOrderPrice.StorePrices.FirstOrDefault(e => e.StoreId == storeOrderItem.StoreId);
                    var orderPaymentPrice = GetOrderPaymentPrice(storePriceItem, orderBuyInput);
                    var reduceAmounts = orderPaymentPrice.Item3; //订单中使用虚拟资产减少的金额
                    var orderFeeAmount = reduceAmounts.Sum(e => e.FeeAmount); // 订单服务费

                    // 验证优惠券使用条件
                    if (couponList.Count > 0)
                    {
                        var coupon = Resolve<IUserCouponService>()
                            .GetSingle(x => x.Id == couponList.FirstOrDefault().ToObjectId());
                        if (coupon != null && storePriceItem.TotalAmount > coupon.MinOrderPrice)
                            couponAmount = coupon.Value;
                    }

                    // var orderForCashAmount= storePriceItem.
                    // 订单表 ,数据插入到Shop_order 中
                    var order = new Order
                    {
                        UserId = orderBuyInput.UserId,
                        StoreId = storeOrderItem.StoreId.ToString(),
                        OrderStatus = OrderStatus.WaitingBuyerPay,
                        OrderType = orderBuyInput.OrderType,
                        AddressId = orderBuyInput.AddressId,
                        TotalAmount = storePriceItem.TotalAmount - couponAmount,
                        TotalCount = storeOrderItem.TotalCount,
                        PayId = 0,
                        PaymentAmount =
                            orderPaymentPrice.Item1 + orderFeeAmount - couponAmount // 获取订单实际支付价格(订单支付价格+服务费）
                    };
                    // 订单扩展数据
                    order.OrderExtension = new OrderExtension
                    {
                        // 留言信息
                        Message = new OrderMessage
                        {
                            BuyerMessage = storeOrderItem.UserMessage // 留言信息
                        },
                        // 价格信息
                        OrderAmount = new OrderAmount
                        {
                            TotalProductAmount = storePriceItem.ProductAmount, // 商品总价
                            ExpressAmount = storePriceItem.ExpressAmount, // 邮费
                            CalculateExpressAmount = storePriceItem.CalculateExpressAmount, // 计算邮费
                            FeeAmount = orderFeeAmount, // 服务费
                            ExpressType = storeOrderItem.ExpressType
                        },
                        // 店铺快照
                        Store = new Store
                        {
                            Id = storeOrderItem.StoreId,
                            Name = storeItem.StoreName
                        },
                        ProductSkuItems = storeItem.ProductSkuItems,
                        IsFromOrder = orderBuyInput.IsFromOrder,
                        AllowMoneys = latestStoreProductSku.AllowMoneys,
                        User = user, // 下单用户
                        ReduceAmounts = orderPaymentPrice.Item3, // 订单使用的资产
                        IsGroupBuy = orderBuyInput.IsGroupBuy, // 是否为拼团记录
                        UserAddress = cacheAddress // 地址
                    };

                    orderPaymentPrice.Item3.Foreach(r =>
                    {
                        order.AccountPayPair.Add(new KeyValuePair<Guid, decimal>(r.MoneyTypeId, r.Amount));
                    });
                    order.AccountPay = ObjectExtension.ToJson(order.AccountPayPair);
                    order.Extension = ObjectExtension.ToJson(order.OrderExtension);

                    // 如果发货人不为空
                    if (deliverUser != null) order.DeliverUserId = deliverUser.Id;

                    Resolve<IOrderService>().Add(order);

                    #endregion 插入订单数据

                    #region 插入店铺用户

                    //  Resolve<ITypeUserService>().AddStoreUser(order.StoreId, order.UserId);

                    #endregion 插入店铺用户

                    #region 插入订单商品数据

                    // 数据擦插入到Shop_product中
                    var orderProducts = new List<OrderProduct>();
                    foreach (var productSku in storeOrderItem.ProductSkuItems)
                    {
                        // 价格模式总价,根据sku获取使员的虚拟资产类型
                        var skuPaymentResult =
                            GetOrderProductPaymentPrice(productSku, orderBuyInput.StoreOrders, orderBuyInput);
                        // 如果校验不通过，订单生成失败，事物回滚
                        if (!skuPaymentResult.Item2.Succeeded)
                        {
                            context.RollbackTransaction();
                            return Tuple.Create(skuPaymentResult.Item2, new BuyOutput());
                        }

                        // sku减少资产统计
                        var reduceAcount = skuPaymentResult.Item3;
                        // 店铺商品Sku数据
                        var storeItemSku =
                            storeItem.ProductSkuItems.FirstOrDefault(r => r.ProductSkuId == productSku.ProductSkuId);
                        //单个商品的Id
                        var orderProduct = new OrderProduct
                        {
                            OrderId = order.Id,
                            ProductId = productSku.ProductId,
                            SkuId = productSku.ProductSkuId,
                            Count = productSku.Count,
                            Amount = storeItemSku.Price,
                            TotalAmount = storeItemSku.Price * productSku.Count,
                            PaymentAmount = skuPaymentResult.Item1 * productSku.Count
                        };
                        orderProduct.FenRunAmount =
                            storeItemSku.FenRunPrice * storeItemSku.BuyCount -
                            reduceAcount.ForCashAmount; // 分润价格,需要减去非人民币支付扣除的价格，比如分润价格为100，人民币支付价格为20，则分润价格为100-20=80
                        if (orderProduct.FenRunAmount <= 0) orderProduct.FenRunAmount = 0;

                        orderProduct.OrderProductExtension = new OrderProductExtension
                        {
                            OrderAmount = new OrderAmount
                            {
                                TotalProductAmount = storePriceItem.ProductAmount, // 商品总价
                                ExpressAmount = storePriceItem.ExpressAmount, // 邮费
                                FeeAmount = reduceAcount.FeeAmount //  服务费
                            },

                            ProductSkuItem = storeItemSku,
                            //new Func<ProductSkuItem,ProductSkuItem>(s=> {
                            //    s.PlatformPrice = s.Price;
                            //    return s;
                            //}).Invoke(storeItemSku), // 商品Sku快照
                            TotalWeight = storeItemSku.Weight * storeItem.TotalCount, // 总重量
                            ReduceAmount = reduceAcount // 减少的资产，改sku使用其他支付的资产
                        };
                        orderProduct.Extension = ObjectExtension.ToJson(orderProduct.OrderProductExtension);
                        orderProducts.Add(orderProduct);
                    }

                    #region 验证订单和订单商品的数据是否相符

                    if (!(order.TotalAmount + couponAmount - storePriceItem.ExpressAmount).EqualsDigits(
                        orderProducts.Sum(r => r.TotalAmount)))
                    {
                        context.RollbackTransaction();
                        return Tuple.Create(ServiceResult.FailedWithMessage("订单和订单商品总价不符"), new BuyOutput());
                    }

                    if (order.TotalCount != orderProducts.Sum(r => r.Count))
                    {
                        context.RollbackTransaction();
                        return Tuple.Create(ServiceResult.FailedWithMessage("订单和订单商品总数不符"), new BuyOutput());
                    }

                    // 验证订单实际支付价格=订单商品支付总价+订单邮费
                    if (!(order.PaymentAmount + couponAmount - storePriceItem.ExpressAmount).EqualsDigits(
                        orderProducts.Sum(r => r.PaymentAmount)))
                    {
                        context.RollbackTransaction();
                        return Tuple.Create(ServiceResult.FailedWithMessage("订单和订单商品人民币支付金额不符"), new BuyOutput());
                    }

                    if (order.PaymentAmount < storePriceItem.ExpressAmount)
                    {
                        context.RollbackTransaction();
                        return Tuple.Create(ServiceResult.FailedWithMessage("支付金额不能小于运费"), new BuyOutput());
                    }

                    // 订单商品抵现金额统计
                    var totalForCashAmount = 0.0m;
                    var totalReduceCashAmount = 0.0m; // 总共减少的现金抵现金额
                    var totalFeeAmount = 0.0m; // 总共使用虚拟资产的服务费
                    orderProducts.Foreach(r =>
                    {
                        totalForCashAmount += r.OrderProductExtension.ReduceAmount.ForCashAmount;
                        var moneyConfig = Resolve<IAutoConfigService>().MoneyTypes()
                            .FirstOrDefault(e => e.Id == r.OrderProductExtension.ReduceAmount.MoneyTypeId);
                        totalReduceCashAmount += r.OrderProductExtension.ReduceAmount.Amount * moneyConfig.RateFee;
                        totalFeeAmount += r.OrderProductExtension.ReduceAmount.FeeAmount;
                    });
                    //if (!(orderProducts.Sum(r => r.FenRunAmount) + totalForCashAmount).EqualsDigits(storeItem.ProductSkuItems.Sum(r => r.FenRunPrice * r.BuyCount))) {
                    //    context.RollbackTransaction();
                    //    return Tuple.Create(ServiceResult.FailedWithMessage("分润价格计算不正确"), new BookingBuyOutput());
                    //}
                    //if ((orderProducts.Sum(r => r.FenRunAmount) + storePriceItem.ExpressAmount).MoreThanDigits(order.PaymentAmount)) {
                    //    context.RollbackTransaction();
                    //    return Tuple.Create(ServiceResult.FailedWithMessage("分润金额不能大于商品支付金额"), new BookingBuyOutput());
                    //}
                    // 验证减少的金额+订单付款金额 是否为订单支付金额相等

                    if (!(order.TotalAmount + couponAmount - storePriceItem.ExpressAmount).EqualsDigits(
                        orderProducts.Sum(r => r.PaymentAmount) + totalReduceCashAmount - totalFeeAmount))
                    {
                        context.RollbackTransaction();
                        return Tuple.Create(ServiceResult.FailedWithMessage("订单总金额与订单支付金额和资产抵现金额不相等"),
                            new BuyOutput());
                    }

                    if (!(totalReduceCashAmount + orderProducts.Sum(r => r.PaymentAmount) - totalFeeAmount)
                        .EqualsDigits(orderProducts.Sum(r => r.TotalAmount)))
                    {
                        context.RollbackTransaction();
                        return Tuple.Create(ServiceResult.FailedWithMessage("订单商品总价与订单抵现金额和订单支付金额不相同"),
                            new BuyOutput());
                    }

                    // 验证扩展字段中扣除资产是否相等
                    foreach (var item in order.OrderExtension.ReduceAmounts)
                    {
                        var amount = 0.0m;
                        var forCashItem = 0.0m;
                        orderProducts.ForEach(r =>
                        {
                            if (r.OrderProductExtension.ReduceAmount.MoneyTypeId == item.MoneyTypeId)
                            {
                                amount += r.OrderProductExtension.ReduceAmount.Amount;
                                forCashItem += r.OrderProductExtension.ReduceAmount.ForCashAmount;
                            }
                        });
                        if (!amount.EqualsDigits(item.Amount) || !item.ForCashAmount.EqualsDigits(forCashItem))
                        {
                            context.RollbackTransaction();
                            return Tuple.Create(ServiceResult.FailedWithMessage("订单商品和订单的扣除资产不相符"),
                                new BuyOutput());
                        }
                    }

                    // 核对订单账户金额和订单商品账户金额是否一致
                    foreach (var accountPay in order.AccountPayPair)
                    {
                        var value = 0.0m;
                        foreach (var orderProduct in orderProducts)
                            if (orderProduct.OrderProductExtension.ReduceAmount.MoneyTypeId == accountPay.Key)
                                value += orderProduct.OrderProductExtension.ReduceAmount.Amount;

                        if (!value.EqualsDigits(accountPay.Value))
                            return Tuple.Create(ServiceResult.FailedWithMessage("订单账户支付金额与订单商品账户金额不对"),
                                new BuyOutput());
                    }

                    #endregion 验证订单和订单商品的数据是否相符

                    Resolve<IOrderProductService>().AddMany(orderProducts);

                    #endregion 插入订单商品数据

                    #region 添加订单操作记录

                    // 添加操作记录
                    Resolve<IOrderActionService>().Add(order, user, OrderActionType.UserCreateOrder);
                    orderList.Add(order);

                    #endregion 添加订单操作记录

                    #region 添加活动记录  目前只有拼团时才加入记录

                    //更新限时购活动库存
                    timeLimitActivities.ForEach(item => { Resolve<IActivityService>().UpdateNoTracking(item); });

                    if (orderBuyInput.IsGroupBuy)
                    {
                        var activityRecord = new ActivityRecord
                        {
                            ActivityId = activity.Id,
                            ParentId = orderBuyInput.ActivityRecordId,
                            OrderId = order.Id,
                            UserId = order.UserId,
                            StoreId = order.StoreId,
                            Status = ActivityRecordStatus.Processing,
                            ActivityRecordExtension = new ActivityRecordExtension
                            {
                                Order = order,
                                User = user
                            }
                            // ProductId
                        };
                        activityRecord.Extension = ObjectExtension.ToJson(activityRecord.ActivityRecordExtension);
                        Resolve<IActivityRecordService>().Add(activityRecord);
                    }

                    #endregion 添加活动记录  目前只有拼团时才加入记录
                }

                #region 添加人民币支付记录

                // 添加人民币支付记录，拼团记录通过此添加
                var singlePayInput = new SinglePayInput
                {
                    Orders = orderList,
                    User = user,
                    ReduceMoneys = latestStoreProductSku.AllowMoneys.ToList(),
                    IsAdminPay = false,
                    IsGroupBuy = orderBuyInput.IsGroupBuy,
                    IsFromOrder = orderBuyInput.IsFromOrder,
                    BuyerCount = groupBuyActivity.BuyerCount
                };
                var payResult = Resolve<IOrderAdminService>().AddSinglePay(singlePayInput);
                if (!payResult.Item1.Succeeded)
                {
                    // 支付记录添加失败，回滚
                    context.RollbackTransaction();
                    return Tuple.Create(payResult.Item1, new BuyOutput());
                }

                //更新人民币支付记录Id
                var orderIds = orderList.Select(e => e.Id).ToList();
                Resolve<IOrderService>().Update(r => { r.PayId = payResult.Item2.Id; }, e => orderIds.Contains(e.Id));

                #endregion 添加人民币支付记录

                #region 删除购物车、更新商品数据等操作

                // 删除购物车数据
                if (orderBuyInput.IsFromCart) Resolve<IOrderActionService>().DeleteCartBuyOrder(orderBuyInput);

                // 更新购买记录
                var productBuyCount = new Dictionary<long, long>();
                foreach (var store in orderBuyInput.StoreOrders)
                {
                    var productIds = store.ProductSkuItems.Select(r => r.ProductId).Distinct();
                    //库存减少
                    foreach (var sku in store.ProductSkuItems)
                        Resolve<IProductSkuService>().Update(r => { r.Stock = r.Stock - sku.Count; },
                            r => r.Id == sku.ProductSkuId);

                    foreach (var productId in productIds)
                    {
                        var buyCount = store.ProductSkuItems.Where(r => r.ProductId == productId).Sum(r => r.Count);
                        productBuyCount.Add(productId, buyCount);
                    }
                }

                //销量
                foreach (var item in productBuyCount)
                    Resolve<IProductService>().Update(r => { r.SoldCount = r.SoldCount + item.Value; },
                        r => r.Id == item.Key);

                // 如果使用了优惠券, set为已经使用
                if (couponAmount > 0)
                {
                    var userCoupon = Resolve<IUserCouponService>().GetSingle(x =>
                        x.UserId == orderBuyInput.UserId && x.Id == couponList.FirstOrDefault().ToObjectId());
                    if (userCoupon != null)
                    {
                        userCoupon.CouponStatus = CouponStatus.Used;

                        Resolve<IUserCouponService>().Update(userCoupon);
                    }
                }

                #endregion 删除购物车、更新商品数据等操作

                // 输出赋值
                BookingBuyOutput.PayAmount = payResult.Item2.Amount;
                BookingBuyOutput.PayId = payResult.Item2.Id;
                BookingBuyOutput.OrderIds = orderList.Select(r => r.Id).ToList();
                context.SaveChanges();
                context.CommitTransaction();
            }
            catch (Exception ex)
            {
                context.RollbackTransaction();
                result = ServiceResult.FailedWithMessage(ex.Message);
            }
            finally
            {
                context.DisposeTransaction();
            }

            ObjectCache.Remove(orderBuyInput.Sign); // 移除缓存
            return Tuple.Create(result, BookingBuyOutput);
        }

        #endregion 订单购物

        #region 生成订单确定页面信息

        /// <summary>
        ///     生成订单确定页面信息
        ///     每次客户端更改数量，通过请求该方法重新计算价格
        ///     后期可以考虑客服端计算价格
        ///     对应前台界面 /order/buy
        /// </summary>
        /// <param name="buyInfoInput"></param>
        public Tuple<ServiceResult, StoreProductSku> BuyInfo(BuyInfoInput buyInfoInput)
        {
            var storeProductSku = new StoreProductSku();
            var orderProductInputList = new List<OrderProductInput>(); //  前端接收参数

            #region 安全验证

            try
            {
                orderProductInputList = buyInfoInput.ProductJson.DeserializeJson<List<OrderProductInput>>();
                orderProductInputList = orderProductInputList.OrderByDescending(r => r.ProductId).ToList();
            }
            catch (Exception ex)
            {
                return Tuple.Create(ServiceResult.FailedWithMessage("序列化出错:" + ex.Message), storeProductSku);
            }

            //购买数量小于等于0直接返回失败
            if (orderProductInputList.Count <= 0)
                return Tuple.Create(ServiceResult.FailedWithMessage("购买数量不能小于等于0"), storeProductSku);

            // 当PriceStyle修改后, 要更新Sku数据才能正确计算订单数据.
            foreach (var product in orderProductInputList)
                Ioc.Resolve<IProductSkuService>().AutoUpdateSkuPrice(product.ProductId);

            var userIds = orderProductInputList.Select(r => r.LoginUserId).Distinct().ToList();
            if (userIds.Count != 1 && userIds.FirstOrDefault() != buyInfoInput.LoginUserId)
                return Tuple.Create(ServiceResult.FailedWithMessage("用户与商品不对应，或者存在多个用户的订单"), storeProductSku);

            var user = Resolve<IUserService>().GetSingle(buyInfoInput.LoginUserId); // 缓存中读取用户
            if (user == null || user.Status != Status.Normal)
                return Tuple.Create(ServiceResult.FailedWithMessage("您的输入的账户不存在，或者状态不正常"), storeProductSku);

            var storeItems = Resolve<IShopStoreService>().GetStoreItemListFromCache()
                .Where(r => orderProductInputList.Select(e => e.StoreId).Contains(r.StoreId))?.ToList();
            if (storeItems == null || storeItems.Count == 0)
                return Tuple.Create(ServiceResult.FailedWithMessage("您购买的商品店铺不存"), storeProductSku);

            // 获取店铺是商品信息
            var storeProductSkuDtos = new StoreProductSkuDtos
            {
                StoreIds = storeItems.Select(r => r.StoreId).ToList(),
                IsGroupBuy = buyInfoInput.IsGroupBuy,
                ProductSkuIds = orderProductInputList.Select(r => r.ProductSkuId).Distinct().ToList(),
                ProductId = orderProductInputList.FirstOrDefault().ProductId,
                IsApiImage = true
            };
            var storeProductSkuResult = Resolve<IShopStoreService>().GetStoreProductSku(storeProductSkuDtos);
            if (storeProductSkuResult == null
                || storeProductSkuResult.Item1 == null
                || !storeProductSkuResult.Item1.Succeeded)
                return Tuple.Create(
                    storeProductSkuResult == null || storeProductSkuResult.Item1 == null
                        ? ServiceResult.FailedWithMessage("Sku库存不足")
                        : storeProductSkuResult.Item1, storeProductSku);

            storeProductSku = storeProductSkuResult.Item2;
            if (storeProductSku == null || storeProductSku?.StoreItems == null ||
                storeProductSku?.StoreItems?.Count == 0)
                return Tuple.Create(ServiceResult.FailedWithMessage("您购买的商品店铺不存"), storeProductSku);

            #endregion 安全验证

            //本地方法 计算购买数量
            storeProductSku.StoreItems.ForEach(t =>
            {
                t.ProductSkuItems.ToList().ForEach(e =>
                {
                    e.BuyCount = (orderProductInputList.FirstOrDefault(r => r.ProductSkuId == e.ProductSkuId)?.Count)
                        .ConvertToLong();
                    if (e.BuyCount > e.Stock) e.BuyCount = e.Stock; // 如果购买数量大于库存，则购买数据等于库存，全部购买，库存0商品已经判断

                    e.DisplayPrice = Resolve<IProductService>().GetDisplayPrice(e.Price, e.PriceStyleId, 0M);
                });
            });
            // 根据店铺Sku以及数据，获取出改订单允许使用的货币以及资产
            var allProductSkus = new List<ProductSkuItem>();
            storeProductSku.StoreItems.ToList().ForEach(r => { allProductSkus.AddRange(r.ProductSkuItems); });

            // 计算价格
            CountPrice(ref storeProductSku, null, user, null, null, false);

            // 如果为下单页面生成缓存，购物车的时候不需要
            if (buyInfoInput.IsBuy)
            {
                storeProductSku.AllowMoneys = Resolve<IProductSkuService>()
                    .GetStoreMoneyBuySkus(allProductSkus, buyInfoInput.LoginUserId).ToList();
                // 根据用户名，店铺Id列表，SkuIds列表，生成签名,使用时间
                storeProductSku.Sign = $"{buyInfoInput.LoginUserId}" +
                                       $"{ObjectExtension.ToJson(storeItems.Select(r => r.TotalCount))}" +
                                       $"{ObjectExtension.ToJson(storeItems.Select(r => r.StoreId))}" +
                                       $"{ObjectExtension.ToJson(orderProductInputList.Select(r => r.Count).ToList())}" +
                                       $"{ObjectExtension.ToJson(orderProductInputList.Select(r => r.ProductSkuId).ToList().Distinct()) + DateTime.Now.ToString("yyyy-MM-dd-HH")}";
                storeProductSku.Sign = storeProductSku.Sign.ToMd5HashString();
                var cacheKey = $"OrderPrice_{storeProductSku.Sign}";
                if (!ObjectCache.TryGet(cacheKey, out OrderPriceCache orderPriceCache))
                {
                    // 将用户所有的地址信息，和商品购买信息，插入缓存
                    orderPriceCache = new OrderPriceCache
                    {
                        StoreProductSku = storeProductSku,
                        UserAddresses = Resolve<IUserAddressService>()
                            .GetList(r => r.UserId == buyInfoInput.LoginUserId).ToList()
                    };
                    orderPriceCache.OrderMoneys = storeProductSku.AllowMoneys;
                    ObjectCache.Set(cacheKey, orderPriceCache, TimeSpan.FromHours(1)); // 缓存1个小时或自动过期回收
                }
            }

            // 获取当前会员，在当前店铺可以使用的优惠券
            storeProductSku.StoreItems.ForEach(r =>
            {
                var ss = Resolve<IUserCouponService>().GetList(e =>
                    e.StoreId == r.StoreId && e.UserId == buyInfoInput.LoginUserId
                                           // && storeProductSku.TotalAmount > e.MinOrderPrice      // has prob, exit select()
                                           && e.CouponStatus == CouponStatus.Normal
                                           && DateTime.Now >= e.StartValidityTime
                                           && DateTime.Now <= e.EndValidityTime).ToList();

                r.Coupons = ss.Where(x => x.MinOrderPrice < storeProductSku.TotalAmount).ToList();
            });

            return Tuple.Create(ServiceResult.Success, storeProductSku);
        }

        #endregion 生成订单确定页面信息

        #region 获取价格

        /// <summary>
        ///     获取价格
        /// </summary>
        /// <param name="userOrderInput"></param>
        public Tuple<ServiceResult, StoreOrderPrice> GetPrice(UserOrderInput userOrderInput)
        {
            //user
            var user = Resolve<IUserService>().GetSingle(userOrderInput.LoginUserId); // 缓存中读取用户
            if (user == null || user.Status != Status.Normal)
                return Tuple.Create(ServiceResult.FailedWithMessage("您的输入的账户不存在，或者状态不正常"), new StoreOrderPrice());
            var cacheKey = $"OrderPrice_{userOrderInput.Sign}";
            try
            {
                userOrderInput.StoreExpress = userOrderInput.StoreExpressJson.DeserializeJson<List<StoreExpress>>();
                if (userOrderInput.StoreExpress == null || userOrderInput.StoreExpress?.ToList()?.Count == 0)
                    return Tuple.Create(ServiceResult.FailedWithMessage("请选择配送方式"), new StoreOrderPrice());
            }
            catch
            {
                return Tuple.Create(ServiceResult.FailedWithMessage("配送方式序列化出错，运费参数传入错误"), new StoreOrderPrice());
            }

            try
            {
                if (!userOrderInput.ReduceMoneysJson.IsNullOrEmpty())
                    userOrderInput.ReduceMoneys = userOrderInput.ReduceMoneysJson
                        .DeserializeJson<List<KeyValuePair<Guid, decimal>>>();
            }
            catch
            {
                return Tuple.Create(ServiceResult.FailedWithMessage("使用资产序列化出错，使用资产传入错误"), new StoreOrderPrice());
            }

            if (ObjectCache.TryGet(cacheKey, out OrderPriceCache orderPriceCache))
            {
                var address =
                    orderPriceCache.UserAddresses.FirstOrDefault(r => r.Id == userOrderInput.AddressId.ToObjectId());
                if (address == null)
                {
                    // 如果缓存不存在地址，则从数据库在读取一次，解决购物时，地址新增的情况
                    address = Resolve<IUserAddressService>()
                        .GetUserAddress(userOrderInput.AddressId.ToObjectId(), userOrderInput.LoginUserId);
                    if (address == null)
                        return Tuple.Create(ServiceResult.FailedWithMessage("您选择地址不存在"), new StoreOrderPrice());
                }

                if (orderPriceCache.StoreProductSku.StoreItems.Count != userOrderInput.StoreExpress.ToList().Count())
                    return Tuple.Create(ServiceResult.FailedWithMessage("店铺数与配送方式数不比配"), new StoreOrderPrice());

                var storeProductSku = orderPriceCache.StoreProductSku;
                storeProductSku.StoreItems.ForEach(item =>
                {
                    var temp = userOrderInput.StoreExpress.Find(s => s.Key == item.StoreId.ToString());
                    if (temp != null) item.ExpressType = temp.ExpressType;
                });
                return CountPrice(ref storeProductSku, userOrderInput, user, orderPriceCache.UserAddresses,
                    orderPriceCache.OrderMoneys, false);
            }

            var result = ServiceResult.FailedWithMessage("订单已过期，请刷新页面或重新下单");
            result.Id = -1; // 用户标记订单缓存不存在，前台重新请求一次
            return Tuple.Create(result, new StoreOrderPrice());
        }

        #endregion 获取价格

        #region 整个系统唯一的价格计算函数

        public Tuple<ServiceResult, StoreOrderPrice> CountPrice(ref StoreProductSku storeProductSku,
            UserOrderInput userOrderInput, User user, IEnumerable<UserAddress> userAddress = null,
            IList<OrderMoneyItem> orderMoneys = null, bool isUpdateSkuPrice = true)
        {
            var address = userOrderInput != null
                ? userAddress.FirstOrDefault(r => r.Id == userOrderInput.AddressId.ToObjectId())
                : null;
            if (address == null && userOrderInput != null)
                address = Resolve<IUserAddressService>()
                    .GetUserAddress(userOrderInput.AddressId.ToObjectId(), userOrderInput.LoginUserId);

            var result = ServiceResult.Success;
            var storeOrderPrice = new StoreOrderPrice();
            //  计算属性
            var minPayCash = 0.0m; // 现金最小支付额度
            //会员价
            var productGradePrices =
                Resolve<IActivityApiService>().GetMemberDiscountPrice(storeProductSku.StoreItems, user);
            foreach (var storeItem in storeProductSku.StoreItems)
            {
                var storePrice = new StorePrice
                {
                    StoreId = storeItem.StoreId
                };
                foreach (var productSkuItem in storeItem.ProductSkuItems)
                {
                    if (productSkuItem.BuyCount > productSkuItem.Stock)
                        result = ServiceResult.FailedWithMessage("购买数量大于商品库存数量");

                    //member sku price
                    var price = productSkuItem.Price;
                    productSkuItem.PlatformPrice = price;
                    var originalAmount = productSkuItem.BuyCount * price;
                    var productGrade = productGradePrices.Find(p =>
                        p.ProductId == productSkuItem.ProductId && p.ProductSkuId == productSkuItem.ProductSkuId);
                    if (productGrade != null)
                    {
                        price = productGrade.MemberPrice;
                        if (isUpdateSkuPrice) productSkuItem.Price = price;
                    }

                    //member discount price
                    storePrice.ProductAmount += productSkuItem.BuyCount * price; // sku价格
                    var discountAmount = originalAmount - storePrice.ProductAmount;
                    storePrice.MemberDiscountAmount += discountAmount > 0 ? discountAmount : 0;
                    if (productSkuItem.IsFreeShipping)
                        storePrice.TotalWeight += 0; // 免邮费
                    else
                        storePrice.TotalWeight += productSkuItem.BuyCount * productSkuItem.Weight / 1000; // 重量
                    minPayCash += productSkuItem.BuyCount * productSkuItem.MinPayCash;
                }

                // 商品总运费
                if (userAddress != null && address != null)
                {
                    var expressFeeResult = Resolve<IShopStoreService>()
                        .CountExpressFee(storeItem.StoreId, address, storeItem.ProductSkuItems);
                    if (!expressFeeResult.Item1.Succeeded)
                    {
                        result = ServiceResult.FailedWithMessage(expressFeeResult.Item1.ToString());
                    }
                    else
                    {
                        storePrice.ExpressAmount = expressFeeResult.Item2;
                        storePrice.CalculateExpressAmount = expressFeeResult.Item2;
                        if (storeItem.ExpressType == ExpressType.Self) storePrice.ExpressAmount = 0;
                    }
                }

                // 总价格
                storePrice.TotalAmount = storePrice.ProductAmount + storePrice.ExpressAmount; // 店铺购买总价格 商品价格+运费
                storeOrderPrice.StorePrices.Add(storePrice);
                storeOrderPrice.ExpressAmount += storePrice.ExpressAmount;
                storeOrderPrice.ProductAmount += storePrice.ProductAmount;
                storeOrderPrice.TotalAmount += storePrice.TotalAmount;

                storeItem.TotalCount = storeItem.ProductSkuItems.Sum(r => r.BuyCount); // 店铺购买总数量
                storeItem.TotalAmount = storePrice.ProductAmount; // 返回店铺价格
            }

            storeProductSku.TotalAmount = storeProductSku.StoreItems.Sum(e => e.TotalAmount); // 总价格
            storeProductSku.TotalCount = storeProductSku.StoreItems.Sum(e => e.TotalCount); // 总数量

            #region 计算虚拟资产

            if (userOrderInput != null)
            {
                if (orderMoneys != null && userOrderInput.ReduceMoneys != null)
                {
                    var moneyTypes = Resolve<IAutoConfigService>().MoneyTypes();
                    var orderConfig = Resolve<IAutoConfigService>().GetValue<OrderConfig>();

                    var reduceMoney = 0.0m; // 减少的金额
                    var feeAmount = 0.0m; // 服务费
                    foreach (var reduceMoneyItem in userOrderInput.ReduceMoneys)
                    {
                        var moneyTypeItem = moneyTypes.FirstOrDefault(e => e.Id == reduceMoneyItem.Key);
                        var orderMoneyItem = orderMoneys.FirstOrDefault(r => r.MoneyId == moneyTypeItem.Id);
                        if (orderMoneyItem != null)
                        {
                            if (orderMoneyItem.MaxPayPrice == reduceMoneyItem.Value)
                            {
                                reduceMoney += reduceMoneyItem.Value * moneyTypeItem.RateFee; // 减少的人民币
                                feeAmount +=
                                    reduceMoneyItem.Value *
                                    moneyTypeItem.ServiceRateFee; //  服务费比例，统一为虚拟资产的一定比例 使用虚拟资产时候需要增加服务费比例
                            }
                            else
                            {
                                result.Id = -1; // 用户资产可能减少或修改，前台重新请求一次
                                var cacheKey = $"OrderPrice_{userOrderInput.Sign}";
                                ObjectCache.Remove(cacheKey); // 删除缓存，前台重新请求tu
                                result = ServiceResult.FailedWithMessage("资产数据传入有误");
                            }

                            // 现金是否可以抵用虚拟资产
                            if (!orderConfig.CashCanToOtherAssets)
                            {
                                var moneyItemAccount = Resolve<IAccountService>()
                                    .GetAccountAmount(userOrderInput.LoginUserId, orderMoneyItem.MoneyId);
                                if (moneyItemAccount < storeProductSku.TotalAmount)
                                {
                                    result = ServiceResult.FailedWithMessage(
                                        $"{orderMoneyItem.Name}余额不足，余额:{moneyItemAccount}");
                                    break;
                                }
                            }

                            storeOrderPrice.ReduceMoneys.Add(orderMoneyItem); // 累加店铺扣除非人民币金额
                        }
                    }

                    // 判断现金最小支付额度
                    var productAmount = storeOrderPrice.ProductAmount;
                    storeOrderPrice.TotalAmount -= reduceMoney; // 现金总额减去资产
                    storeOrderPrice.FeeAmount = feeAmount; // 店铺 服务费
                    storeOrderPrice.TotalAmount += feeAmount; //总资产加上服务费
                }

                // 优惠券计算
                if (userOrderInput.CouponJson.IsNotNullOrEmpty())
                {
                    var coupList = userOrderInput.CouponJson.ToObject<List<string>>();
                    if (coupList.Count > 0)
                    {
                        // 判断现金最小支付额度
                        var productAmount = storeOrderPrice.ProductAmount;
                        var coupon = Resolve<IUserCouponService>()
                            .GetSingle(x => x.Id == coupList.FirstOrDefault().ToObjectId());
                        if (coupon != null)
                            if (productAmount > coupon.MinOrderPrice)
                            {
                                storeOrderPrice.TotalAmount -= coupon.Value; // 现金总额减去资产
                                //storeOrderPrice.ProductAmount -= coupon.Value; // 现金总额减去资产
                                storeOrderPrice.FeeAmount -= coupon.Value; // 店铺 服务费
                                //storeOrderPrice.TotalAmount += feeAmount; //总资产加上服务费
                            }
                    }
                }

                //if (storeOrderPrice.TotalAmount < minPayCash)
                //{
                //    result = ServiceResult.FailedWithMessage("现金最低字符金额超出");
                //}
            }

            #endregion 计算虚拟资产

            return Tuple.Create(result, storeOrderPrice);
        }

        #endregion 整个系统唯一的价格计算函数

        #region 代付款订单支付

        /// <summary>
        ///     代付款订单支付
        ///     tods:此方法有逻辑漏洞，如果一个订单存在多个商品时可能出错。后期优化
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Tuple<ServiceResult, BuyOutput> Pay(long orderId, long userId)
        {
            var order = Resolve<IOrderService>().GetSingle(e => e.Id == orderId && e.UserId == userId);
            if (order == null) return Tuple.Create(ServiceResult.FailedWithMessage("订单不存在"), new BuyOutput());
            if (order.OrderStatus != OrderStatus.WaitingBuyerPay)
                return Tuple.Create(ServiceResult.FailedWithMessage("非待付款订单，不能付款"), new BuyOutput());
            order.OrderExtension = order.Extension.ToObject<OrderExtension>();
            var user = Resolve<IUserService>().GetNomarlUser(userId);
            if (user == null) return Tuple.Create(ServiceResult.FailedWithMessage("用户不存在或状态不正常"), new BuyOutput());
            var ordersList = new List<Order>
            {
                order
            };
            // 添加人民币支付记录，拼团记录通过此添加 此函数可能有问题
            var singlePayInput = new SinglePayInput
            {
                Orders = ordersList,
                User = user,
                ReduceMoneys = order.OrderExtension.AllowMoneys.ToList(),
                IsAdminPay = false,
                IsGroupBuy = order.OrderExtension.IsGroupBuy,
                IsFromOrder = order.OrderExtension.IsFromOrder,
                BuyerCount = order.TotalCount
            };
            var payResult = Resolve<IOrderAdminService>().AddSinglePay(singlePayInput);
            if (!payResult.Item1.Succeeded) return Tuple.Create(payResult.Item1, new BuyOutput());

            var orderBuy = new BuyOutput
            {
                PayAmount = payResult.Item2.Amount,
                PayId = payResult.Item2.Id
            };
            orderBuy.OrderIds.Add(order.Id);

            return Tuple.Create(ServiceResult.Success, orderBuy);
        }

        #endregion 代付款订单支付

        #region 订单处理私有辅助函数

        private BuyInfoInput BuyInputToBuyInfoInput(BuyInput orderBuyInput)
        {
            var orderProductInputList = new List<OrderProductInput>();
            orderBuyInput.StoreOrders.Foreach(s =>
            {
                s.ProductSkuItems.Foreach(p =>
                {
                    orderProductInputList.Add(new OrderProductInput
                    {
                        ProductId = p.ProductId,
                        ProductSkuId = p.ProductSkuId,
                        Count = p.Count,
                        LoginUserId = orderBuyInput.UserId,
                        StoreId = s.StoreId
                    });
                });
            });
            var buyInfoInput = new BuyInfoInput
            {
                LoginUserId = orderBuyInput.UserId,
                IsBuy = true,
                ProductJson = ObjectExtension.ToJson(orderProductInputList)
            };

            return buyInfoInput;
        }

        /// <summary>
        ///     将前台订单数据转换成，店铺商品信息，重新生成订单信息
        ///     重新计算价格
        /// </summary>
        /// <param name="buyInput"></param>
        private StoreProductSku BuyInputToStoreProductSku(BuyInput buyInput)
        {
            var storeProductSku = new StoreProductSku();
            var skuIdList = new List<long>();
            long productId = 0; // 拼团时用
            buyInput.StoreOrders.ToList().ForEach(r =>
            {
                skuIdList.AddRange(r.ProductSkuItems.Select(e => e.ProductSkuId).Distinct().ToList());
                productId = r.ProductSkuItems.FirstOrDefault().ProductId;
            });
            // 从数据库中，重新读取出信息
            // 获取店铺是商品信息
            var storeProductSkuDtos = new StoreProductSkuDtos
            {
                StoreIds = buyInput.StoreOrders.Select(r => r.StoreId).ToList(),
                IsGroupBuy = buyInput.IsGroupBuy,
                ProductSkuIds = skuIdList,
                ProductId = productId,
                IsApiImage = false
            };
            storeProductSku = Resolve<IShopStoreService>().GetStoreProductSku(storeProductSkuDtos).Item2;
            storeProductSku.StoreItems.ToList().ForEach(r =>
            {
                r.ProductSkuItems.ToList().ForEach(e =>
                {
                    // 计算购买数量
                    e.BuyCount = buyInput.StoreOrders.FirstOrDefault(t => t.StoreId == r.StoreId).ProductSkuItems
                        .FirstOrDefault(p => p.ProductSkuId == e.ProductSkuId).Count;
                });
            });
            //前端使用的资产 ，从数据库中，重新读取可使用的资产
            // 根据店铺Sku以及数据，获取出改订单允许使用的货币以及资产
            var allProductSkus = new List<ProductSkuItem>();
            storeProductSku.StoreItems.ToList().ForEach(r => { allProductSkus.AddRange(r.ProductSkuItems); });
            //店铺使用的资产
            var allowMoneys = Resolve<IProductSkuService>().GetStoreMoneyBuySkus(allProductSkus, buyInput.UserId)
                .ToList();
            if (buyInput.ReduceMoneys != null)
                buyInput.ReduceMoneys.Foreach(r =>
                {
                    var useMoneyItem = allowMoneys.First(e => e.MoneyId == r.Key);
                    storeProductSku.AllowMoneys.Add(useMoneyItem);
                });

            return storeProductSku;
        }

        /// <summary>
        ///     将前台订单输入转换为用户订单
        /// </summary>
        /// <param name="buyInput"></param>
        private UserOrderInput BuyInputToUserOrderInput(BuyInput buyInput)
        {
            var userOrderInput = new UserOrderInput
            {
                AddressId = buyInput.AddressId,
                LoginUserId = buyInput.UserId
            };
            buyInput.StoreOrders.ToList().ForEach(r =>
            {
                userOrderInput.StoreExpress.Add(new StoreExpress
                {
                    Key = r.StoreId.ToString(),
                    Value = r.DeliveryId,
                    ExpressType = r.ExpressType
                });
            });
            userOrderInput.Sign = buyInput.Sign;
            userOrderInput.ReduceMoneys = buyInput.ReduceMoneys;
            userOrderInput.CouponJson = buyInput.CouponJson;

            return userOrderInput;
        }

        /// <summary>
        ///     Gets the order payment price.
        /// </summary>
        /// <param name="storePrice"></param>
        /// <param name="orderBuyInput">The order buy input.</param>
        private Tuple<decimal, ServiceResult, List<ReduceAmount>> GetOrderPaymentPrice(StorePrice storePrice,
            BuyInput orderBuyInput)
        {
            var result = ServiceResult.Success;
            var resultList = new List<ReduceAmount>();
            //decimal feeAmount = 0.0m; //服务费
            var storePayment = storePrice.TotalAmount; // 店铺支付价格,为店铺总价
            var priceStyleConfigs =
                Resolve<IAutoConfigService>()
                    .GetList<PriceStyleConfig>(r => r.Status == Status.Normal); // 状态不正常的商品，可能不支持价格类型

            foreach (var reduceMoneyItem in orderBuyInput.ReduceMoneys)
            {
                //当前货币类型
                var moneyItemConfig = Resolve<IAutoConfigService>().MoneyTypes()
                    .FirstOrDefault(r => r.Id == reduceMoneyItem.Key); // 货币类型
                // 根据sku商城模式，获取所有使用同种货币类型的
                var skuMoneyPriceStyleIds =
                    priceStyleConfigs.Where(r => r.MoneyTypeId == reduceMoneyItem.Key).Select(r => r.Id);
                // 所有店铺：货币类型相同的Sku总价格
                var skuTotalAmount = 0.0m;
                orderBuyInput.StoreOrders.Foreach(t =>
                {
                    skuTotalAmount += t.ProductSkuItems.Where(r => skuMoneyPriceStyleIds.Contains(r.PriceStyleId))
                        .Sum(r => r.Amount);
                });
                // 总共使用的虚拟资产
                var reduceTotalAmount = reduceMoneyItem.Value;

                // 当前店铺：货币类型相同的Sku总价格
                var skuStoreTotalAmount = 0.0m;
                orderBuyInput.StoreOrders.Where(r => r.StoreId == storePrice.StoreId).Foreach(t =>
                {
                    skuStoreTotalAmount += t.ProductSkuItems
                        .Where(r => skuMoneyPriceStyleIds.Contains(r.PriceStyleId)).Sum(r => r.Amount);
                });

                // 根据店铺sku价格比例设置使员的虚拟资产
                var reduceMoney = Math.Round(reduceTotalAmount * skuStoreTotalAmount / skuTotalAmount, 4);
                // 根据虚拟资产计算服务费
                //var priceStyleConfig= priceStyleConfigs.FirstOrDefault(r=>r.Id==)
                //feeAmount = reduceTotalAmount * orderConfig.FeeRate;

                storePayment = Math.Round(storePayment - reduceMoney * moneyItemConfig.RateFee, 4); // 店铺支付金额减少使用资产支付的部分

                var reduceCashAmount = new ReduceAmount
                {
                    Amount = reduceMoney,
                    ForCashAmount = reduceMoney * moneyItemConfig.RateFee,
                    MoneyName = moneyItemConfig.Name,
                    FeeAmount = reduceMoney * moneyItemConfig.ServiceRateFee,
                    MoneyTypeId = moneyItemConfig.Id
                };
                resultList.Add(reduceCashAmount);
            }

            return Tuple.Create(storePayment, ServiceResult.Success, resultList);
        }

        #region 获取sku的支付信息，以及人民币付款

        /// <summary>
        ///     Gets the order product payment price.
        ///     获取sku的支付信息，以及人民币付款
        ///     获取订单商品
        /// </summary>
        /// <param name="storeOrderSku">The store order sku.</param>
        /// <param name="storeOrders">The store orders.</param>
        /// <param name="orderBuyInput">The order buy input.</param>
        private Tuple<decimal, ServiceResult, ReduceAmount> GetOrderProductPaymentPrice(
            StoreOrderProductSkuItem storeOrderSku, IList<StoreOrderItem> storeOrders, BuyInput orderBuyInput)
        {
            var result = ServiceResult.Success;
            var priceStyleConfigs =
                Resolve<IAutoConfigService>().GetList<PriceStyleConfig>(r => r.Status == Status.Normal);
            // 根据sku获取价格模式，在根据价格模式获取虚拟资产
            var skuPriceStyle = priceStyleConfigs.FirstOrDefault(r => r.Id == storeOrderSku.PriceStyleId);
            if (skuPriceStyle == null)
                return Tuple.Create(0m, ServiceResult.FailedWithMessage("sku的价格模式已不存在"), new ReduceAmount());

            var moneyConfig = Resolve<IAutoConfigService>().MoneyTypes()
                .FirstOrDefault(r => r.Id == skuPriceStyle.MoneyTypeId);
            if (moneyConfig == null)
                return Tuple.Create(0m, ServiceResult.FailedWithMessage("sku的货币类型不正常"), new ReduceAmount());
            // 现金商城，支付金额与商品金额相等
            if (skuPriceStyle.Id == Guid.Parse("e0000000-1478-49bd-bfc7-e73a5d699000"))
            {
                var reduceCashAmount = new ReduceAmount
                {
                    Amount = 0,
                    ForCashAmount = 0,
                    MoneyName = moneyConfig.Name,
                    FeeAmount = 0,
                    MoneyTypeId = moneyConfig.Id
                };
                return Tuple.Create(storeOrderSku.Amount, ServiceResult.Success, reduceCashAmount);
            }

            // 根据sku商城模式，获取所有使用同种货币类型的
            var skuMoneyPriceStyleIds = priceStyleConfigs.Where(r => r.MoneyTypeId == skuPriceStyle.MoneyTypeId)
                .Select(r => r.Id);
            // 货币类型相同的Sku总价格
            var skuTotalAmount = 0.0m;
            storeOrders.Foreach(t =>
            {
                skuTotalAmount += t.ProductSkuItems.Where(r => skuMoneyPriceStyleIds.Contains(r.PriceStyleId))
                    .Sum(r => r.Amount);
            });
            // 总共使用的虚拟资产
            var reduceTotalAmount = orderBuyInput.ReduceMoneys.Where(r => r.Key == skuPriceStyle.MoneyTypeId)
                .Sum(r => r.Value);
            // 根据虚拟资产计算服务费(总服务费)(sku服务费)
            //(虚拟资产使用*比例),比如使用了100积分支付，虚拟资产服务费比例：10% 则抵现资金为：100*10%=10元
            var feeTotalAmount = reduceTotalAmount * moneyConfig.ServiceRateFee; // 总服务费
            var feeAmount = Math.Round(feeTotalAmount * storeOrderSku.Amount / skuTotalAmount, 4); // 按照比例设定服务费
            // 根据sku价格比例设置使员的虚拟资产
            var reduceMoney = Math.Round(reduceTotalAmount * storeOrderSku.Amount / skuTotalAmount, 4);
            //支付价格=商品总价-虚拟资产抵现费用+服务费
            var skuPayment =
                Math.Round(storeOrderSku.Amount - reduceMoney / storeOrderSku.Count * moneyConfig.RateFee + feeAmount,
                    4);
            if (skuPayment < 0)
                return Tuple.Create(0m, ServiceResult.FailedWithMessage("sku价格计算错误"), new ReduceAmount());

            var reduceAmount = new ReduceAmount
            {
                Amount = reduceMoney,
                ForCashAmount = reduceMoney * moneyConfig.RateFee,
                MoneyName = moneyConfig.Name,
                FeeAmount = feeAmount, // 服务费
                MoneyTypeId = moneyConfig.Id
            };
            return Tuple.Create(skuPayment, ServiceResult.Success, reduceAmount);
        }

        #endregion 获取sku的支付信息，以及人民币付款

        #endregion 订单处理私有辅助函数
    }
}