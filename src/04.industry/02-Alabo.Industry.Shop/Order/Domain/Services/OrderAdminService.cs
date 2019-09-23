using Alabo.App.Core.Common.Domain.CallBacks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Entities.Extension;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Order.Domain.Entities.Extensions;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.App.Shop.Order.Domain.Repositories;
using Alabo.App.Shop.Order.ViewModels;
using Alabo.App.Shop.Order.ViewModels.OrderEdit;
using Alabo.App.Shop.Product.Domain.CallBacks;
using Alabo.App.Shop.Product.Domain.Services;
using Alabo.App.Shop.Store.Domain.Entities.Extensions;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using Convert = System.Convert;

namespace Alabo.App.Shop.Order.Domain.Services {

    /// <summary>
    ///     订单后台操作相关 接口
    /// </summary>
    public class OrderAdminService : ServiceBase, IOrderAdminService {

        /// <summary>
        ///     根据订单和用户生成支付记录
        ///     生成的支付记录没有，支付方式，支付方式有前端返回来
        /// </summary>
        /// <param name="singlePayInput">The order.</param>
        public Tuple<ServiceResult, Pay> AddSinglePay(SinglePayInput singlePayInput) {
            var webSite = Resolve<IAutoConfigService>().GetValue<WebSiteConfig>();

            var pay = new Pay {
                EntityId = singlePayInput.Orders.Select(r => r.Id).ToJson(),
                Status = PayStatus.WaiPay,
                Type = singlePayInput.CheckoutType,

                Amount = singlePayInput.Orders.Sum(r => r.PaymentAmount),
                UserId = singlePayInput.User.Id
            };
            // 是否为管理员代付
            if (singlePayInput.IsAdminPay) {
                pay.PayType = PayType.AdminPay;
            }

            if (singlePayInput.Type == CheckoutType.Customer) {
                pay.Type = singlePayInput.Type;
            }

            var payExtension = new PayExtension {
                TradeNo =
                    singlePayInput.Orders.FirstOrDefault()
                        .Serial, // 使用第一个订单作为交易号                                              // Subject = order.OrderExtension?.Product?.Name, // 支付标题为商品名称
                TotalAmount = singlePayInput.Orders.Sum(r => r.PaymentAmount),
                Body = $"您正在{webSite.WebSiteName}商城上购买商品，请认真核对订单信息",
                // ProductCode = order.OrderExtension?.Product?.Bn,
                UserName = singlePayInput.User.GetUserName(),
                IsFromOrder = singlePayInput.IsFromOrder,
            };
            if (singlePayInput.IsGroupBuy) {
                //是否为拼团购买
                payExtension.IsGroupBuy = true;
                payExtension.BuyerCount = singlePayInput.BuyerCount;
            }

            if (singlePayInput.ExcecuteSqlList != null) {
                payExtension.ExcecuteSqlList = singlePayInput.ExcecuteSqlList;
            }
            if (singlePayInput.AfterSuccess != null) {
                payExtension.AfterSuccess = singlePayInput.AfterSuccess;
            }

            if (singlePayInput.OrderUser != null) {
                payExtension.OrderUser = singlePayInput.OrderUser;
            }
            payExtension.RedirectUrl = singlePayInput.RedirectUrl;
            if (Convert.ToInt16(singlePayInput.TriggerType) > 0) {
                payExtension.TriggerType = singlePayInput.TriggerType;
            }

            if (!singlePayInput.EntityId.IsNullOrEmpty()) {
                pay.EntityId = singlePayInput.EntityId;
            }
            IList<KeyValuePair<Guid, decimal>> acmountPay = new List<KeyValuePair<Guid, decimal>>();
            if (singlePayInput.ReduceMoneys != null && singlePayInput.ReduceMoneys.Count > 0) {
                payExtension.Note = "扣除";
                singlePayInput.ReduceMoneys.Foreach(r => {
                    payExtension.Note += " " + r.Name + r.MaxPayPrice;
                    acmountPay.Add(new KeyValuePair<Guid, decimal>(r.MoneyId, r.MaxPayPrice));
                });
            }

            pay.AccountPay = acmountPay.ToJson();

            if (!singlePayInput.RedirectUrl.IsNullOrEmpty()) {
                payExtension.RedirectUrl = singlePayInput.RedirectUrl;
            }

            pay.Extensions = payExtension.ToJson();

            if (Resolve<IPayService>().Add(pay)) {
                return Tuple.Create(ServiceResult.Success, pay);
            }

            return Tuple.Create(ServiceResult.FailedWithMessage("支付订单创建失败"), new Pay());
        }

