using Alabo.App.Share.OpenTasks.Configs.UserRecommendedRelationship;
using Alabo.App.Share.TaskExecutes.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Extensions;
using Xunit;

namespace Alabo.Test.Open.Tasks.Configs.UserRecommendedRelationship
{
    /// <summary>
    ///     请登录网站http://test.5ug.com 手动添加条相关的记录
    /// </summary>
    public class RebateModuleTests : BaseShareTest
    {
        public RebateModuleTests()
        {
            RegModuleConfigId = 316;
            ShopModuleConfigId = 317;
            ModuleType = typeof(RebateModule);
            ConfigType = typeof(RebateConfig);
        }

        //[Theory]
        //[InlineData("A001")]
        //[InlineData("A002")]
        //[InlineData("B001")]
        //[InlineData("B002")]
        [InlineData("admin")]
        public void UserRegTest(string userName)
        {
            var user = Resolve<IUserService>().GetSingle(userName);
            var shareOrder = UserRegShareOrder(user);
            var beforeAccount = GetShareAccount(user.Id);

            TaskActuator.ExecuteTask(ModuleType, shareOrder,
                new {ShareOrderId = shareOrder.Id, shareOrder.TriggerType, BaseFenRunAmount = shareOrder.Amount});

            var afterAccount = GetShareAccount(user.Id);
            var ratios = Ratios(RegModuleConfigId);
            var shareAmount = shareOrder.Amount * ratios[0]; // 分润金额
            var shareRewardInput = new ShareRewardInput(shareOrder, beforeAccount, afterAccount, user.Id, shareAmount,
                RegModuleConfigId, user.Id);
            AssertResult(shareRewardInput);
        }

        [Theory]
        //[InlineData("B003")]
        [InlineData("admin")]
        public void ShopOrderTest(string userName)
        {
            var user = Resolve<IUserService>().GetSingle(userName);
            var orderResult = GetShopOrder(user);
            var beforeAccount = GetShareAccount(user.Id);

            TaskActuator.ExecuteTask(ModuleType, orderResult.Item1,
                new
                {
                    ShareOrderId = orderResult.Item1.Id, orderResult.Item1.TriggerType, OrderId = orderResult.Item2.Id
                });

            var afterAccount = GetShareAccount(user.Id);
            var ratios = Ratios(ShopModuleConfigId);
            var shareAmount = orderResult.Item2.PaymentAmount * ratios[0]; // 分润金额

            var shareRewardInput = new ShareRewardInput(orderResult.Item1, beforeAccount, afterAccount, user.Id,
                shareAmount, ShopModuleConfigId, user.Id);
            AssertResult(shareRewardInput);
        }

        [Fact]
        public void ModuleAttributeTest()
        {
            var moduleAttribute = Resolve<ITaskModuleConfigService>().GetModuleAttribute(ModuleType);
            Assert.NotNull(moduleAttribute);
            Assert.False(moduleAttribute.Id.IsGuidNullOrEmpty());
            Assert.NotNull(ConfigType);
        }
    }
}