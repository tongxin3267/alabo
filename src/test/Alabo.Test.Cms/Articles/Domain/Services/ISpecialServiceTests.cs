using Xunit;
using Alabo.App.Cms.Articles.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Cms.Articles.Domain.Services
{
    public class ISpecialServiceTests : CoreTest
    {
        [Fact]
        [TestMethod("AddOrUpdate_Special")]
        public void AddOrUpdate_Special_test()
        {
            //Special model = new ;
            //var result = Service<ISpecialService>().AddOrUpdate(model);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPagePath_String_Boolean")]
        public void GetPagePath_String_Boolean_test()
        {
            var key = "";
            var ismobile = false;
            var result = Resolve<ISpecialService>().GetPagePath(key, ismobile);
            Assert.NotNull(result);
        } /*end*/
    }
}