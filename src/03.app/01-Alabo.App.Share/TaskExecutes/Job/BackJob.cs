using Alabo.Dependency;
using Alabo.Extensions;
using Alabo.Framework.Tasks.Queues.Domain.Entities;
using Alabo.Framework.Tasks.Queues.Domain.Enums;
using Alabo.Framework.Tasks.Queues.Domain.Servcies;
using Alabo.Helpers;
using Alabo.Linq.Dynamic;
using Alabo.Schedules;
using Alabo.Schedules.Job;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Alabo.App.Share.TaskExecutes.Job
{
    /// <summary>
    ///     后台任务执行
    /// </summary>
    public class BackJob : JobBase
    {
        public override TimeSpan? GetInterval()
        {
            return TimeSpan.FromMinutes(1);
        }

        protected override async Task Execute(IJobExecutionContext context, IScope scope)
        {
            var backJobTaskQueues = Ioc.Resolve<ITaskQueueService>().GetBackJobPendingList();
            foreach (var taskQueue in backJobTaskQueues)
            {
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

        private void ExecuteBackJob(TaskQueue taskQueue)
        {
            var backJobParameter = taskQueue.Parameter.ToObject<BackJobParameter>();
            if (backJobParameter != null)
            {
                if (backJobParameter.Parameter.IsNullOrEmpty()) // 无参数方法
                    DynamicService.ResolveMethod(backJobParameter.ServiceName, backJobParameter.Method);
                else // 有参数方法
                    DynamicService.ResolveMethod(backJobParameter.ServiceName, backJobParameter.Method,
                        backJobParameter.Parameter);
            }
        }
    }
}