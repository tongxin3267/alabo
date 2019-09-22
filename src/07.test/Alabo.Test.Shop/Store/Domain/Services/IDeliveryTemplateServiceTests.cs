using Xunit;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Shop.Store.Domain.Services
{
    public class IDeliveryTemplateServiceTests : CoreTest
    {
        /*end*/

        [Fact]
        [TestMethod("AddOrUpdate_DeliveryTemplate_Store")]
        [TestIgnore]
        public void AddOrUpdate_DeliveryTemplate_Store_test()
        {
            //DeliveryTemplate deliveryTemplate = null;
            //Store store = null;
            //var result = Service<IDeliveryTemplateService>().AddOrUpdate( deliveryTemplate, store);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("CountExpressFee_Int64_Guid_UserAddress_Decimal")]
        [TestIgnore]
        public void CountExpressFee_Int64_Guid_UserAddress_Decimal_test()
        {
            //var stroeId = 0;
            //var expressId =Guid.Empty ;
            //UserAddress userAddress = null;
            //var weight = 0;
            //var result = Service<IDeliveryTemplateService>().CountExpressFee( stroeId, expressId, userAddress, weight);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetTemplate_Int64")]
        [TestIgnore]
        public void GetTemplate_Int64_test()
        {
            //var stroeId = 0;
            //var result = Service<IDeliveryTemplateService>().GetTemplate( stroeId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("InitAllStoreDelivery")]
        public void InitAllStoreDelivery_test()
        {
            //Resolve<IDeliveryTemplateService>().InitAllStoreDelivery();
        }

        [Fact]
        [TestMethod("InitStoreDeliveryTemplate")]
        public void InitStoreDeliveryTemplate_test()
        {
            //var result = Resolve<IDeliveryTemplateService>().InitStoreDeliveryTemplate();
            //Assert.NotNull(result);
        }
    }
}