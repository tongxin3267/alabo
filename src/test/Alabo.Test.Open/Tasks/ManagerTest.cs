using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Alabo.App.Core.Tasks;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.Test.Base.Core.Model;

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