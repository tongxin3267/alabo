using Alabo.App.Share.TaskExecutes;
using Alabo.App.Share.TaskExecutes.Extensions;
using Alabo.Data.Things.Orders.Extensions;
using Alabo.Test.Base.Core.Model;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Alabo.Test.Open.Tasks
{
    public class ManagerTest : CoreTest
    {
        [Fact]
        public void Test()
        {
            var taskManager = Services.GetService<TaskManager>();
            Assert.NotNull(taskManager);
            var taskContext = Services.GetService<TaskContext>();
            Assert.NotNull(taskContext);
            var taskModuleFactory = Services.GetService<TaskModuleFactory>();
            Assert.NotNull(taskModuleFactory);
            var taskActuator = Services.GetService<ITaskActuator>();
            Assert.NotNull(taskActuator);
        } /*end*/
    }
}