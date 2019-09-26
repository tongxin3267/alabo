using Alabo.App.Share.TaskExecutes;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Data.Things.Orders.Extensions;
using Alabo.Data.Things.Orders.ResultModel;
using Alabo.Framework.Tasks.Queues.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Alabo.App.Share.OpenTasks.Result {

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