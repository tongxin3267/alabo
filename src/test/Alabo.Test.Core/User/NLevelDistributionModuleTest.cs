using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Alabo.App.Core.Tasks;
using Alabo.Test.Base.Core.Model;

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