using System;
using System.Linq;
using Xunit;
using Alabo.App.Agent.Citys.Domain.CallBacks;
using Alabo.App.Cms.Articles.Controllers;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Common.Controllers;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Market.SmallTargets.Controllers;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.AutoConfigs;
using Alabo.Core.Enums.Enum;
using Alabo.Core.WebApis.Controller;
using Alabo.Domains.Entities;
using Alabo.Reflections;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Alabo.Users.Enum;

namespace Alabo.Test.Core.Admin.Domain.Services {

    public class ITypeServiceTests : CoreTest {

        [Fact]
        public void AllKeyValues_Test() {
            var result = Resolve<ITypeService>().AllKeyValues();
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(typeof(UserGradeConfig))]
        [InlineData(typeof(CityGradeConfig))]
        [TestMethod("GetAutoConfigDictionary_String")]
        public void GetAutoConfigDictionary_String_test(Type type) {
            var configName = type.FullName;
            var result = Resolve<ITypeService>().GetAutoConfigDictionary(configName);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("BankType")]
        [InlineData("AccountFlow")]
        [InlineData("ActivateState")]
        [InlineData("ApiMethodType")]
        [InlineData("AuthorityType")]
        [InlineData("BillActionType")]
        [InlineData("BillStatus")]
        [InlineData("BooleanValues")]
        [InlineData("CardCouponsType")]
        [InlineData("CardType")]
        [InlineData("CheckoutType")]
        [InlineData("ClientType")]
        [InlineData("CommentCheck")]
        [InlineData("CommentIndustry")]
        [InlineData("CommentLevel")]
        [InlineData("CommentType")]
        [InlineData("ConditionType")]
        [InlineData("ConfigType")]
        [InlineData("CreateStyle")]
        [InlineData("Currency")]
        [InlineData("DialogSize")]
        [InlineData("IdentityStatus")]
        [InlineData("IdentityType")]
        [InlineData("Languages")]
        [InlineData("LoginAuthorizeType")]
        [InlineData("Nation")]
        [InlineData("MarketEnum")]
        [InlineData("PayPlatform")]
        [InlineData("PayStatus")]
        [InlineData("PriorityType")]
        [InlineData("ProductRange")]
        [InlineData("QueryEnum")]
        [InlineData("RchargeStyle")]
        [InlineData("RecommendModel")]
        [InlineData("RegularType")]
        [InlineData("RelationType")]
        [InlineData("ScoreDes")]
        [InlineData("Sex")]
        [InlineData("TaskType")]
        [InlineData("UserRange")]
        [InlineData("UserTypeEnum")]
        [InlineData("ViewLocation")]
        [InlineData("ZoneTime")]
        [TestMethod("GetEnumDictionary_String")]
        public void GetEnumDictionary_String_test(string enumName) {
            var result = Resolve<ITypeService>().GetEnumDictionary(enumName);
            if (result == null) {
                Console.WriteLine(result);
            }

            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(typeof(AccountFlow))]
        [InlineData(typeof(ActivateState))]
        [InlineData(typeof(ApiMethodType))]
        [InlineData(typeof(AuthorityType))]
        [InlineData(typeof(BillActionType))]
        [InlineData(typeof(BillStatus))]
        [InlineData(typeof(BooleanValues))]
        [InlineData(typeof(CallBackType))]
        [InlineData(typeof(CardCouponsType))]
        [InlineData(typeof(CardType))]
        [InlineData(typeof(CheckoutType))]
        [InlineData(typeof(ClientType))]
        [InlineData(typeof(CommentCheck))]
        [InlineData(typeof(CommentIndustry))]
        [InlineData(typeof(CommentLevel))]
        [InlineData(typeof(CommentType))]
        [InlineData(typeof(ConditionType))]
        [InlineData(typeof(ConfigType))]
        //[InlineData(typeof(Country))] ������
        [InlineData(typeof(CreateStyle))]
        [InlineData(typeof(Currency))]
        [InlineData(typeof(DialogSize))]
        [InlineData(typeof(IdentityStatus))]
        [InlineData(typeof(IdentityType))]
        [InlineData(typeof(Languages))]
        [InlineData(typeof(LoginAuthorizeType))]
        [InlineData(typeof(Nation))]
        [InlineData(typeof(MarketEnum))]
        [InlineData(typeof(PayPlatform))]
        [InlineData(typeof(PayStatus))]
        [InlineData(typeof(PriorityType))]
        [InlineData(typeof(ProductRange))]
        [InlineData(typeof(QueryEnum))]
        [InlineData(typeof(RchargeStyle))]
        [InlineData(typeof(RecommendModel))]
        [InlineData(typeof(RegularType))]
        [InlineData(typeof(RelationType))]
        [InlineData(typeof(ScoreDes))]
        [InlineData(typeof(Sex))]
        [InlineData(typeof(TaskType))]
        [InlineData(typeof(OrderStatus))]
        [InlineData(typeof(UserRange))]
        [InlineData(typeof(UserTypeEnum))]
        [InlineData(typeof(ViewLocation))]
        //[InlineData(typeof(ZKCloudDateType))]   ������
        [InlineData(typeof(ZoneTime))]
        [InlineData(typeof(BankType))]
        [TestMethod("GetEnumSelectItem_Type")]
        public void GetEnumSelectItem_Type_test(Type type) {
            var result = Resolve<ITypeService>().GetEnumSelectItem(type);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(typeof(AccountFlow))]
        [InlineData(typeof(ActivateState))]
        [InlineData(typeof(ApiMethodType))]
        [InlineData(typeof(AuthorityType))]
        [InlineData(typeof(BillActionType))]
        [InlineData(typeof(BillStatus))]
        [TestMethod("GetEnumDictionary_Type")]
        public void GetEnumDictionary_Type_test(Type type) {
            var result = Resolve<ITypeService>().GetEnumDictionary(type);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("BankType")]
        [InlineData("AccountFlow")]
        [TestIgnore]
        public void GetEnumType_String_test(string enumName) {
            //var result = Service<ITypeService>().GetEnumType(enumName);
            //Assert.NotNull(result);
        }

