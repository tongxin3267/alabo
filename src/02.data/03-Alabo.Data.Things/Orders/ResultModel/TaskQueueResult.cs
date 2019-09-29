using Alabo.Data.Things.Orders.Extensions;
using Alabo.Framework.Tasks.Queues.Domain.Enums;
using Alabo.Framework.Tasks.Queues.Domain.Servcies;
using Alabo.Framework.Tasks.Queues.Models;
using Alabo.Helpers;
using System;

namespace Alabo.Data.Things.Orders.ResultModel
{
    /// <summary>
    ///     此结果类型输入数据新增到任务队列表中
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TaskQueueResult<T> : ITaskResult where T : class
    {
        public TaskQueueResult(TaskContext context)
        {
            Context = context;
        }

        public TaskQueueResult(TaskContext context, long queueId)
        {
            Context = context;
            QueueId = queueId;
        }

        public long UserId { get; set; }

        public Guid ModuleId { get; set; }

        public T Parameter { get; set; }

        public TaskQueueType Type { get; set; } = TaskQueueType.Once;

        public DateTime ExecutionTime { get; set; } = DateTime.Now;

        public int MaxExecutionTimes { get; set; } = 0;

        public long QueueId { get; set; }

        public ShareResult ShareResult { get; set; }
        public TaskContext Context { get; }

        public ExecuteResult Update()
        {
            try
            {
                if (typeof(T) == typeof(TaskQueueParameter))
                {
                    if (QueueId < 1) return ExecuteResult.Cancel("QueueId is empty.");

                    Ioc.Resolve<ITaskQueueService>().Handle(QueueId);
                    return ExecuteResult.Success();
                }

                if (Parameter == null) return ExecuteResult.Cancel("parameter is null.");

                Ioc.Resolve<ITaskQueueService>()
                    .Add(UserId, ModuleId, Type, ExecutionTime, MaxExecutionTimes, Parameter);
                return ExecuteResult.Success();
            }
            catch (Exception e)
            {
                return ExecuteResult.Error(e);
            }
        }
    }
}