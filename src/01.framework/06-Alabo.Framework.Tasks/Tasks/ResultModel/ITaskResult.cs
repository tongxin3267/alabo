using Alabo.App.Core.Tasks.Extensions;

namespace Alabo.App.Core.Tasks.ResultModel {

    public interface ITaskResult {
        TaskContext Context { get; }

        ExecuteResult Update();
    }
}