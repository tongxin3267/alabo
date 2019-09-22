using Xunit;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Reports.Domain.Services
{
    public class IStatisticsServiceTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            //var model = Service<IStatisticsService>().GetRandom(entityId);
            //if (model != null)
            //{
            //    var newModel = Service<IStatisticsService>().GetSingleFromCache(model.Id);
            //    Assert.NotNull(newModel);
            //    Assert.Equal(newModel.Id, model.Id);
            //}
        } /*end*/
    }
}