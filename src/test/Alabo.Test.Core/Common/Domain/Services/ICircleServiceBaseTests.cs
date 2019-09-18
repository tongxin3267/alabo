using Xunit;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Common.Domain.Services
{
    public class ICircleServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            //var model = Service<ICircleService>().GetRandom(entityId);
            //if (model != null)
            //{
            //    var newModel = Service<ICircleService>().GetSingleFromCache(model.Id);
            //    Assert.NotNull(newModel);
            //    Assert.Equal(newModel.Id, model.Id);
            //}
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            //var count = Service<ICircleService>().Count();
            //Assert.True(count >= 0);

            //var list = Service<ICircleService>().GetList();
            //var countList = Service<ICircleService>().Count();
            //Assert.Equal(count, countList);
        } /*end*/
    }
}