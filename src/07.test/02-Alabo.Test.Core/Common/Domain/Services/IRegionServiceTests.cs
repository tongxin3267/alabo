using Xunit;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Framework.Basic.Address.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Common.Domain.Services {

    public class IRegionServiceTests : CoreTest {

        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId) {
            var model = Resolve<IRegionService>().GetRandom(entityId);
            if (model != null) {
                var newModel = Resolve<IRegionService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Theory]
        [InlineData(-1)]
        [TestMethod("GetCountyByRegionId_Int64")]
        public void GetCountyByRegionId_Int64_test(long entityId) {
            var model = Resolve<IRegionService>().GetRandom(entityId);
            if (model != null) {
                var result = Resolve<IRegionService>().GetCountyId(model.RegionId);
                Assert.True(result >= 0);
            }
        }

        /*end*/

        [Fact]
        [TestMethod("GetCity_Int64")]
        public void GetCity_Int64_test() {
            var cityId = 0;
            var result = Resolve<IRegionService>().GetSingle(r => r.Name == "¶«Ý¸ÊÐ");
            Assert.NotNull(result);

            var regions = Resolve<IRegionService>().GetList(r => r.ParentId == result.RegionId || r.CityId == result.RegionId);
            Assert.NotNull(regions);

            var vantAddress = Resolve<IUserAddressService>().GetVantAddress();
            Assert.NotNull(vantAddress);
        }

        [Fact]
        [TestMethod("GetCityIdByCountryId_Int64")]
        [TestIgnore]
        public void GetCityIdByCountryId_Int64_test() {
            //var id = 0;
            //var result = Service<IRegionService>().GetCityIdByCountryId(id);
            //Assert.True(result > 0);
        }

        [Fact]
        [TestMethod("GetNewId_Country_Int32_Int64")]
        [TestIgnore]
        public void GetNewId_Country_Int32_Int64_test() {
            //var country = (Alabo.Framework.Core.Enums.Enum.Country)0;
            //var level = 0;
            //var parentId = 0;
            //var result = Service<IRegionService>().GetNewId(country, level, parentId);
            //Assert.True(result > 0);
        }

        [Fact]
        [TestMethod("GetProvinceIdByCountryId_Int64")]
        [TestIgnore]
        public void GetProvinceIdByCountryId_Int64_test() {
            //var id = 0;
            //var result = Service<IRegionService>().GetProvinceIdByCountryId(id);
            //Assert.True(result > 0);
        }

        [Fact]
        [TestMethod("GetRegionName_Int64")]
        public void GetRegionName_Int64_test() {
            var areaId = 0;
            var result = Resolve<IRegionService>().GetFullName(areaId);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ImportChinaRegion")]
        public void ImportChinaRegion_test() {
            Resolve<IRegionService>().Init();
        }

        [Fact]
        [TestMethod("RegionToJson")]
        public void RegionToJson_test() {
            var result = Resolve<IRegionService>().RegionTrees();
            Assert.NotNull(result);
        }
    }
}