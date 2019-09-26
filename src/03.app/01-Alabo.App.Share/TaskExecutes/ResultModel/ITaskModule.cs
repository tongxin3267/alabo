using Alabo.Data.Things.Orders.Extensions;
using Alabo.Data.Things.Orders.ResultModel;
using Alabo.Framework.Tasks.Queues.Models;

namespace Alabo.App.Share.TaskExecutes.ResultModel {

    public interface ITaskModule {
        TaskContext Context { get; }

        ExecuteResult<ITaskResult[]> Execute(TaskParameter parameter);
    }
}