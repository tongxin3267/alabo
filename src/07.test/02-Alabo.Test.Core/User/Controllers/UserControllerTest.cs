using System.Threading.Tasks;
using Alabo.Data.People.Users.ViewModels;
using Xunit;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.User.Controllers
{
    /// <summary>
    /// </summary>
    public class UserControllerTest : BaseControllerTest
    {
        public void Add()
        {
        }

        /// <summary>
        ///     Logins the test.http://www.cnblogs.com/shenba/p/6417893.html
        /// </summary>
        [Fact]
        public async Task LoginTest()
        {
            var view = new ViewUserLogin
            {
                UserName = "admin",
                Password = "111111",
                VerifyCode = "01245"
            };
        } /*end*/
    }
}