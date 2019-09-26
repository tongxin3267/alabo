using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.Tasks;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.App.Open.Tasks.Parameter;
using Alabo.Framework.Core.Enums.Enum;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Alabo.App.Share.Share.Domain.Services;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Reward = Alabo.App.Share.Share.Domain.Entities.Reward;

namespace Alabo.App.Open.Tasks.Result {

    public class InvoiceResult : ITaskResult {
        public TaskContext Context { get; private set; }

        private readonly TaskManager _taskManager;

        #region invoice properties

        public FenRunResultParameter Parameter;

        #endregion invoice properties

        public InvoiceResult(TaskContext context) {
            Context = context;
            _taskManager = Context.HttpContextAccessor.HttpContext.RequestServices.GetService<TaskManager>();
        }

        public ExecuteResult Update() {
            var currentTime = DateTime.Now;
            var moneyType = Alabo.Helpers.Ioc.Resolve<IAutoConfigService>()
                .GetList<MoneyTypeConfig>()
                .FirstOrDefault(e => e.Id == Parameter.MoneyTypeId);
            if (moneyType == null) {
                return ExecuteResult.Fail($"未找到MoneyTypeId为{Parameter.MoneyTypeId}的货币类型.");
            }

            var userAccount = Alabo.Helpers.Ioc.Resolve<IAccountService>().GetAccount(Parameter.ReceiveUserId, moneyType.Id);
            var invoiceReward = new Reward() {
                AfterAmount = userAccount.Amount,
                CreateTime = currentTime,

                ModuleId = Parameter.ModuleId,
                MoneyTypeId = moneyType.Id,

                Amount = Parameter.Amount,
                //Remark = Parameter.Remark,
                UserId = Parameter.ReceiveUserId,

                Intro = Parameter.Summary,
                //TriggerType = Parameter.TriggerType,
                ModuleConfigId = Parameter.ModuleConfigId
            };
            if (Parameter.Order != null) {
                invoiceReward.OrderId = Parameter.Order.Id;
            }
            Alabo.Helpers.Ioc.Resolve<IRewardService>().AddOrUpdate(invoiceReward);
            var invoiceBill = new Bill() {
                Type = BillActionType.FenRun,

                AfterAmount = userAccount.Amount,
                Amount = Parameter.Amount,

                CreateTime = currentTime,

                Flow = Parameter.Amount >= 0 ? AccountFlow.Income : AccountFlow.Spending,
                Intro = Parameter.Summary,

                MoneyTypeId = moneyType.Id,

                //Remark = Parameter.Remark,
                UserId = Parameter.ReceiveUserId,
                OtherUserId = Parameter.TriggerUserId,
            };
            Alabo.Helpers.Ioc.Resolve<IBillService>().Add(invoiceBill);
            return ExecuteResult.Success();
        }
    }
}