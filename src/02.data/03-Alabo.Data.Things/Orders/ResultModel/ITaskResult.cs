using Alabo.Data.Things.Orders.Extensions;
using Alabo.Framework.Tasks.Queues.Models;

namespace Alabo.Data.Things.Orders.ResultModel {

    public interface ITaskResult {
        TaskContext Context { get; }

        ExecuteResult Update();
    }
}