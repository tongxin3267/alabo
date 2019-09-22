using Microsoft.AspNetCore.Http;
using Quartz;
using System;
using System.Threading.Tasks;
using Alabo.App.Core.Common.Domain.CallBacks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.Dependency;
using Alabo.Runtime;
using Alabo.Schedules.Job;

namespace Alabo.App.Core.Tasks.Job {

    /// <summary>
    ///     队列方式执行
    ///     执行队列
    /// </summary>
    public class QueueTaskJob : JobBase {

        public override TimeSpan? GetInterval() {
            return TimeSpan.FromMinutes(3);
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

            var updateGradeQueue = scope.Resolve<ITaskQueueService>().GetUpgradePendingList();

            foreach (var item in updateGradeQueue) {
                var moduleTypeArray = taskManager.GetModuleUpgradeArray();
                foreach (var type in moduleTypeArray) {
                    taskActuator.ExecuteQueue(type, item, new { QueueId = item.Id });
                }
            }
        }
    }
}