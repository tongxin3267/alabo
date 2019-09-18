using Xunit;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Finance.Domain.Services
{
    public class ITradeServiceTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<ITradeService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<ITradeService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        } /*end*/

        [Fact]
        [TestMethod("AddOffOnline_RechargeAddInput")]
        [TestIgnore]
        public void AddOffOnline_RechargeAddInput_test()
        {
            //RechargeAddInput view = null;
            //var result = Service<IRechargeService>().AddOffOnline(view);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("AddOnline_RechargeAddInput")]
        [TestIgnore]
        public void AddOnline_RechargeAddInput_test()
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
        [TestIgnore]
        public void Delete_Int64_Int64_test()
        {
            //var userId = 0;
            //var id = 0;
            //var result = Service<ITradeService>().Delete(userId, id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAdminRecharge_Int64")]
        [TestIgnore]
        public void GetAdminRecharge_Int64_test()
        {
            //var id = 0;
            //var result = Service<ITradeService>().GetAdminRecharge(id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Getkkkk_HttpContext")]
        public void Getkkkk_HttpContext_test()
        {
            //HttpContext httpContext = null;
            //Service<ITradeService>().Getkkkk( httpContext);
        }

        [Fact]
        [TestMethod("GetPageList_Object")]
        [TestIgnore]
        public void GetPageList_Object_test()
        {
            //Object query = null;
            //var result = Service<ITradeService>().GetPageList(query);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetRechargeMoneys")]
        [TestIgnore]
        public void GetRechargeMoneys_test()
        {
            //var result = Service<ITradeService>().GetRechargeMoneys();
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_Int64")]
        [TestIgnore]
        public void GetSingle_Int64_test()
        {
            //var tradeId = 0;
            //var result = Service<ITradeService>().GetSingle(tradeId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserList_RechargeApiInput")]
        [TestIgnore]
        public void GetUserList_RechargeApiInput_test()
        {
            //RechargeApiInput parameter = null;
            //var result = Service<ITradeService>().GetUserList(parameter);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("RechargeCheck_ViewRechargeCheck")]
        [TestIgnore]
        public void RechargeCheck_ViewRechargeCheck_test()
        {
            //ViewRechargeCheck view = null;
            //var result = Service<ITradeService>().RechargeCheck( view);
            //Assert.NotNull(result);
        }
    }
}