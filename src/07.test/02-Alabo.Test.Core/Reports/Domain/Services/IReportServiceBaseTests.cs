using Alabo.Framework.Reports.Domain.Services;
using Xunit;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Reports.Domain.Services
{
    public class IReportServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IReportService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IReportService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IReportService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IReportService>().GetList();
            var countList = Resolve<IReportService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}