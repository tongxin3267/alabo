using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Order.Domain.Entities.Extensions;
using Alabo.App.Shop.Order.Domain.PcDtos;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Mapping;

namespace Alabo.App.Shop.Order.Domain.Services {

    public class OrderApiService : ServiceBase, IOrderApiService {

        public OrderApiService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public PagedList<ApiOrderListOutput> GetPageList(object query, ExpressionQuery<Entities.Order> expressionQuery) {
            //query.PageIndex = (int)orderInput.PageIndex;
            // query.PageSize = (int)orderInput.PageSize;
            expressionQuery.OrderByDescending(e => e.Id);
            //所有的绑定都使用快照数据，而不是使用当前数据库中的数据
            var orders = Resolve<IOrderService>().GetPagedList(query, expressionQuery.BuildExpression());

            var model = new List<ApiOrderListOutput>();
            if (orders.Count < 0) {
                return new PagedList<ApiOrderListOutput>();
            }

            foreach (var item in orders) {
                var listOutput = new ApiOrderListOutput {
                    CreateTime = item?.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    Id = item.Id,
                    OrderStatuName = item?.OrderStatus.GetDisplayName(),
                    User = item?.OrderExtension?.User,
                    Address = item?.OrderExtension?.UserAddress?.Address,
                };
                if (item.OrderExtension != null) {
                    if (item.OrderExtension.UserAddress != null) {
                        listOutput.RegionName = Resolve<IRegionService>()
                            .GetRegionNameById(item.OrderExtension.UserAddress.RegionId);
                    }
                } else {
                    listOutput.RegionName = null;
                }
                listOutput = AutoMapping.SetValue(item, listOutput);
                var orderExtension = item.Extension.DeserializeJson<OrderExtension>();
                if (orderExtension != null && orderExtension.Store != null) {
                    listOutput.StoreName = orderExtension.Store.Name;
                    listOutput.ExpressAmount = orderExtension.OrderAmount.ExpressAmount;
                }

                listOutput.OutOrderProducts = orderExtension.ProductSkuItems;
                listOutput.OutOrderProducts?.ToList().ForEach(c => {
                    c.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(c.ThumbnailUrl);
                });
                model.Add(listOutput);
            }

            return PagedList<ApiOrderListOutput>.Create(model, orders.RecordCount, orders.PageSize, orders.PageIndex);
        }
    }
}