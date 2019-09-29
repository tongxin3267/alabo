using Alabo.Industry.Shop.Orders.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Shop.Order.Domain.Service
{
    public class IOrderServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IOrderService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IOrderService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IOrderService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IOrderService>().GetList();
            var countList = Resolve<IOrderService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}