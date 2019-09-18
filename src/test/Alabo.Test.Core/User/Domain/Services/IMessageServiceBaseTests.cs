using Xunit;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.User.Domain.Services
{
    public class IMessageServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            //var model = Service<IMessageService>().GetRandom(entityId);
            //if (model != null)
            //{
            //    var newModel = Service<IMessageService>().GetSingleFromCache(model.Id);
            //    Assert.NotNull(newModel);
            //    Assert.Equal(newModel.Id, model.Id);
            //}
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            //var count = Service<IMessageService>().Count();
            //Assert.True(count >= 0);

            //var list = Service<IMessageService>().GetList();
            //var countList = Service<IMessageService>().Count();
            //Assert.Equal(count, countList);
        } /*end*/
    }
}