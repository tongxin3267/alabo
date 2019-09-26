using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.Finance.Domain.Services
{
    public class IBankCardServiceTests : CoreTest
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
        [TestMethod("Add_BankCard")]
        [TestIgnore]
        public void Add_BankCard_test()
        {
            //BankCard BankCard = null;
            //var result = Service<IBankCardService>().Add(BankCard);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("AddOrUpdate_ViewBankCard")]
        [TestIgnore]
        public void AddOrUpdate_ViewBankCard_test()
        {
            //ViewBankCard view = null;
            //var result = Service<IBankCardService>().AddOrUpdate( view);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetBankCard_Nullable_System_Guid_Int64")]
        [TestIgnore]
        public void GetBankCard_Nullable_System_Guid_Int64_test()
        {
            //var guid = Guid.Empty;
            //var userId = 0;
            //var result = Service<IBankCardService>().GetBankCard(guid, userId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetList_Int64")]
        [TestIgnore]
        public void GetList_Int64_test()
        {
            //var userId = 0;
            //var result = Service<IBankCardService>().GetList(userId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserPage_Object")]
        [TestIgnore]
        public void GetUserPage_Object_test()
        {
            //Object query = null;
            //var result = Service<IBankCardService>().GetUserPage( query);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("SetDefault_Int64_Guid")]
        public void SetDefault_Int64_Guid_test()
        {
            //var userId = 0;
            //var id =Guid.Empty ;
            //var result = Service<IBankCardService>().SetDefault( userId, id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Update_BankCard")]
        [TestIgnore]
        public void Update_BankCard_test()
        {
            //BankCard bankCard = null;
            //var result = Service<IBankCardService>().Update(bankCard);
            //Assert.NotNull(result);
        }
    }
}