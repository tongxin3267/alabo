using Xunit;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.User.Domain.Services
{
    public class IUserServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IUserService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IUserService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IUserService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IUserService>().GetList();
            var countList = Resolve<IUserService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}