using Xunit;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.UserType.Domain.Services
{
    public class IPartnerServiceTests : CoreTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
        } /*end*/

        [Fact]
        [TestMethod("AddOrUpdate_PartnerView")]
        public void AddOrUpdate_PartnerView_test()
        {
            //PartnerView partnerView = null;
            //var result = Service<IPartnerService>().AddOrUpdate(partnerView);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Delete_Int64")]
        public void Delete_Int64_test()
        {
            //var id = 0;
            //var result = Service<IPartnerService>().Delete(id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPageList_Object")]
        public void GetPageList_Object_test()
        {
            //object query = null;
            //var result = Service<IPartnerService>().GetPageList(query);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPartnerUserType_Int64")]
        public void GetPartnerUserType_Int64_test()
        {
            //var regionId = 0;
            //var result = Service<IPartnerService>().GetPartnerUserType(regionId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetView_Int64")]
        public void GetView_Int64_test()
        {
            //var id = 0;
            //var result = Service<IPartnerService>().GetView(id);
            //Assert.NotNull(result);
        }
    }
}