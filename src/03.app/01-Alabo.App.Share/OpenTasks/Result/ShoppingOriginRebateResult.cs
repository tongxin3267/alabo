using System.Collections.Generic;
using Alabo.App.Asset.Accounts.Domain.Services;
using Alabo.App.Asset.Bills.Domain.Entities;
using Alabo.App.Asset.Bills.Domain.Services;
using Alabo.App.Share.TaskExecutes;
using Alabo.Data.Things.Orders.Extensions;
using Alabo.Data.Things.Orders.ResultModel;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Tasks.Queues.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Alabo.App.Share.OpenTasks.Result {

    public class ShoppingOriginRebateResult : ITaskResult {
        public TaskContext Context { get; private set; }

        private readonly TaskManager _taskManager;

        public List<Bill> BillList = new List<Bill>();

        public ShoppingOriginRebateResult(TaskContext context) {
            Context = context;
            _taskManager = Context.HttpContextAccessor.HttpContext.RequestServices.GetService<TaskManager>();
        }

        public ExecuteResult Update() {
            foreach (var item in BillList) {
                var userAccout = Alabo.Helpers.Ioc.Resolve<IAccountService>().GetAccount(item.UserId, item.MoneyTypeId);
                if (userAccout == null) {
                    continue;
                }
                userAccout.Amount = item.Amount;
                userAccout.HistoryAmount += (item.Flow == AccountFlow.Income ? item.Amount : 0);
                Alabo.Helpers.Ioc.Resolve<IAccountService>().Update(userAccout);
                item.AfterAmount = userAccout.Amount;
                Alabo.Helpers.Ioc.Resolve<IBillService>().Add(item);
            }
            return ExecuteResult.Success();
        }
    }
}