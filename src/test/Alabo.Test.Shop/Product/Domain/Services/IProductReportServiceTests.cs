using Xunit;
using Alabo.App.Shop.Product.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Shop.Product.Domain.Services
{
    public class IProductReportServiceTests : CoreTest
    {
        /*end*/

        [Fact]
        [TestMethod("GetProductBaseReportPage_Object")]
        public void GetProductBaseReportPage_Object_test()
        {
            object query = null;
            var result = Resolve<IProductReportService>().GetProductBaseReportPage(query);
            Assert.NotNull(result);
        }
    }
}