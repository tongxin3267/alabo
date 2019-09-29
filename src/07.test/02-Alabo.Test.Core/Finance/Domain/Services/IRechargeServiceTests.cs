using Alabo.App.Asset.Recharges.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.Finance.Domain.Services
{
    public class IRechargeServiceTests : CoreTest
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
        [TestMethod("AddOffOnline_RechargeAddInput")]
        [TestIgnore]
        public void AddOffOnline_RechargeAddInput_test()
        {
            //RechargeAddInput view = null;
            //var result = Service<IRechargeService>().AddOffOnline( view);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("AddOnline_RechargeOnlineAddInput")]
        [TestIgnore]
        public void AddOnline_RechargeOnlineAddInput_test()
        {
            //RechargeOnlineAddInput view = null;
            //var result = Service<IRechargeService>().AddOnline(view);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Check_DefaultHttpContext")]
        [TestIgnore]
        public void Check_DefaultHttpContext_test()
        {
            //DefaultHttpContext httpContext = null;
            //var result = Service<IRechargeService>().Check(httpContext);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Delete_Int64_Int64")]
        public void Delete_Int64_Int64_test()
        {
            var userId = 0;
            var id = 0;
            var result = Resolve<IRechargeService>().Delete(userId, id);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAdminRecharge_Int64")]
        [TestIgnore]
        public void GetAdminRecharge_Int64_test()
        {
            //var id = 0;
            //var result = Service<IRechargeService>().GetAdminRecharge(id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPageList_Object")]
        public void GetPageList_Object_test()
        {
            object query = null;
            var result = Resolve<IRechargeService>().GetPageList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetRechargeMoneys")]
        public void GetRechargeMoneys_test()
        {
            var result = Resolve<IRechargeService>().GetRechargeMoneys();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_Int64")]
        [TestIgnore]
        public void GetSingle_Int64_test()
        {
            //var id = 0;
            //var result = Service<IRechargeService>().GetSingle(id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserList_RechargeAddInput")]
        [TestIgnore]
        public void GetUserList_RechargeAddInput_test()
        {
            //RechargeAddInput parameter = null;
            //var result = Service<IRechargeService>().GetUserList( parameter);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserPage_Object")]
        public void GetUserPage_Object_test()
        {
            object query = null;
            var result = Resolve<IRechargeService>().GetPageList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetView_Int64")]
        public void GetView_Int64_test()
        {
            //var id = 0;
            //var result = Service<IRechargeService>().GetView( id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("RechargeCheck_ViewRechargeCheck")]
        public void RechargeCheck_ViewRechargeCheck_test()
        {
            //ViewRechargeCheck view = null;
            //var result = Service<IRechargeService>().RechargeCheck( view);
            //Assert.NotNull(result);
        }
    }
}