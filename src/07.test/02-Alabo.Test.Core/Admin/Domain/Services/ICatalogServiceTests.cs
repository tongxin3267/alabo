using Xunit;
using Alabo.Framework.Core.Admins.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Admin.Domain.Services
{
    public class ICatalogServiceTests : CoreTest
    {
        [Fact]
        [TestMethod("GetSqlTable")]
        public void GetSqlTable_test()
        {
            var result = Resolve<ICatalogService>().GetSqlTable();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("UpdateDatabase")]
        public void UpdateDatabase_test()
        {
            Resolve<ICatalogService>().UpdateDatabase();
        }

        /*end*/
    }
}