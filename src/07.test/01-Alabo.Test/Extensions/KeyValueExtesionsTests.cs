using System;
using System.Linq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Xunit;
using ZKCloud.App.Core.User.Domain.Dtos;
using ZKCloud.Core.Enums.Enum;
using ZKCloud.Extensions;
using ZKCloud.Test.Base.Core;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Extensions
{
    public class KeyValueExtesionsTests : CoreTest
    {
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
        public void EnumToKeyValues_StateUnderTest_ExpectedBehavior(string typeName)
        {
            var list = KeyValueExtesions.EnumToKeyValues(typeName);
            Assert.NotNull(list);
            Assert.True(list.Any());
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
        [InlineData(typeof(UserRange))]
        [InlineData(typeof(UserTypeEnum))]
        [InlineData(typeof(ViewLocation))]
        [InlineData(typeof(ZoneTime))]
        [InlineData(typeof(BankType))]
        public void EnumToKeyValues_StateUnderTest_ExpectedBehavior222(Type type)
        {
            var list = KeyValueExtesions.EnumToKeyValues(type);
            Assert.NotNull(list);
            Assert.True(list.Any());
        }

        // [Theory]
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
        [InlineData(typeof(UserRange))]
        [InlineData(typeof(UserTypeEnum))]
        [InlineData(typeof(ViewLocation))]
        [InlineData(typeof(ZoneTime))]
        [InlineData(typeof(BankType))]
        [TestMethod("EnumToKeyValues_String")]
        public void EnumToKeyValues_String_test(Type type)
        {
            var result = KeyValueExtesions.EnumToKeyValues(type.Name);
            Assert.NotNull(result);
        }

        //  [Theory]
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
        [InlineData(typeof(UserRange))]
        [InlineData(typeof(UserTypeEnum))]
        [InlineData(typeof(ViewLocation))]
        [InlineData(typeof(ZoneTime))]
        [InlineData(typeof(BankType))]
        [TestMethod("EnumToKeyValues_Type")]
        public void EnumToKeyValues_Type_test(Type type)
        {
            var result = KeyValueExtesions.EnumToKeyValues(type);
            Assert.NotNull(result);
        }

        // [Fact]
        [TestMethod("ToKeyValues_T")]
        public void ToKeyValues_T_test()
        {
            T instance = null;
            var result = instance.ToKeyValues();
            Assert.NotNull(result);
        }

        [Fact]
        public void ToKeyValuesTest()
        {
            var userOutput = new UserOutput
            {
                UserName = "admin",
                Name = "管理员"
            };

            var keyValues = userOutput.ToKeyValues();
            Assert.NotNull(keyValues);
            Assert.True(keyValues.Any());
        }

        /*end*/
    }
}