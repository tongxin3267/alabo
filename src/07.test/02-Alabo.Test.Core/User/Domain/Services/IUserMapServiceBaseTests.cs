using Xunit;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.User.Domain.Services
{
    public class IUserMapServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IUserMapService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IUserMapService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IUserMapService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IUserMapService>().GetList();
            var countList = Resolve<IUserMapService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}