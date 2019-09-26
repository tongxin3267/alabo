using Xunit;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.Data.People.Stores.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Shop.Store.Domain.Services
{
    public class IStoreServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IStoreService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IStoreService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IStoreService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IStoreService>().GetList();
            var countList = Resolve<IStoreService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}