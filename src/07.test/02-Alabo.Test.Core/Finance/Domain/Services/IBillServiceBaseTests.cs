using Xunit;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Finance.Domain.Services
{
    public class IBillServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IBillService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IBillService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IBillService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IBillService>().GetList();
            var countList = Resolve<IBillService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}