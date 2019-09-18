using Microsoft.Extensions.DependencyInjection;
using Alabo.App.Core.Tasks;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.App.Core.User.Domain.Services;

namespace Alabo.App.Share.Tasks.Result {

    public class UserOutResult : ITaskResult {
        public TaskContext Context { get; private set; }

        private readonly TaskManager _taskManager;

        public long UserId { get; set; }

        public UserOutResult(TaskContext context) {
            Context = context;
            _taskManager = Context.HttpContextAccessor.HttpContext.RequestServices.GetService<TaskManager>();
        }

        public ExecuteResult Update() {
            var user = Alabo.Helpers.Ioc.Resolve<IUserService>().GetSingle(UserId);
            if (user != null) {
                user.Status = Alabo.Domains.Enums.Status.Freeze;
                Alabo.Helpers.Ioc.Resolve<IUserService>().Update(user);
            }
            return ExecuteResult.Success();
        }
    }
}