using Microsoft.Extensions.Logging;
using Alabo.App.Core.Tasks.Extensions;

namespace Alabo.App.Core.Tasks.ResultModel {

    public abstract class TaskModuleBase : ITaskModule {

        public TaskModuleBase(TaskContext context) {
            Context = context;
            Logger = context.LoggerFactory.CreateLogger<ITaskModule>();
        }

        protected ILogger<ITaskModule> Logger { get; }
        public TaskContext Context { get; }

        public abstract ExecuteResult<ITaskResult[]> Execute(TaskParameter parameter);

        protected ExecuteResult<ITaskResult[]> FailAndLogError(string message) {
            Logger.LogError(message);
            return ExecuteResult<ITaskResult[]>.Fail(message);
        }
    }
}