using Alabo.Industry.Cms.Articles.Domain.Services;
using Xunit;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Cms.Articles.Domain.Services
{
    public class IAboutServiceTests : CoreTest
    {
        [Fact]
        [TestMethod("InitialData")]
        public void InitialData_test()
        {
            Resolve<IAboutService>().InitialData();
        } /*end*/
    }
}