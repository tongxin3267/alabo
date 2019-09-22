using Xunit;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.Core.Enums.Enum;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Finance.Domain.Services
{
    public class IPayServiceTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IPayService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IPayService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        } /*end*/

        [Fact]
        [TestMethod("AfterPay_Pay_Boolean")]
        public void AfterPay_Pay_Boolean_test()
        {
            var pay = Resolve<IPayService>().FirstOrDefault();
            pay.Status = PayStatus.WaiPay;
            var result = Resolve<IPayService>().AfterPay(pay, true);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAllPayClientTypeAttribute")]
        public void GetAllPayClientTypeAttribute_test()
        {
            var result = Resolve<IPayService>().GetAllPayClientTypeAttribute();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPageList_Object")]
        public void GetPageList_Object_test()
        {
            object query = null;
            var result = Resolve<IPayService>().GetPageList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPayType_ClientInput")]
        [TestIgnore]
        public void GetPayType_ClientInput_test()
        {
            //ClientInput parameter = null;
            //var result = Service<IPayService>().GetPayType(parameter);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_Int64")]
        [TestIgnore]
        public void GetSingle_Int64_test()
        {
            //var id = 0;
            //var result = Service<IPayService>().GetSingle(id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Pay_PayInput_HttpContext")]
        [TestIgnore]
        public void Pay_PayInput_HttpContext_test()
        {
            //PayInput payInput = null;
            //HttpContext httpContext = null;
            //var result = Service<IPayService>().Pay(payInput, httpContext);
            //Assert.NotNull(result);
        }
    }
}