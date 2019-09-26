using System;
using System.Linq;
using Alabo.App.Asset.Accounts.Domain.Services;
using Alabo.App.Asset.Bills.Domain.Entities;
using Alabo.App.Asset.Bills.Domain.Services;
using Alabo.App.Share.OpenTasks.Parameter;
using Alabo.App.Share.Rewards.Domain.Entities;
using Alabo.App.Share.Rewards.Domain.Services;
using Alabo.App.Share.TaskExecutes;
using Alabo.Data.Things.Orders.Extensions;
using Alabo.Data.Things.Orders.ResultModel;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Tasks.Queues.Models;
using Alabo.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Alabo.App.Share.OpenTasks.Result
{
    public class InvoiceResult : ITaskResult
    {
        private readonly TaskManager _taskManager;

        #region invoice properties

        public FenRunResultParameter Parameter;

        #endregion invoice properties

        public InvoiceResult(TaskContext context)
        {
            Context = context;
            _taskManager = Context.HttpContextAccessor.HttpContext.RequestServices.GetService<TaskManager>();
        }

        public TaskContext Context { get; }

        public ExecuteResult Update()
        {
            var currentTime = DateTime.Now;
            var moneyType = Ioc.Resolve<IAutoConfigService>()
                .GetList<MoneyTypeConfig>()
                .FirstOrDefault(e => e.Id == Parameter.MoneyTypeId);
            if (moneyType == null) return ExecuteResult.Fail($"未找到MoneyTypeId为{Parameter.MoneyTypeId}的货币类型.");

            var userAccount = Ioc.Resolve<IAccountService>().GetAccount(Parameter.ReceiveUserId, moneyType.Id);
            var invoiceReward = new Reward
            {
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
            if (Parameter.Order != null) invoiceReward.OrderId = Parameter.Order.Id;
            Ioc.Resolve<IRewardService>().AddOrUpdate(invoiceReward);
            var invoiceBill = new Bill
            {
                Type = BillActionType.FenRun,

                AfterAmount = userAccount.Amount,
                Amount = Parameter.Amount,

                CreateTime = currentTime,

                Flow = Parameter.Amount >= 0 ? AccountFlow.Income : AccountFlow.Spending,
                Intro = Parameter.Summary,

                MoneyTypeId = moneyType.Id,

                //Remark = Parameter.Remark,
                UserId = Parameter.ReceiveUserId,
                OtherUserId = Parameter.TriggerUserId
            };
            Ioc.Resolve<IBillService>().Add(invoiceBill);
            return ExecuteResult.Success();
        }
    }
}