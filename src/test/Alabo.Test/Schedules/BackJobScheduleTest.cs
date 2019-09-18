using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZKCloud.App.Core.Tasks.Domain.Entities;
using ZKCloud.App.Core.Tasks.Domain.Enums;
using ZKCloud.App.Core.Tasks.Domain.Services;
using ZKCloud.Extensions;
using ZKCloud.Helpers;
using ZKCloud.Schedules;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Schedules {

    public class BackJobScheduleTest : CoreTest {

        [Fact]
        public void BackJobSchedule_Job_Test() {
            while (true) {
                IntervalExecute();

                var delayInterval = TimeSpan.FromSeconds(3);
                Task.Delay(delayInterval);
            }
        }

        protected void IntervalExecute() {
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
                Ioc.Resolve<ITaskQueueService>().UpdateNoTracking(taskQueue);
            }
        }

        private void ExecuteBackJob(TaskQueue taskQueue) {
            var backJobParameter = taskQueue.Parameter.ToObject<BackJobParameter>();
            if (backJobParameter != null && !backJobParameter.ServiceName.IsNullOrEmpty()) {
                if (backJobParameter.Parameter.IsNullOrEmpty()) {
                    // 无参数方法
                    ZKCloud.Linq.Dynamic.DynamicService.ResolveMethod(backJobParameter.ServiceName, backJobParameter.Method);
                } else {
                    // 有参数方法
                    ZKCloud.Linq.Dynamic.DynamicService.ResolveMethod(backJobParameter.ServiceName, backJobParameter.Method, backJobParameter.Parameter);
                }
            }
        }
    }
}