using Xunit;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Framework.Basic.Relations.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Common.Domain.Services
{
    public class IRelationIndexServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IRelationIndexService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IRelationIndexService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IRelationIndexService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IRelationIndexService>().GetList();
            var countList = Resolve<IRelationIndexService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}