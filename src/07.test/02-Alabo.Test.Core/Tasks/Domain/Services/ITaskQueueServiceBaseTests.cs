using Alabo.Framework.Tasks.Queues.Domain.Servcies;
using Xunit;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Tasks.Domain.Services
{
    public class ITaskQueueServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<ITaskQueueService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<ITaskQueueService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<ITaskQueueService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<ITaskQueueService>().GetList();
            var countList = Resolve<ITaskQueueService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}