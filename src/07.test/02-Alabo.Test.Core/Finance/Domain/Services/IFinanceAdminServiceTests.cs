using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.Finance.Domain.Services
{
    public class IFinanceAdminServiceTests : CoreTest
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
        [TestMethod("GetApiBillPageList_BillApiInput_Int64")]
        [TestIgnore]
        public void GetApiBillPageList_BillApiInput_Int64_test()
        {
            //BillApiInput billApiInput = null;
            //Int64 count = 0;
            //var result = Service<IFinanceAdminService>().GetApiBillPageList(billApiInput, out count);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetBillPageList_Object")]
        public void GetBillPageList_Object_test()
        {
            //Object query = null;
            //var result = Service<IFinanceAdminService>().GetBillPageList( query);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetViewBillPageList_BillInput")]
        [TestIgnore]
        public void GetViewBillPageList_BillInput_test()
        {
            //BillInput userInput = null;
            //var result = Service<IFinanceAdminService>().GetViewBillPageList(userInput);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetViewBillSingle_Int64")]
        [TestIgnore]
        public void GetViewBillSingle_Int64_test()
        {
            //var id = 0;
            //var result = Service<IFinanceAdminService>().GetViewBillSingle(id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetViewUserPageList_UserInput")]
        [TestIgnore]
        public void GetViewUserPageList_UserInput_test()
        {
            //UserInput userInput = null;
            //var result = Service<IFinanceAdminService>().GetViewUserPageList(userInput);
            //Assert.NotNull(result);
        }
    }
}