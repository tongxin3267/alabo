using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Repositories;
using Alabo.App.Shop.Order.ViewModels;
using Alabo.App.Shop.Store.Domain.CallBacks;
using Alabo.Datas.UnitOfWorks;

using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Mapping;

namespace Alabo.App.Shop.Order.Domain.Services {

    public class OrderDeliveryService : ServiceBase<OrderDelivery, long>, IOrderDeliveryService {

        /// <summary>
        ///     根据订单ID获取发货记录
        /// </summary>
        /// <param name="orderId"></param>
        public List<OrderDelivery> GetOrderDeliveries(long orderId) {
            return Repository<IOrderDeliveryRepository>().GetList(p => p.OrderId == orderId).ToList();
        }

        /// <summary>
        /// </summary>
        /// <param name="query"></param>
        public PagedList<ViewOrderDeliveryList> GetPageList(object query) {
            var page = GetPagedList(query);
            var list = new List<ViewOrderDeliveryList>();
            var orderIds = page.Select(r => r.OrderId).ToList();
            var orders = Resolve<IOrderService>().GetList(r => orderIds.Contains(r.Id));
            var users = Resolve<IUserService>().GetList();
            var expressConfig = Resolve<IAutoConfigService>().GetList<ExpressConfig>(r => r.Status == Status.Normal);
            foreach (var item in page) {
                var viewOrderDelivery = AutoMapping.SetValue<ViewOrderDeliveryList>(item);
                var order = orders.FirstOrDefault(r => r.Id == item.OrderId);
                if (order != null) {
                    viewOrderDelivery = AutoMapping.SetValue<ViewOrderDeliveryList>(order);
                    viewOrderDelivery.Id = item.Id;
                    viewOrderDelivery.ExpressName = expressConfig.FirstOrDefault(r => r.Id == item.ExpressGuid)?.Name;
                    viewOrderDelivery.StoreName = order.OrderExtension?.Store?.Name;
                    viewOrderDelivery.UserName = users.FirstOrDefault(u => u.Id == item.UserId)?.UserName;
                    list.Add(viewOrderDelivery);
                }
            }
            return PagedList<ViewOrderDeliveryList>.Create(list, page.RecordCount, page.PageSize, page.PageIndex);
        }

        /// <summary>
        /// Gets the view order delivery edit.
        /// </summary>
        /// <param name="id">Id标识</param>

        public ViewOrderDeliveryList GetViewOrderDeliveryEdit(long id) {
            ViewOrderDeliveryList view = new ViewOrderDeliveryList();
            if (id > 0) {
                var orderDelivery = Resolve<IOrderDeliveryService>().GetSingle(id);
                var expressConfig = Resolve<IAutoConfigService>().GetList<ExpressConfig>(r => r.Status == Status.Normal);
                if (orderDelivery.Extension != null) {
                    view = AutoMapping.SetValue<ViewOrderDeliveryList>(orderDelivery);
                    view = AutoMapping.SetValue<ViewOrderDeliveryList>(orderDelivery.OrderDeliveryExtension.Order);
                    var order = Resolve<IOrderService>().GetSingle(orderDelivery.OrderId);
                    view.StoreName = order.OrderExtension?.Store?.Name;
                    view.ExpressNumber = orderDelivery.ExpressNumber;
                    view.User = Resolve<IUserService>().GetSingle(view.UserId);
                    view.ExpressName = expressConfig.FirstOrDefault(r => r.Id == orderDelivery.ExpressGuid)?.Name;
                    var productInfo = orderDelivery.OrderDeliveryExtension.ProductDeliveryInfo;
                    foreach (var item in productInfo) {
                        view.ProductName = item.Name;
                    }
                    view.Address = orderDelivery.OrderDeliveryExtension.Order.OrderExtension.UserAddress.AddressDescription;
                    view.Remark = orderDelivery.OrderDeliveryExtension.Remark;
                    //view.UserId = orderDelivery.OrderDeliveryExtension.Order.UserId;
                    //view.UserName = Service<IUserService>().GetSingle(view.UserId).UserName;
                    //view.Serial = orderDelivery.OrderDeliveryExtension.Order.Serial;
                    //view.TotalAmount = orderDelivery.OrderDeliveryExtension.Order.TotalAmount;
                    //view.TotalCount = orderDelivery.OrderDeliveryExtension.Order.TotalCount;
                }
            }
            return view;
        }

        /// <summary>
        /// 根据订单Id获取订单的发货用户Id
        /// </summary>
        /// <param name="orderId">The order identifier.</param>

        public long GetOrderDeliverUserId(long orderId) {
            var order = Resolve<IOrderService>().GetSingle(orderId);
            if (order != null) {
                return order.DeliverUserId;
            }
            return 0;
        }

        public OrderDeliveryService(IUnitOfWork unitOfWork, IRepository<OrderDelivery, long> repository) : base(unitOfWork, repository) {
        }
    }
}