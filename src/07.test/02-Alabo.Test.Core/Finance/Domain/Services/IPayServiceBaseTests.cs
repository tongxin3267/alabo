using Alabo.App.Asset.Pays.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.Finance.Domain.Services
{
    public class IPayServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IPayService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IPayService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IPayService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IPayService>().GetList();
            var countList = Resolve<IPayService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}