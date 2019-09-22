using System;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.Helpers;

namespace Alabo.App.Core.Tasks.ResultModel {

    public class TaskQueueHandleResult : ITaskResult {
        private readonly ITaskQueueService _TaskQueueService;

        public TaskQueueHandleResult(TaskContext context, ITaskQueueService TaskQueueService) {
            Context = context;
            _TaskQueueService = TaskQueueService;
        }

        public int QueueId { get; set; }

        public TaskContext Context { get; }

        public ExecuteResult Update() {
            try {
                Ioc.Resolve<ITaskQueueService>().Handle(QueueId);
                return ExecuteResult.Success();
            } catch (Exception e) {
                return ExecuteResult.Error(e);
            }
        }
    }
}