        /// <summary>
        /// 订单管理页导出表格
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PagedList<OrderExcelOutPut> GetOrderPageList(object query) {
            var orders = Resolve<IOrderService>().GetPagedList(query, typeof(AdminOrderList));
            var model = new List<OrderExcelOutPut>();
            foreach (var item in orders) {
                var view = AutoMapping.SetValue<OrderExcelOutPut>(item);
                view.Serial = item.Serial;
                view.Order = item;
                view.Address = item.OrderExtension.UserAddress.AddressDescription;
                view.Mobile = item.OrderExtension.User.Mobile;
                view.Message = item.OrderExtension.OrderRemark.BuyerRemark;
                view.Status = item.OrderStatus;
                view.OrderStatusName = item.OrderStatus.GetDisplayName();

                model.Add(view);
            }

            orders.PageSize = model.Count;
            return PagedList<OrderExcelOutPut>.Create(model, orders.RecordCount, orders.PageSize, 1);
        }

        /// <summary>
        ///     获取订单列表
        /// </summary>
        /// <param name="query"></param>
        public PagedList<AdminOrderList> GetAdminPageList(object query) {
            //所有的绑定都使用快照数据，而不是使用当前数据库中的数据
            var orders = Resolve<IOrderService>().GetPagedList(query, typeof(AdminOrderList));

            var model = new List<AdminOrderList>();
            foreach (var item in orders) {
                var listOutput = AutoMapping.SetValue<AdminOrderList>(item);
                listOutput.Order = item;
                listOutput.Order.OrderExtension = item.Extension.DeserializeJson<OrderExtension>();
                listOutput.Serial = item.Serial;
                listOutput.User = listOutput.Order.OrderExtension.User;
                if (listOutput.User != null) {
                    var gradeConfig = Resolve<IGradeService>().GetGrade(listOutput.User.GradeId);
                    listOutput.UserName =
                        $"<img src='{gradeConfig?.Icon}' alt='{gradeConfig?.Name}' class='user-pic' style='width:18px;height:18px;' /><a class='primary-link margin-8' href='/Admin/User/Edit?id={listOutput.User?.Id}' title='{listOutput.User?.UserName}({listOutput.User?.Name}) 等级:{gradeConfig?.Name}'>{listOutput.User?.UserName}({listOutput.User?.Name})</a>";
                }

                listOutput.Order.OrderExtension.ReduceAmounts.Foreach(e => {
                    listOutput.ForCash += $"{e.MoneyName}{e.Amount}抵现{e.ForCashAmount}";
                    if (e.FeeAmount > 0) {
                        listOutput.ForCash += $"服务费{e.FeeAmount}";
                    }

                    listOutput.ForCash += "</br>";
                });

                model.Add(listOutput);
            }

            return PagedList<AdminOrderList>.Create(model, orders.RecordCount, orders.PageSize, orders.PageIndex);
        }

