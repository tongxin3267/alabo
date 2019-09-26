using Xunit;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Finance.Domain.Services
{
    public class IWithDrawServiceTests : CoreTest
    {
        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetWithDrawMoneys_Int64")]
        [TestIgnore]
        public void GetWithDrawMoneys_Int64_test(long userId)
        {
            //var user = Service<IUserService>().GetRandom(userId);
            //var result = Service<IWithDrawService>().GetWithDrawMoneys(user.Id);
            //Assert.NotNull(result);
        }

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
        [TestMethod("Add_WithDrawInput")]
        [TestIgnore]
        public void Add_WithDrawInput_test()
        {
            //WithDrawInput view = null;
            //var result = Service<IWithDrawService>().Add(view);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Check_DefaultHttpContext")]
        [TestIgnore]
        public void Check_DefaultHttpContext_test()
        {
            //DefaultHttpContext httpContext = null;
            //var result = Service<IWithDrawService>().Check(httpContext);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Delete_Int64_Int64")]
        [TestIgnore]
        public void Delete_Int64_Int64_test()
        {
            //var userId = 0;
            //var id = 0;
            //var result = Service<IWithDrawService>().Delete(userId, id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAdminWithDraw_Int64")]
        [TestIgnore]
        public void GetAdminWithDraw_Int64_test()
        {
            //var id = 0;
            //var result = Service<IWithDrawService>().GetAdminWithDraw(id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPageList_Object")]
        [TestIgnore]
        public void GetPageList_Object_test()
        {
            //Object query = null;
            //var result = Service<IWithDrawService>().GetPageList(query);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_Int64_Int64")]
        [TestIgnore]
        public void GetSingle_Int64_Int64_test()
        {
            //var userId = 0;
            //var id = 0;
            //var result = Service<IWithDrawService>().GetSingle(userId, id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserList_WithDrawApiInput")]
        [TestIgnore]
        public void GetUserList_WithDrawApiInput_test()
        {
            //WithDrawApiInput parameter = null;
            //var result = Service<IWithDrawService>().GetUserList(parameter);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserPage_Object")]
        [TestIgnore]
        public void GetUserPage_Object_test()
        {
            //Object query = null;
            //var result = Service<IWithDrawService>().GetUserPage( query);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetView_Int64")]
        [TestIgnore]
        public void GetView_Int64_test()
        {
            //var id = 0;
            //var result = Service<IWithDrawService>().GetView( id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("WithDrawCheck_ViewWithDrawCheck")]
        [TestIgnore]
        public void WithDrawCheck_ViewWithDrawCheck_test()
        {
            //ViewWithDrawCheck view = null;
            //var result = Service<IWithDrawService>().WithDrawCheck( view);
            //Assert.NotNull(result);
        }
    }
}