using Xunit;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Finance.Domain.Services
{
    public class IAccountServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IAccountService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IAccountService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IAccountService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IAccountService>().GetList();
            var countList = Resolve<IAccountService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}