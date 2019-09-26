using Alabo.App.Kpis.Kpis.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Open.Kpi.Domain.Services
{
    public class IKpiServiceBaseTests : CoreTest
    {
        public class IKpiServiceTests : CoreTest
        {
            [Theory]
            [InlineData(-1)]
            [TestMethod("GetSingleFromCache_Test")]
            public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
            {
                var model = Resolve<IKpiService>().GetRandom(entityId);
                if (model != null)
                {
                    var newModel = Resolve<IKpiService>().GetSingleFromCache(model.Id);
                    Assert.NotNull(newModel);
                    Assert.Equal(newModel.Id, model.Id);
                }
            }

            [Fact]
            [TestMethod("Count_Expected_Test")]
            public void Count_ExpectedBehavior()
            {
                var count = Resolve<IKpiService>().Count();
                Assert.True(count >= 0);

                var list = Resolve<IKpiService>().GetList();
                var countList = Resolve<IKpiService>().Count();
                Assert.Equal(count, countList);
            }

            /*end*/
        }
    }
}