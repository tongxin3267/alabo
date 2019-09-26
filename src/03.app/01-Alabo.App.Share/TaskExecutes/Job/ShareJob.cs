using Microsoft.AspNetCore.Http;
using Quartz;
using System;
using System.Threading.Tasks;
using Alabo.App.Core.Common.Domain;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.Domain.Repositories;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.Core.Admins.Configs;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Dependency;
using Alabo.Runtime;
using Alabo.Schedules.Job;

namespace Alabo.App.Core.Tasks.Job {

    public class ShareJob : JobBase {

        /// <summary>
        /// 获取重复执行间隔时间，单位：秒
        /// </summary>
        public override int? GetIntervalInMinutes() {
            return 3;
        }

        protected override async Task Execute(IJobExecutionContext context, IScope scope) {
            var taskActuator = scope.Resolve<ITaskActuator>();
            var taskManager = scope.Resolve<TaskManager>();

            var httpContextAccessor = scope.Resolve<IHttpContextAccessor>();
            if (httpContextAccessor != null) {
                httpContextAccessor.HttpContext = new DefaultHttpContext {
                    RequestServices = scope.Resolve<IServiceProvider>(),
                };
            }
            // 平台暂停分润
            if (RuntimeContext.Current.WebsiteConfig.IsDevelopment == false) {
                var adminCenterConfig = scope.Resolve<IAutoConfigService>().GetValue<AdminCenterConfig>();
                if (adminCenterConfig.StartFenrun == false) {
                    return;
                }
            }
            var unHandledIdList = scope.Resolve<IShareOrderRepository>().GetUnHandledIdList();

            if (unHandledIdList != null && unHandledIdList.Count > 0) {
                foreach (var id in unHandledIdList) {
                    HandleQueueAsync(taskActuator, taskManager, id, scope);
                }
            }
        }

        private void HandleQueueAsync(ITaskActuator taskActuator, TaskManager taskManager, long queueId, IScope scope) {
            var shareOrder = scope.Resolve<IShareOrderService>().GetSingle(r => r.Id == queueId);

            if (shareOrder != null) {
                try {
                    if (shareOrder.Status != ShareOrderStatus.Pending) {
                        throw new MessageQueueHandleException(queueId, "分润订单状态不是待处理状态");
                    }

                    var order = scope.Resolve<IShareOrderRepository>().GetSingleNative(shareOrder.Id);
                    if (order.Status != ShareOrderStatus.Pending) {
                        throw new MessageQueueHandleException(queueId, "分润订单状态不是待处理状态");
                    }

                    var moduleTypeArray = taskManager.GetModulePriceArray();
                    foreach (var type in moduleTypeArray) {
                        if (shareOrder.TriggerType == TriggerType.Order) {
                            // 确认收货的时候，产生分润
                            if (shareOrder.SystemStatus != ShareOrderSystemStatus.Pending) {
                                continue;
                            }
                            taskActuator.ExecuteTask(type, shareOrder,
                                new { ShareOrderId = queueId, shareOrder.TriggerType, OrderId = shareOrder.EntityId });
                        } else {
                            taskActuator.ExecuteTask(type, shareOrder,
                                new { ShareOrderId = queueId, shareOrder.TriggerType, BaseFenRunAmount = shareOrder.Amount });
                        }
                    }

                    //更新成功
                    scope.Resolve<IShareOrderService>().Update(r => {
                        r.Status = ShareOrderStatus.Handled;
                        r.ExecuteCount += 1;
                        r.UpdateTime = DateTime.Now;
                    }, r => r.Id == shareOrder.Id);
                    //原生数据库才插入一次
                    var unitOfWork = scope.Resolve<IUnitOfWork>();
                    ShareOrderRepository shareOrderRepository = new ShareOrderRepository(unitOfWork);
                    shareOrderRepository.SuccessOrder(shareOrder.Id);
                } catch (Exception ex) {
                    //处理失败
                    scope.Resolve<IShareOrderService>().Update(r => {
                        r.Status = ShareOrderStatus.Error;
                        r.UpdateTime = DateTime.Now;
                        shareOrder.Summary = ex.Message;
                    }, r => r.Id == shareOrder.Id);
                }
            }
        }
    }
}