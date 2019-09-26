using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.Finance.Domain.Services
{
    public class ITradeServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<ITradeService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<ITradeService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<ITradeService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<ITradeService>().GetList();
            var countList = Resolve<ITradeService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}