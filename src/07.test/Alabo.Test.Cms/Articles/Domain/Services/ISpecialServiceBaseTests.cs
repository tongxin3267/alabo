using Alabo.Industry.Cms.Articles.Domain.Services;
using Xunit;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Cms.Articles.Domain.Services
{
    public class ISpecialServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<ISpecialService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<ISpecialService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<ISpecialService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<ISpecialService>().GetList();
            var countList = Resolve<ISpecialService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}