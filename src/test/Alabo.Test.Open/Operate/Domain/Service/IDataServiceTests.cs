using Xunit;
using Alabo.App.Open.Operate.Domain.Service;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Open.Operate.Domain.Service
{
    public class IDataServiceTests : CoreTest
    {
        [Fact]
        [TestMethod("CheckOutUserMap")]
        public void CheckOutUserMap_test()
        {
            var result = Resolve<IDataService>().CheckOutUserMap();
            Assert.True(result.Succeeded);
        }

        [Fact]
        [TestMethod("UpdateAllUserMap")]
        public void UpdateAllUserMap_test()
        {
            Resolve<IDataService>().UpdateAllUserMap();
        }

        /*end*/
    }
}