        public PagedList<ViewShopSale> GetShopSalePagedList(object query) {
            var pageList = Resolve<IUserMapService>().GetPagedList(query);
            var dictionary = query.DeserializeJson<Dictionary<string, string>>();
            dictionary.TryGetValue("type", out var type);
            var shopSaleList = new List<ViewShopSale>();
            var userIds = pageList.Select(r => r.UserId).Distinct().ToList();
            foreach (var item in pageList) {
                var view = new ViewShopSale();
                AutoMapping.SetValue(item, view);
                item.ShopSaleExtension = item.ShopSaleInfo.DeserializeJson<ShopSaleExtension>();
                view.ModifiedTime = item.ShopSaleExtension.ModifiedTime;
                view.Id = item.UserId; //Id=用户Id
                if (type == "user") {
                    var result = GetDictionary(item.ShopSaleExtension.UserShopSale.PriceStyleSales);
                    view.PriceStyleSales = result.Item1;
                    AutoMapping.SetValue(item.ShopSaleExtension.UserShopSale, view);
                }

                if (type == "recomend") {
                    var result = GetDictionary(item.ShopSaleExtension.RecomendShopSale.PriceStyleSales);
                    view.PriceStyleSales = result.Item1;
                    AutoMapping.SetValue(item.ShopSaleExtension.RecomendShopSale, view);
                }

                if (type == "second") {
                    var result = GetDictionary(item.ShopSaleExtension.SecondShopSale.PriceStyleSales);
                    view.PriceStyleSales = result.Item1;
                    AutoMapping.SetValue(item.ShopSaleExtension.SecondShopSale, view);
                }

                if (type == "team") {
                    var result = GetDictionary(item.ShopSaleExtension.TeamShopSale.PriceStyleSales);
                    view.PriceStyleSales = result.Item1;
                    AutoMapping.SetValue(item.ShopSaleExtension.TeamShopSale, view);
                }

                shopSaleList.Add(view);
            }

            return PagedList<ViewShopSale>.Create(shopSaleList, pageList.RecordCount, pageList.PageSize,
                pageList.PageIndex);
        }

        public PagedList<ViewPriceStyleSale> GetViewPriceStyleSalePagedList(object query) {
            var pageList = Resolve<IUserMapService>().GetPagedList(query);
            var dictionary = query.DeserializeJson<Dictionary<string, string>>();
            dictionary.TryGetValue("type", out var type);
            dictionary.TryGetValue("priceStyle", out var _priceStyleId);
            var priceStyleId = _priceStyleId.ToGuid();
            var shopSaleList = new List<ViewPriceStyleSale>();
            var userIds = pageList.Select(r => r.UserId).Distinct().ToList();
            foreach (var item in pageList) {
                var view = new ViewPriceStyleSale();
                AutoMapping.SetValue(item, view);
                item.ShopSaleExtension = item.ShopSaleInfo.DeserializeJson<ShopSaleExtension>();
                view.ModifiedTime = item.ShopSaleExtension.ModifiedTime;
                view.Id = item.UserId; //Id=用户Id
                if (type == "user") {
                    var priceStyleSales =
                        item.ShopSaleExtension.UserShopSale.PriceStyleSales.FirstOrDefault(r =>
                            r.PriceStyleId == priceStyleId);
                    AutoMapping.SetValue(priceStyleSales, view);
                }

                if (type == "recomend") {
                    var priceStyleSales =
                        item.ShopSaleExtension.RecomendShopSale.PriceStyleSales.FirstOrDefault(r =>
                            r.PriceStyleId == priceStyleId);
                    AutoMapping.SetValue(priceStyleSales, view);
                }

                if (type == "second") {
                    var priceStyleSales =
                        item.ShopSaleExtension.SecondShopSale.PriceStyleSales.FirstOrDefault(r =>
                            r.PriceStyleId == priceStyleId);
                    AutoMapping.SetValue(priceStyleSales, view);
                }

                if (type == "team") {
                    var priceStyleSales =
                        item.ShopSaleExtension.TeamShopSale.PriceStyleSales.FirstOrDefault(r =>
                            r.PriceStyleId == priceStyleId);
                    AutoMapping.SetValue(priceStyleSales, view);
                }

                shopSaleList.Add(view);
            }

            return PagedList<ViewPriceStyleSale>.Create(shopSaleList, pageList.RecordCount, pageList.PageSize,
                pageList.PageIndex);
        }

