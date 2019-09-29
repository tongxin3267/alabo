using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.Common.Domain.Services
{
    public class ICircleServiceTests : CoreTest
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
        } /*end*/

        [Fact]
        [TestMethod("GetListByCityId_Int64")]
        [TestIgnore]
        public void GetListByCityId_Int64_test()
        {
            //var cityId = 0;
            //var result = Service<ICircleService>().GetListByCityId( cityId);
            //Assert.NotNull(result);
        }
    }
}