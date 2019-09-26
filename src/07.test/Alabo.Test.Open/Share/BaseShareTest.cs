using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

using Alabo.App.Core.Tasks;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.Framework.Basic.Notifications;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Open.Share
{
    public class BaseShareTest : CoreTest
    {
        private void HandleQueueAsync(ITaskActuator taskActuator,
            TaskManager taskManager, long queueId)
        {
            var shareOrder = Resolve<IShareOrderService>().GetSingle(r => r.Id == queueId);

            if (shareOrder == null)
            {
                throw new MessageQueueHandleException(queueId, $"shareOrder queue with id {queueId} not found.");
            }

            try
            {
                if (shareOrder.Status != ShareOrderStatus.Pending)
                {
                    throw new MessageQueueHandleException(queueId, "分润订单状态不是待处理状态");
                }

                var moduleTypeArray = taskManager.GetModulePriceArray();
                foreach (var type in moduleTypeArray)
                {
                    if (shareOrder.TriggerType == TriggerType.Order)
                    {
                        taskActuator.ExecuteTask(type, shareOrder,
                            new {ShareOrderId = queueId, shareOrder.TriggerType, OrderId = shareOrder.EntityId});
                    }
                    else
                    {
                        taskActuator.ExecuteTask(type, shareOrder,
                            new {ShareOrderId = queueId, shareOrder.TriggerType, BaseFenRunAmount = shareOrder.Amount});
                    }
                }

                //更新成功
                Resolve<IShareOrderService>().Update(r =>
                {
                    r.Status = ShareOrderStatus.Handled;
                    r.UpdateTime = DateTime.Now;
                }, r => r.Id == shareOrder.Id);
            }
            catch (Exception ex)
            {
                //处理失败
                Resolve<IShareOrderService>().Update(r =>
                {
                    r.Status = ShareOrderStatus.Error;
                    r.UpdateTime = DateTime.Now;
                    shareOrder.Summary = ex.Message;
                }, r => r.Id == shareOrder.Id);
            }
        } /*end*/

        [Fact]
        public void ServiceTest()
        {
            var taskManager = Services.GetService<TaskManager>();
            Assert.NotNull(taskManager);
            var taskContext = Services.GetService<TaskContext>();
            Assert.NotNull(taskContext);
            var taskModuleFactory = Services.GetService<TaskModuleFactory>();
            Assert.NotNull(taskModuleFactory);
            var taskActuator = Services.GetService<ITaskActuator>();
            Assert.NotNull(taskActuator);
        }

        [Fact]
        public void Start()
        {
            var taskActuator = Services.GetService<ITaskActuator>();
            var taskManager = Services.GetService<TaskManager>();

            var unHandledIdList = Resolve<IShareOrderService>().GetUnHandledIdList();

            if (unHandledIdList != null && unHandledIdList.Count > 0)
            {
                var i = 0;
                foreach (var id in unHandledIdList)
                {
                    i++;
                    try
                    {
                        HandleQueueAsync(taskActuator, taskManager, id);
                        Console.WriteLine(
                            $"shareOrder id {id} sucess.index:{i}/{unHandledIdList.Count}. {DateTime.Now}");
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
    }
}