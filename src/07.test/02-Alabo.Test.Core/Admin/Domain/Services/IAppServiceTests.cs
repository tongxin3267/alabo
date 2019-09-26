using Xunit;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Admin.Domain.Services
{
    public class IAppServiceTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            //var model = Service<IAppService>().GetRandom(entityId);
            //if (model != null)
            //{
            //    var newModel = Service<IAppService>().GetSingleFromCache(model.Id);
            //    Assert.NotNull(newModel);
            //    Assert.Equal(newModel.Id, model.Id);
            //}
        } /*end*/

        [Fact]
        [TestMethod("AppNameCollection")]
        public void AppNameCollection_test()
        {
            var result = Resolve<IAppService>().AppNameCollection();
            Assert.True(result.Count > 10);
            Assert.NotNull(result);
        }
    }
}