        [Theory]
        [InlineData(typeof(IAutoConfig))]
        [TestMethod("GetAllTypeByInterface_Type")]
        public void GetAllTypeByInterface_Type_test(Type interfaceType) {
            var result = Resolve<ITypeService>().GetAllTypeByInterface(interfaceType);
            Assert.NotNull(result);
            Assert.True(result.Any());
        }

        [Theory]
        [TestMethod("GetAppName_Type")]
        [InlineData(null, "")]
        public void GetAppName_Type_test(Type type, string appName) {
            var result = Resolve<ITypeService>().GetAppName(type);
            Assert.Equal(result, appName);
        }

        [Theory]
        [TestMethod("GetAppName_Type")]
        [InlineData(null, "")]
        public void GetGroupName_Type_test(Type type, string groupName) {
            var result = Resolve<ITypeService>().GetGroupName(type);
            Assert.Equal(result, groupName);
        }

        [Theory]
        [InlineData(typeof(ArticleApiController))]
        [InlineData(typeof(ApiSmallTargetController))]
        public void BaseTypeContains_Test(Type type) {
            var topBaseType = Reflection.BaseTypeContains(type, typeof(ApiBaseController));
            Assert.True(topBaseType);
        }

        [Fact]
        [TestMethod("GetAllApiController")]
        public void GetAllApiController_test() {
            var type = typeof(ArticleApiController);
            var result = Resolve<ITypeService>().GetAllApiController().ToList();

            Assert.NotNull(result);
            Assert.True(result.Any());
            var find = result.FirstOrDefault(r => r.FullName == type.FullName);
            Assert.NotNull(find);

            type = typeof(ApiController);
            find = result.FirstOrDefault(r => r.FullName == type.FullName);
            Assert.NotNull(find);
        }

        [Fact]
        [TestMethod("GetAllConfigType_String")]
        [TestIgnore]
        public void GetAllConfigType_String_test() {
            //var confinName = "";
            //var result = Service<ITypeService>().GetAllConfigType( confinName);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAllConfigType")]
        public void GetAllConfigType_test() {
            var result = Resolve<ITypeService>().GetAllConfigType();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAllEntityType")]
        [TestIgnore]
        public void GetAllEntityType_test() {
            //var result = Service<ITypeService>().GetAllEntityType();
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAllEnumType")]
        [TestIgnore]
        public void GetAllEnumType_test() {
            var result = Resolve<ITypeService>().GetAllEnumType();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAllServiceType")]
        public void GetAllServiceType_test() {
            var result = Resolve<ITypeService>().GetAllServiceType();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAutoConfigType_String")]
        public void GetAutoConfigType_String_test() {
            var configName = "";
            var result = Resolve<ITypeService>().GetAutoConfigType(configName);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetEntityType_Object")]
        [TestIgnore]
        public void GetEntityType_Object_test() {
            //Object entityName = null;
            //var result = Service<ITypeService>().GetEntityType( entityName);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetEntityType_String")]
        [TestIgnore]
        public void GetEntityType_String_test() {
            //var entityName = "";
            //var result = Service<ITypeService>().GetEntityType( entityName);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetEnumType_String")]
        [TestIgnore]
        public void GetEnumType_String_test1() {
            //var enumName = "";
            //var result = Service<ITypeService>().GetEnumType( enumName);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetServiceType_String")]
        public void GetServiceType_String_test() {
            var serviceName = "";
            var result = Resolve<ITypeService>().GetServiceType(serviceName);
            Assert.NotNull(result);
        }

        /*end*/
    }
}