        public ViewAdminOrder GetViewAdminOrder(long id, long UserId) {
            var view = new ViewAdminOrder();
            var order = Resolve<IOrderService>().GetSingle(e => e.Id == id);
            if (order != null) {
                view.Order = order;
                // 获取门店信息
                var store = Resolve<IStoreService>().GetSingle(e => e.Id == order.StoreId);
                if (store.Extension != null) {
                    store.StoreExtension = store.Extension.DeserializeJson<StoreExtension>();
                }

                view.Order.OrderExtension.Store = store;
                view.Actions = Resolve<IOrderActionService>().GetList(e => e.OrderId == order.Id)
                    .OrderByDescending(r => r.Id).ToList();
                view.OrderProducts = Resolve<IOrderProductService>().GetList(e => e.OrderId == order.Id).ToList();
                view.OrderDeliveries = Resolve<IOrderDeliveryService>().GetOrderDeliveries(order.Id);

                //处理发货数量
                IList<ProductDeliveryInfo> productDeliveryInfos = new List<ProductDeliveryInfo>();
                if (view.OrderDeliveries.Count > 0) {
                    foreach (var item in view.OrderDeliveries) {
                        productDeliveryInfos.AddRange(item.OrderDeliveryExtension.ProductDeliveryInfo);
                    }
                    // 已发货数量
                    view.DeliveryProduct = (from ProductDeliveryInfo p in productDeliveryInfos
                                            group p by p.ProductSkuId
                        into g
                                            select new OrderEditDeliveryProduct { SkuId = g.Key, Count = g.Sum(e => e.Count) }).ToList();
                }

                //上下记录
                view.NextOrder = Resolve<IOrderService>().Next(order)?.Id;
                view.PrexOrder = Resolve<IOrderService>().Prex(order)?.Id;
                view.ShareOrder = Resolve<IShareOrderService>().GetSingle(r => r.EntityId == order.Id);
                view.User = Resolve<IUserService>().GetSingle(order.UserId);
                view.OrderExtension = order.OrderExtension;

                var isAdmin = Resolve<IUserService>().IsAdmin(UserId);
                if (isAdmin) {
                    view.Methods = Resolve<IOrderService>().GetMethodByStatus(order.OrderStatus)
                        .Where(r => r.Method.Contains("Admin")).ToList();
                } else {
                    view.Methods = Resolve<IOrderService>().GetMethodByStatus(order.OrderStatus)
                        .Where(r => r.Method.Contains("Seller")).ToList();
                }

                view.Pay = Resolve<IPayService>().GetSingle(order.PayId);
                view.OrderExtension.ReduceAmounts.Foreach(r => {
                    view.ReduceMoneyIntro += $"{r.MoneyName}:{r.Amount}(抵现{r.ForCashAmount})  ";
                });
            }

            return view;
        }

        /// <summary>
        ///     更新订单业绩
        ///     更新到表User_UserMap.ShopSale中
        /// </summary>
        /// <param name="orderId"></param>
        public void UpdateUserSaleInfo(long orderId) {
            var order = Resolve<IOrderService>().GetSingleWithProducts(orderId);
            if (order != null && order.OrderStatus != OrderStatus.WaitingBuyerPay &&
                order.OrderStatus != OrderStatus.Closed) {
                var userMap = Resolve<IUserMapService>().GetSingle(order.UserId);
                // 下单用户更新自身业绩
                userMap.ShopSaleExtension.UserShopSale =
                    SetSalfInfoByOrderId(userMap.ShopSaleExtension.UserShopSale, order); // 自身业绩
                Resolve<IUserMapService>().UpdateShopSale(order.UserId, userMap.ShopSaleExtension);

                // 上一级用户更新推荐业绩
                var userConfig = Resolve<IAutoConfigService>().GetValue<TeamConfig>();
                if (userConfig.TeamLevel < 2) {
                    userConfig.TeamLevel = 2;
                }

                foreach (var parent in userMap.ParentMapList) {
                    if (parent.ParentLevel > userConfig.TeamLevel) {
                        break;
                    }

                    var parentUserMap = Resolve<IUserMapService>().GetSingle(parent.UserId);
                    if (parentUserMap != null) {
                        // 团队业绩
                        parentUserMap.ShopSaleExtension.TeamShopSale =
                            SetSalfInfoByOrderId(parentUserMap.ShopSaleExtension.TeamShopSale, order);
                        if (parent.ParentLevel == 2) {
                            parentUserMap.ShopSaleExtension.SecondShopSale =
                                SetSalfInfoByOrderId(parentUserMap.ShopSaleExtension.SecondShopSale, order);
                        }

                        if (parent.ParentLevel == 1) {
                            parentUserMap.ShopSaleExtension.RecomendShopSale =
                                SetSalfInfoByOrderId(parentUserMap.ShopSaleExtension.RecomendShopSale, order);
                        }

                        Resolve<IUserMapService>().UpdateShopSale(parent.UserId, parentUserMap.ShopSaleExtension);
                    }
                }
            }
        }

