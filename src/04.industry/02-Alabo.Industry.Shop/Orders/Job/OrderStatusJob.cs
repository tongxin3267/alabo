﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Alabo.App.Shop.Order.Domain.CallBacks;
using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.App.Shop.Product.Domain.Services;
using Alabo.Dependency;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Helpers;
using Alabo.Schedules.Job;

namespace Alabo.App.Shop.Order.Job {

    /// <summary>
    /// 定时修改订单状态 每小时执行一次
    /// </summary>
    public class OrderStatusJob : JobBase {

        /// <summary>
        /// 每小时执行一次
        /// </summary>
        /// <returns></returns>
        public override TimeSpan? GetInterval() {
            return TimeSpan.FromHours(1);
        }

        /// <summary>
        /// 每小时检索待付款以及待收货的订单并且判断是否修改状态
        /// </summary>
        /// <param name="context"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        protected override Task Execute(IJobExecutionContext context, IScope scope) {
            var orders = scope.Resolve<IOrderService>()
                .GetListExtension(u => u.OrderStatus == OrderStatus.WaitingBuyerPay || u.OrderStatus == OrderStatus.WaitingReceiptProduct);
            if (orders.Count > 0) {
                var orderConfig = scope.Resolve<IAutoConfigService>().GetValue<OrderConfig>();
                foreach (var item in orders) {
                    TimeSpan ts = DateTime.Now.Subtract(item.CreateTime);
                    if (item.OrderExtension == null || item.OrderExtension.ProductSkuItems == null) {
                        continue;
                    }
                    //待付款定时关闭
                    if (item.OrderStatus == OrderStatus.WaitingBuyerPay) {
                        if (ts.Hours >= orderConfig.OrderClosedHour) {
                            item.OrderStatus = OrderStatus.Closed;

                            #region 根据订单内商品 增加对应库存

                            //先获取skuId的list
                            var skuList = new List<long>();

                            foreach (var view in item.OrderExtension.ProductSkuItems) {
                                skuList.Add(view.ProductSkuId);
                            }
                            //根据获取的skulist来获取数据 减少循环数量
                            var productSkus = scope.Resolve<IProductSkuService>().GetList(skuList);
                            foreach (var view in productSkus) {
                                var buyCount =
                                    item.OrderExtension.ProductSkuItems.FirstOrDefault(u => u.ProductSkuId == view.Id).BuyCount;
                                long stock = view.Stock + buyCount;
                                view.Stock = stock;
                                scope.Resolve<IProductSkuService>().Update(view);
                            }

                            #endregion 根据订单内商品 增加对应库存

                            scope.Resolve<IOrderService>().Update(item);
                            var orderAction = new OrderAction {
                                Intro = "买家未在指定时间内付款，系统已关闭订单",
                                OrderId = item.Id,
                                ActionUserId = item.UserId,
                                OrderActionType = OrderActionType.AdminClose
                            };

                            scope.Resolve<IOrderActionService>().Add(orderAction);
                        }
                    }
                    //待收货定时确认收货
                    if (item.OrderStatus == OrderStatus.WaitingReceiptProduct) {
                        if (ts.Days >= orderConfig.OrderTakeDay) {
                            item.OrderStatus = OrderStatus.WaitingEvaluated;
                            scope.Resolve<IOrderService>().Update(item);
                            var orderAction = new OrderAction {
                                Intro = "买家15天内未确认收货，系统已自动确认收货",
                                OrderId = item.Id,
                                ActionUserId = item.UserId,
                                OrderActionType = OrderActionType.AdminClose
                            };

                            scope.Resolve<IOrderActionService>().Add(orderAction);
                        }
                    }
                }
            }
            return Task.FromResult(true);
        }
    }
}