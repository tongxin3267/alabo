using Alabo.Data.People.Users.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.User.Domain.Services
{
    public class IUserMapServiceTests : CoreTest
    {
        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetSingle_Int64")]
        [TestIgnore]
        public void GetSingle_Int64_test(long userId)
        {
            //         var user = Service<IUserService>().GetRandom(userId);
            //var result = Service<IUserMapService>().GetSingle( user.Id);
            //Assert.NotNull(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetTeamMap_Int64")]
        [TestIgnore]
        public void GetTeamMap_Int64_test(long userId)
        {
            //         var user = Service<IUserService>().GetRandom(userId);
            //         var result = Service<IUserMapService>().GetTeamMap( user.Id);
            //Assert.NotNull(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetList_Int64")]
        [TestIgnore]
        //����ûд
        public void GetList_Int64_test(long userId)
        {
            //         var user = Service<IUserService>().GetRandom(userId);

            //         var result = Service<IUserMapService>().GetList(user.ParentId);
            //Assert.NotNull(result);
        }

        /*end*/

        [Fact]
        [TestMethod("GetChildUserIds_Int64")]
        //����ûд
        [TestIgnore]
        public void GetChildUserIds_Int64_test()
        {
            //var parentId = 0;
            //var result = Service<IUserMapService>().GetChildUserIds( parentId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetParentMap_Int64")]
        public void GetParentMap_Int64_test()
        {
            var parentId = 0;
            var result = Resolve<IUserMapService>().GetParentMap(parentId);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetParentMapFromCache_Int64")]
        public void GetParentMapFromCache_Int64_test()
        {
            var userId = 0;
            var result = Resolve<IUserMapService>().GetParentMapFromCache(userId);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetTeamUser_UserMap")]
        [TestIgnore]
        public void GetTeamUser_UserMap_test()
        {
            //UserMap userMap = null;
            //var result = Service<IUserMapService>().GetTeamUser( userMap);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserGradeInfoPageList_Object")]
        public void GetUserGradeInfoPageList_Object_test()
        {
            object query = null;
            var result = Resolve<IUserMapService>().GetUserGradeInfoPageList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("UpdateAllUserParentMap")]
        public void UpdateAllUserParentMap_test()
        {
            Resolve<IUserMapService>().UpdateAllUserParentMap();
        }

        [Fact]
        [TestMethod("UpdateMap_Int64_Int64")]
        public void UpdateMap_Int64_Int64_test()
        {
            var userId = 0;
            var parentId = 0;
            Resolve<IUserMapService>().UpdateMap(userId, parentId);
        }

        [Fact]
        [TestMethod("UpdateShopSale_Int64_ShopSaleExtension")]
        public void UpdateShopSale_Int64_ShopSaleExtension_test()
        {
            var userId = 0;
            ShopSaleExtension shopSaleExtension = null;
            Resolve<IUserMapService>().UpdateShopSale(userId, shopSaleExtension);
        }

        [Fact]
        [TestMethod("UpdateTeamInfo_Int64")]
        public void UpdateTeamInfo_Int64_test()
        {
            var childuUserId = 0;
            Resolve<IUserMapService>().UpdateTeamInfo(childuUserId);
        }
    }
}