        private Tuple<Dictionary<Guid, string>, string> GetDictionary(IEnumerable<PriceStyleSale> priceStyleSales) {
            var dictionary = new Dictionary<Guid, string>();
            var str = string.Empty;
            if (priceStyleSales != null) {
                priceStyleSales.Foreach(r => {
                    str =
                        $"订单({r.OrderCount})商品({r.ProductCount})总金额({r.PriceAmount})总分润({r.FenRunAmount})总支付({r.PaymentAmount})总服务费({r.FeeAmount})";
                    str = $"<code>{str}</code>";
                    dictionary.Add(r.PriceStyleId, str);
                    // str += $"{r.GradeName}({r.Count})  ";
                });
            }

            return Tuple.Create(dictionary, str);
        }

        private ShopSale SetSalfInfoByOrderId(ShopSale shopSale, Entities.Order order) {
            // 统计下单会员自身业绩数据
            shopSale.TotalOrderCount += 1; // 总订单数加1
            shopSale.TotalPaymentAmount += order.PaymentAmount;
            shopSale.TotalPriceAmount += order.TotalAmount;
            shopSale.TotalProductCount += order.TotalCount;
            shopSale.TotalExpressAmount += order.OrderExtension.OrderAmount.ExpressAmount;

            shopSale.TotalFeeAmount += order.OrderExtension.OrderAmount.FeeAmount; //总服务费

            var priceStyleConfigs =
                Resolve<IAutoConfigService>().GetList<PriceStyleConfig>(r => r.Status == Status.Normal);
            foreach (var priceStyle in priceStyleConfigs) {
                var productSkuItems = order.OrderExtension.ProductSkuItems.Where(r => r.PriceStyleId == priceStyle.Id);
                if (productSkuItems != null && productSkuItems.Count() > 0) {
                    // 所有商城的下单商品
                    var priceStyleProducts =
                        order.Products.Where(r => productSkuItems.Select(e => e.ProductSkuId).Contains(r.SkuId));
                    if (priceStyleProducts.Count() <= 0) {
                        continue;
                    }

                    var priceStyleSale = shopSale.PriceStyleSales.FirstOrDefault(r => r.PriceStyleId == priceStyle.Id);
                    if (priceStyleSale == null) {
                        priceStyleSale = new PriceStyleSale {
                            PriceStyleId = priceStyle.Id
                        };
                        shopSale.PriceStyleSales.Add(priceStyleSale);
                    }

                    shopSale.PriceStyleSales.Foreach(e => {
                        if (e.PriceStyleId == priceStyle.Id) {
                            e.PriceStyleName = priceStyle.Name;

                            e.OrderCount += 1; // 订单数加1
                            e.ProductCount += priceStyleProducts.Sum(r => r.Count); // 商品总数
                            e.PriceAmount += priceStyleProducts.Sum(r => r.TotalAmount); // 价格统计
                            e.FenRunAmount += priceStyleProducts.Sum(r => r.FenRunAmount); // 分润统计
                            e.PaymentAmount += priceStyleProducts.Sum(r => r.PaymentAmount); // 支付金额统计
                            e.FeeAmount +=
                                priceStyleProducts.Sum(r => r.OrderProductExtension.OrderAmount.FeeAmount); //服务费统计

                            shopSale.TotalFenRunAmount += priceStyleProducts.Sum(r => r.FenRunAmount); // 分润费用
                        }
                    });
                }
            }

            return shopSale;
        }

        public void ProductStockUpdate() {
            var orders = Repository<IOrderRepository>().GetOrders();
            if (orders.Count() > 0) {
                var orderIds = orders.Select(e => e.Id).ToList();
                var orderProducts = Resolve<IOrderProductService>().GetList(e => orderIds.Contains(e.OrderId));
                foreach (var item in orderProducts) {
                    Resolve<IProductSkuService>().Update(r => { r.Stock += item.Count; }, e => e.Id == item.SkuId);
                    Resolve<IProductService>().Update(r => { r.Stock += item.Count; }, e => e.Id == item.ProductId);
                }
                foreach (var a in orders) {
                    Resolve<IOrderService>().Update(r => { r.OrderStatus = OrderStatus.Closed; }, e => e.Id == a.Id);
                }
            }
        }

        public OrderAdminService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}