using Xunit;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.User.Domain.Services {

    public class IIdentityServiceTests : CoreTest {

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetSingle_Int64")]
        [TestIgnore]
        public void GetSingle_Int64_test(long userId) {
            //         var user = Service<IUserService>().GetRandom(userId);
            //         var result = Service<IIdentityService>().GetSingle( user.Id);
            //Assert.NotNull(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("IsIdentity_Int64")]
        public void IsIdentity_Int64_test(long userId) {
            var user = Resolve<IUserService>().GetRandom(userId);
            var result = Resolve<IIdentityService>().IsIdentity(user.Id);

            //var model = Resolve<IUserDetailService>().GetSingle(user.Id);
            //if (model != null) {
            //    if (model.IdentityStatus == IdentityStatus.IsChecked) {
            //        Assert.True(result);
            //    } else {
            //        Assert.False(result);
            //    }
            //}
        }

        /*end*/

        [Fact]
        [TestMethod("AddOrUpdate_Identity")]
        [TestIgnore]
        public void AddOrUpdate_Identity_test() {
            //Identity input = null;
            //var result = Service<IIdentityService>().AddOrUpdate( input);
            //Assert.NotNull(result);
        }
    }
}