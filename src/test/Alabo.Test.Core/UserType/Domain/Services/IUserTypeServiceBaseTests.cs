using Xunit;
using Alabo.App.Core.UserType.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.UserType.Domain.Services
{
    public class IUserTypeServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IUserTypeService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IUserTypeService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IUserTypeService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IUserTypeService>().GetList();
            var countList = Resolve<IUserTypeService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}