using Xunit;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Shop.Order.Domain.Service
{
    public class IOrderDeliveryServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IOrderDeliveryService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IOrderDeliveryService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IOrderDeliveryService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IOrderDeliveryService>().GetList();
            var countList = Resolve<IOrderDeliveryService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}