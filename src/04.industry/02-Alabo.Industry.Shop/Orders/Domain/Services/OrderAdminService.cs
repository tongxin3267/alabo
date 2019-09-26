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
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.AutoConfigs;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
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

        public ViewAdminOrder GetViewAdminOrder(long id, long UserId) {
            var view = new ViewAdminOrder();
            var order = Resolve<IOrderService>().GetSingle(e => e.Id == id);
            if (order != null) {
                view.Order = order;
                // 获取门店信息
                var store = Resolve<IShopStoreService>().GetSingle(e => e.Id == order.StoreId);
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