using Alabo.Industry.Shop.Activitys.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Shop.Activitys.Domain.Services
{
    public class IActivityRecordServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IActivityRecordService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IActivityRecordService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IActivityRecordService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IActivityRecordService>().GetList();
            var countList = Resolve<IActivityRecordService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}