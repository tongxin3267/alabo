using Alabo.Data.People.Users.Domain.Services;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.User.Domain.Services
{
    public class IGradeInfoServiceTest : CoreTest
    {
        [Fact]
        public void UpdateAllUser_Test()
        {
            Resolve<IGradeInfoService>().UpdateAllUser();
        }

        [Fact]
        public void UpdateUserTeamGrade_test()
        {
            var user = Resolve<IUserService>().GetSingle("admin");
            if (user != null) Resolve<IGradeInfoService>().UpdateSingle(user.Id);
        }
    }
}