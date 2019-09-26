using MongoDB.Bson;
using Xunit;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Framework.Basic.Address.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.User.Domain.Services {

    public class IUserAddressServiceTests : CoreTest {

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetList_Int64")]
        [TestIgnore]
        public void GetList_Int64_test(long userId) {
            //var user = Service<IUserService>().GetRandom(userId);
            //var result = Service<IUserAddressService>().GetList(user.Id);
            //Assert.NotNull(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetUserAddress_Nullable_System_Guid_Int64")]
        [TestIgnore]
        public void GetUserAddress_Nullable_System_Guid_Int64_test(long userId) {
            //var guid = Guid.Empty;
            //var user = Service<IUserService>().GetRandom(userId);
            //var result = Service<IUserAddressService>().GetUserAddress(guid, userId);
            //Assert.NotNull(result);
        }

        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId) {
            var model = Resolve<IUserService>().GetRandom(entityId);
            if (model != null) {
                var newModel = Resolve<IUserService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        } /*end*/

        [Fact]
        [TestMethod("Add_UserAddress")]
        [TestIgnore]
        public void Add_UserAddress_test() {
            //UserAddress userAddress = null;
            //var result = Service<IUserAddressService>().Add(userAddress);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Delete_Int64_Guid")]
        public void Delete_Int64_Guid_test() {
            //var userId = 0;
            //var id = Guid.Empty;
            //var result = Resolve<IUserAddressService>().Delete(userId, id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPageList_Object")]
        [TestIgnore]
        public void GetPageList_Object_test() {
            //object query = null;
            //var result = Service<IUserAddressService>().GetPageList(query);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("SetDefault_Int64_Guid")]
        public void SetDefault_Int64_Guid_test() {
            var userId = 0;
            var addressId = ObjectId.Empty;
            var result = Resolve<IUserAddressService>().SetDefault(userId, addressId);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Update_UserAddress")]
        [TestIgnore]
        public void Update_UserAddress_test() {
            //UserAddress userAddress = null;
            //var result = Service<IUserAddressService>().Update(userAddress);
            //Assert.NotNull(result);
        }
    }
}