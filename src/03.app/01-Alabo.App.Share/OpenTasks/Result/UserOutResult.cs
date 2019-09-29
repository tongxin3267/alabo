using Alabo.App.Share.TaskExecutes;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Data.Things.Orders.Extensions;
using Alabo.Data.Things.Orders.ResultModel;
using Alabo.Domains.Enums;
using Alabo.Framework.Tasks.Queues.Models;
using Alabo.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Alabo.App.Share.OpenTasks.Result
{
    public class UserOutResult : ITaskResult
    {
        private readonly TaskManager _taskManager;

        public UserOutResult(TaskContext context)
        {
            Context = context;
            _taskManager = Context.HttpContextAccessor.HttpContext.RequestServices.GetService<TaskManager>();
        }

        public long UserId { get; set; }
        public TaskContext Context { get; }

        public ExecuteResult Update()
        {
            var user = Ioc.Resolve<IUserService>().GetSingle(UserId);
            if (user != null)
            {
                user.Status = Status.Freeze;
                Ioc.Resolve<IUserService>().Update(user);
            }

            return ExecuteResult.Success();
        }
    }
}