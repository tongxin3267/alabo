using Alabo.App.Core.Tasks.Extensions;

namespace Alabo.App.Core.Tasks.ResultModel {

    public interface ITaskModule {
        TaskContext Context { get; }

        ExecuteResult<ITaskResult[]> Execute(TaskParameter parameter);
    }
}