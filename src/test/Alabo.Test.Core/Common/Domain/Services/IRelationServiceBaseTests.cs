using Xunit;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Common.Domain.Services
{
    public class IRelationServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IRelationService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IRelationService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IRelationService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IRelationService>().GetList();
            var countList = Resolve<IRelationService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}