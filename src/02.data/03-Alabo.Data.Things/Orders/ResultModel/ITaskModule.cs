using Alabo.Data.Things.Orders.Extensions;
using Alabo.Framework.Tasks.Queues.Models;

namespace Alabo.Data.Things.Orders.ResultModel
{
    public interface ITaskModule
    {
        TaskContext Context { get; }

        ExecuteResult<ITaskResult[]> Execute(TaskParameter parameter);
    }
}