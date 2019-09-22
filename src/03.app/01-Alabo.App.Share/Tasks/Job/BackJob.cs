using Quartz;
using System;
using System.Threading.Tasks;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.Dependency;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Schedules;
using Alabo.Schedules.Job;

namespace Alabo.App.Core.Tasks.Job {

    /// <summary>
    ///     后台任务执行
    /// </summary>
    public class BackJob : JobBase {

        public override TimeSpan? GetInterval() {
            return TimeSpan.FromMinutes(1);
        }

        protected override async Task Execute(IJobExecutionContext context, IScope scope) {
            var backJobTaskQueues = Ioc.Resolve<ITaskQueueService>().GetBackJobPendingList();
            foreach (var taskQueue in backJobTaskQueues) {
                // 设置执行时间
                taskQueue.ExecutionTime = DateTime.Now;
                taskQueue.Status = QueueStatus.Processing;
                Ioc.Resolve<ITaskQueueService>().Update(taskQueue);

                // 开始执行
                ExecuteBackJob(taskQueue);

                //更新执行完成时间
                taskQueue.HandleTime = DateTime.Now;
                taskQueue.Status = QueueStatus.Handled;
                taskQueue.ExecutionTimes += 1;
                Ioc.Resolve<ITaskQueueService>().Update(taskQueue);
            }
        }

        private void ExecuteBackJob(TaskQueue taskQueue) {
            var backJobParameter = taskQueue.Parameter.ToObject<BackJobParameter>();
            if (backJobParameter != null) {
                if (backJobParameter.Parameter.IsNullOrEmpty()) {
                    // 无参数方法
                    Linq.Dynamic.DynamicService.ResolveMethod(backJobParameter.ServiceName, backJobParameter.Method);
                } else {
                    // 有参数方法
                    Linq.Dynamic.DynamicService.ResolveMethod(backJobParameter.ServiceName, backJobParameter.Method, backJobParameter.Parameter);
                }
            }
        }
    }
}