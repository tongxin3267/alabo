using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.Tasks;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.App.Core.Tasks.ResultModel;

namespace Alabo.App.Share.Tasks.Result {

    public class SuperiorDeductionsResult : ITaskResult {
        public TaskContext Context { get; private set; }

        private readonly TaskManager _taskManager;

        public List<Bill> BillList = new List<Bill>();

        public SuperiorDeductionsResult(TaskContext context) {
            Context = context;
            _taskManager = Context.HttpContextAccessor.HttpContext.RequestServices.GetService<TaskManager>();
        }

        public ExecuteResult Update() {
            foreach (var item in BillList) {
                var userAccout = Alabo.Helpers.Ioc.Resolve<IAccountService>().GetAccount(item.UserId, item.MoneyTypeId);
                if (userAccout == null) {
                    continue;
                }
                item.AfterAmount = userAccout.Amount;
                Alabo.Helpers.Ioc.Resolve<IBillService>().Add(item);
            }
            return ExecuteResult.Success();
        }
    }
}