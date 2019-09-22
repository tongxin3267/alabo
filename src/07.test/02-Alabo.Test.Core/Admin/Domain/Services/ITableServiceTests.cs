using Xunit;
using Alabo.App.Core.Admin.Domain.Extensions;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.App.Core.User.Domain.Entities.Extensions;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Admin.Domain.Services
{
    public class ITableServiceTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            //var model = Service<IAdminService>().GetRandom(entityId);
            //if (model != null)
            //{
            //    var newModel = Service<IAdminService>().GetSingleFromCache(model.Id);
            //    Assert.NotNull(newModel);
            //    Assert.Equal(newModel.Id, model.Id);
            //}
        } /*end*/

        [Fact]
        [TestMethod("GetSetting_String_Int64")]
        public void GetSetting_String_Int64_test()
        {
            var key = "";
            var userId = 0;
            var result = Resolve<IAdminTableService>().GetSetting(key, userId);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Settings_TableSetting_Int64")]
        public void Settings_TableSetting_Int64_test()
        {
            TableSetting tableSetting = null;
            var userId = 0;
            var result = Resolve<IAdminTableService>().Settings(tableSetting, userId);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToExcel_String_String_String_Object")]
        public void ToExcel_String_String_String_Object_test()
        {
            var key = "";
            var service = "";
            var method = "";
            object query = null;
            var result = Resolve<IAdminTableService>().ToExcel(key, service, method, query);
            Assert.NotNull(result);
        }
    }
}