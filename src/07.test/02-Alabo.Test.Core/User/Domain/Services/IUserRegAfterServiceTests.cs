using Xunit;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.User.Domain.Services
{
    public class IUserRegAfterServiceTests : CoreTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
        } /*end*/

        [Fact]
        [TestMethod("After_User")]
        public void After_User_test()
        {
            Users.Entities.User user = null;
            Resolve<IUserRegAfterService>().AddBackJob(user);
        }
    }
}