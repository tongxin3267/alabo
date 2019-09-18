using Xunit;
using Alabo.App.Cms.Support.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Cms.Support.Domain.Services
{
    public class IWorkOrderServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IWorkOrderService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IWorkOrderService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IWorkOrderService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IWorkOrderService>().GetList();
            var countList = Resolve<IWorkOrderService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}