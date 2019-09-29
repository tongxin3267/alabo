using Alabo.App.Share.TaskExecutes;
using Alabo.Test.Base.Core.Model;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Alabo.Test.Core.User
{
    [TestCaseOrderer("Alabo.Test.Core.PriorityOrderer", "Alabo.Test")]
    public class NLevelDistributionModuleTest : CoreTest
    {
        [Fact]
        public void ExecuteTest()
        {
            var taskManager = Services.GetService<TaskManager>();
            var taskActuator = Services.GetService<ITaskActuator>();
            //taskActuator.ExecuteTaskAndUpdateResults(7, new { ShareOrderId = 7 });
            Assert.NotNull(taskActuator);
        } /*end*/
    }
}