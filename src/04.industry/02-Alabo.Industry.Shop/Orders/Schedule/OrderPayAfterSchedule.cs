using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Quartz;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.Domain.Repositories;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Dependency;
using Alabo.Schedules;
using Alabo.Schedules.Enum;
using Alabo.Schedules.Job;

namespace Alabo.App.Shop.Order.Schedule {

    /// <summary>
    ///     订单支付成功后处理任务
    ///     处理的任务
    /// </summary>
    public class OrderPayAfterSchedule : JobBase {

        protected override async Task Execute(IJobExecutionContext context, IScope scope) {
            var shareOrderService = scope.Resolve<IShareOrderService>();
            var repository = scope.Resolve<IShareOrderRepository>();
            //获取刚刚支付成功的订单
            var userRegShareOrders = shareOrderService.GetList(r =>
                r.SystemStatus == ShareOrderSystemStatus.Pending && r.TriggerType == TriggerType.Order);
            foreach (var shareOrder in userRegShareOrders) {
                //TODO 2019年9月23日 重构 更新团队和团队业绩
                //  scope.Resolve<IOrderAdminService>().UpdateUserSaleInfo(shareOrder.EntityId);
                // 更新会员本身的团队信息 LevelNumber, ChildNode, TeamNumber 字段
                //  scope.Resolve<IUserMapService>().UpdateTeamInfo(shareOrder.UserId);

                // 更新会员更新用户团队的等级信息 UserMap.GradeInfo字段
                //    scope.Resolve<IUserMapService>().UpdateUserTeamGrade(shareOrder.UserId);

                //团队等级自动更新模块
                var taskQueue = new TaskQueue {
                    UserId = shareOrder.Id,
                    ModuleId = TaskQueueModuleId.TeamUserGradeAutoUpdate, //团队等级自动更新模块
                    Type = TaskQueueType.Once
                };
                scope.Resolve<ITaskQueueService>().Add(taskQueue);

                //    更新订单状态
                shareOrder.SystemStatus = ShareOrderSystemStatus.OrderHandled;
                shareOrder.ExecuteCount = shareOrder.ExecuteCount + 1;
                shareOrder.UpdateTime = DateTime.Now;
                shareOrderService.Update(shareOrder);
            }
        }
    }
}