using Alabo.Data.Things.Orders.Extensions;
using Alabo.Data.Things.Orders.ResultModel;
using Alabo.Framework.Tasks.Queues.Domain.Servcies;
using Alabo.Framework.Tasks.Queues.Models;
using Alabo.Helpers;
using System;

namespace Alabo.App.Share.TaskExecutes.ResultModel
{
    public class TaskQueueHandleResult : ITaskResult
    {
        private readonly ITaskQueueService _TaskQueueService;

        public TaskQueueHandleResult(TaskContext context, ITaskQueueService TaskQueueService)
        {
            Context = context;
            _TaskQueueService = TaskQueueService;
        }

        public int QueueId { get; set; }

        public TaskContext Context { get; }

        public ExecuteResult Update()
        {
            try
            {
                Ioc.Resolve<ITaskQueueService>().Handle(QueueId);
                return ExecuteResult.Success();
            }
            catch (Exception e)
            {
                return ExecuteResult.Error(e);
            }
        }
    }
}