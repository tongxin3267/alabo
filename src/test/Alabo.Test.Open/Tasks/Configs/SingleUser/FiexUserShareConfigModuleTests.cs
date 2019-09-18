using Xunit;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
//using Alabo.App.Open.Tasks.Configs.SingleUser;
using Alabo.Extensions;

namespace Alabo.Test.Open.Tasks.Configs.SingleUser
{
    /// <summary>
    ///     请登录网站http://test.5ug.com 手动添加条相关的记录
    /// </summary>
    public class FiexUserShareConfigModuleTests : BaseShareTest
    {
        public FiexUserShareConfigModuleTests()
        {
            RegModuleConfigId = 316;
            ShopModuleConfigId = 354;
            //ModuleType = typeof(FiexUserShareConfigModule);
            //ConfigType = typeof(FiexUserShareConfig);
        }

        //[Theory]
        [InlineData("B003")]
        [InlineData("B004")]
        public void ShopOrderTest(string userName)
        {
            var user = Resolve<IUserService>().GetSingle(userName);
            var orderResult = GetShopOrder(user);
            var shareUser = Resolve<IUserService>().GetSingle("admin"); // 与单元测试配置固定
            var beforeAccount = GetShareAccount(shareUser.Id);

            TaskActuator.ExecuteTask(ModuleType, orderResult.Item1,
                new
                {
                    ShareOrderId = orderResult.Item1.Id, orderResult.Item1.TriggerType, OrderId = orderResult.Item2.Id
                });

            var afterAccount = GetShareAccount(shareUser.Id);
            var ratios = 0.1m; // 与单元测试配置固定
            var shareAmount = orderResult.Item2.PaymentAmount * ratios; // 分润金额

            Assert.Equal(1000, shareAmount);

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