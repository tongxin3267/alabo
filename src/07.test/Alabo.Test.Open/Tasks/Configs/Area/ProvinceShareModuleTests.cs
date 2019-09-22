using Xunit;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Open.Tasks.Configs.Area;
using Alabo.Extensions;

namespace Alabo.Test.Open.Tasks.Configs.Area
{
    /// <summary>
    ///     请登录网站http://test.5ug.com 手动添加条相关的记录
    /// </summary>
    public class ProvinceShareModuleTests : BaseShareTest
    {
        public ProvinceShareModuleTests()
        {
            RegModuleConfigId = 316;
            ShopModuleConfigId = 317;
            //ModuleType = typeof(ProvinceShareModule);
            //ConfigType = typeof(ProvinceShareConfig);
        }

        //  [Theory]
        [InlineData("B003")]
        [InlineData("B004")]
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