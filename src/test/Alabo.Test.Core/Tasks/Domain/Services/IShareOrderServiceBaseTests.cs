using Xunit;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Tasks.Domain.Services
{
    public class IShareOrderServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IShareOrderService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IShareOrderService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IShareOrderService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IShareOrderService>().GetList();
            var countList = Resolve<IShareOrderService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}