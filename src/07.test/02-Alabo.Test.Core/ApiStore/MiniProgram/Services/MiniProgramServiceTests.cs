using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Alabo.Tool.Payment.MiniProgram.Dtos;
using Alabo.Tool.Payment.MiniProgram.Services;
using Xunit;

namespace Alabo.Test.Core.ApiStore.MiniProgram.Services
{
    public class MiniProgramServiceTests : CoreTest
    {
        //  [Fact]
        [TestMethod("Login_LoginInput")]
        public void Login_LoginInput_test()
        {
            LoginInput loginInput = null;
            var result = Resolve<MiniProgramService>().Login(loginInput);
            Assert.NotNull(result);
        }

        // [Fact]
        [TestMethod("PubLogin_LoginInput")]
        public void PubLogin_LoginInput_test()
        {
            LoginInput loginInput = null;
            var result = Resolve<MiniProgramService>().PubLogin(loginInput);
            Assert.NotNull(result);
        }

        /*end*/
    }
}