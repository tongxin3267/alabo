using Alabo.App.Share.OpenTasks.Parameter;
using Alabo.App.Share.TaskExecutes;
using Alabo.Data.Things.Orders.Extensions;
using Alabo.Data.Things.Orders.ResultModel;
using Alabo.Framework.Tasks.Queues.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Alabo.App.Share.OpenTasks.Result {

    public class SharesValueChangeResult : ITaskResult {
        public TaskContext Context { get; private set; }

        private readonly TaskManager _taskManager;

        #region invoice properties

        public FenRunResultParameter Parameter { get; set; }

        #endregion invoice properties

        public SharesValueChangeResult(TaskContext context) {
            Context = context;
            _taskManager = Context.HttpContextAccessor.HttpContext.RequestServices.GetService<TaskManager>();
        }

        public ExecuteResult Update() {
            //var lastSharesValue = Alabo.Helpers.Ioc.Resolve<ISharesValueService>().GetLastRecored(Parameter.ModuleConfigId);
            //if (lastSharesValue == null) {
            //    // 设置初始默认值
            //    var initAmount = Parameter.ExtraDate.ToDecimal();
            //    var shareValue = new SharesValue {
            //        ConfigId = Parameter.ModuleConfigId,
            //        CreateTime = DateTime.Now,
            //        Increment = initAmount,
            //        OrderId = 0,
            //        TotalValue = initAmount,
            //        Remark = $"股权初始化值{initAmount}"
            //    };
            //    Alabo.Helpers.Ioc.Resolve<ISharesValueService>().Add(shareValue);
            //    return ExecuteResult.Success();

            //    //return ExecuteResult.Fail("未找到股权值变更分润配置信息");
            //}
            //if (Parameter.Order == null) {
            //    return ExecuteResult.Fail("未找到触发股权值变更的订单信息");
            //}
            //var shareValue2 = new SharesValue {
            //    ConfigId = lastSharesValue.ConfigId,
            //    CreateTime = DateTime.Now,
            //    Increment = Parameter.Amount,
            //    OrderId = Parameter.Order.Id,
            //    TotalValue = Parameter.Order.Amount + lastSharesValue.TotalValue,
            //    Remark = $"{Parameter.OrderUserName}消费产生新股值{Parameter.Amount}[{Parameter.Remark}]"
            //};
            //Alabo.Helpers.Ioc.Resolve<ISharesValueService>().Add(shareValue2);

            return ExecuteResult.Success();
        }
    }
}