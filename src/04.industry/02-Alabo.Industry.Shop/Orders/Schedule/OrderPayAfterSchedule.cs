using System;
using System.Threading.Tasks;
using Alabo.Data.Things.Orders.Domain.Repositories;
using Alabo.Data.Things.Orders.Domain.Services;
using Alabo.Dependency;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Tasks.Queues.Domain.Entities;
using Alabo.Framework.Tasks.Queues.Domain.Enums;
using Alabo.Framework.Tasks.Queues.Domain.Servcies;
using Alabo.Schedules;
using Alabo.Schedules.Job;
using Quartz;

namespace Alabo.Industry.Shop.Orders.Schedule
{
    /// <summary>
    ///     订单支付成功后处理任务
    ///     处理的任务
    /// </summary>
    public class OrderPayAfterSchedule : JobBase
    {
        protected override async Task Execute(IJobExecutionContext context, IScope scope)
        {
            var shareOrderService = scope.Resolve<IShareOrderService>();
            var repository = scope.Resolve<IShareOrderRepository>();
            //获取刚刚支付成功的订单
            var userRegShareOrders = shareOrderService.GetList(r =>
                r.SystemStatus == ShareOrderSystemStatus.Pending && r.TriggerType == TriggerType.Order);
            foreach (var shareOrder in userRegShareOrders)
            {
                //TODO 2019年9月23日 重构 更新团队和团队业绩
                //  scope.Resolve<IOrderAdminService>().UpdateUserSaleInfo(shareOrder.EntityId);
                // 更新会员本身的团队信息 LevelNumber, ChildNode, TeamNumber 字段
                //  scope.Resolve<IUserMapService>().UpdateTeamInfo(shareOrder.UserId);

                // 更新会员更新用户团队的等级信息 UserMap.GradeInfo字段
                //    scope.Resolve<IUserMapService>().UpdateUserTeamGrade(shareOrder.UserId);

                //团队等级自动更新模块
                var taskQueue = new TaskQueue
                {
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