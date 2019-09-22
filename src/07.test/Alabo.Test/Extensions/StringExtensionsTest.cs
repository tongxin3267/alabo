using MongoDB.Bson;
using Xunit;
using ZKCloud.Extensions;

namespace ZKCloud.Test.Extensions
{
    public class StringExtensionsTest
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
        public void ConvertToObjectIdText(string input)
        {
            var objectId = input.ConvertToObjectId();
            Assert.True(objectId != ObjectId.Empty);
        }

        [Theory]
        [InlineData("/finance/withdraw/index", "/finance/withdraw")]
        [InlineData("finance/withdraw/index", "finance/withdraw")]
        public void SubStringLastEndText(string input, string endInput)
        {
            var objectId = input.SubStringLastEnd("/");
            Assert.True(objectId == endInput);
        }

        [Fact]
        public void GetQueryStringTest()
        {
            var url = "http://admin.czhait.com/Admin/Basic/List?Service=IProductLineService&Method=GetPageList";
            var result = StringExtensions.ParseUrl(url, out var baseUrl);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("GetPageList", result[1]);
            Assert.Equal("http://admin.czhait.com/Admin/Basic/List", baseUrl);

            url = "/Admin/Basic/List?Service=IProductLineService&Method=GetPageList";
            result = StringExtensions.ParseUrl(url, out baseUrl);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("GetPageList", result[1]);
            Assert.Equal("IProductLineService", result[0]);
        } /*end*/
    }
}