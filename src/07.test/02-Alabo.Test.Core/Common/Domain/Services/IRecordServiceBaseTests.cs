using Xunit;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Common.Domain.Services
{
    public class IRecordServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IRecordService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IRecordService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IRecordService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IRecordService>().GetList();
            var countList = Resolve<IRecordService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}