using Xunit;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.ApiStore.MiniProgram.Services
{
    public class IMiniProgramServiceTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            //var model = Service<IAdminService>().GetRandom(entityId);
            //if (model != null)
            //{
            //    var newModel = Service<IAdminService>().GetSingleFromCache(model.Id);
            //    Assert.NotNull(newModel);
            //    Assert.Equal(newModel.Id, model.Id);
            //}
        } /*end*/

        [Fact]
        [TestMethod("Login_LoginInput")]
        [TestIgnore]
        public void Login_LoginInput_test()
        {
            //LoginInput loginInput = null;
            //var result = Service<IMiniProgramService>().Login( loginInput);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("PubLogin_LoginInput")]
        [TestIgnore]
        public void PubLogin_LoginInput_test()
        {
            //LoginInput loginInput = null;
            //var result = Service<IMiniProgramService>().PubLogin( loginInput);
            //Assert.NotNull(result);
        }
    }
}