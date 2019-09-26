using System.Linq;
using Xunit;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Open.Tasks.Configs.Area;
using Alabo.Extensions;

namespace Alabo.Test.Open.Tasks.Configs.Area {

    /// <summary>
    ///     请登录网站http://test.5ug.com 手动添加条相关的记录
    /// </summary>
    public class CountryModuleTests : BaseShareTest {

        public CountryModuleTests() {
            RegModuleConfigId = 316;
            ShopModuleConfigId = 333;
            //ModuleType = typeof(CountryModule);
            //ConfigType = typeof(CountryShareConfig);
        }

        [Theory]
        [InlineData("A001")]
        [InlineData("A002")]
        [InlineData("B001")]
        [InlineData("B002")]
        [InlineData("admin")]
        public void UserRegTest(string userName) {
            //var user = Service<IUserService>().GetSingle(userName);
            //var shareOrder = UserRegShareOrder(user);
            //var beforeAccount = GetShareAccount(user.Id);

            //this.TaskActuator.ExecuteTask(this.ModuleType, shareOrder,
            //    new {ShareOrderId = shareOrder.Id, shareOrder.TriggerType, BaseFenRunAmount = shareOrder.Amount});

            //var afterAccount = GetShareAccount(user.Id);
            //var ratios = this.Ratios(this.RegModuleConfigId);
            //var shareAmount = shareOrder.Amount*ratios[0];// 分润金额
            //var shareRewardInput = new ShareRewardInput(shareOrder, beforeAccount, afterAccount, user.Id, shareAmount, this.RegModuleConfigId,user.Id);
            //this.AssertResult(shareRewardInput);
        }

        [Theory]
        [InlineData("B003")]
        [InlineData("B004")]
        public void ShopOrderTest(string userName) {
            //var user = Resolve<IUserService>().GetSingle(userName);
            //if (user != null)
            //{
            //    var userAddresss = Resolve<IUserAddressService>().GetList(user.Id);
            //    // 广东广州花都
            //    var findAddress = userAddresss.FirstOrDefault(r => r.RegionId == 440114);
            //    if (findAddress == null)
            //    {
            //        var threeAddress = Resolve<IRegionService>().GetThreeAddress(440114);

            //        findAddress = new UserAddress
            //        {
            //            RegionId = threeAddress.Country,
            //            Province = threeAddress.Province,
            //            City = threeAddress.City,
            //            Address = "分润测试地址",
            //            AddressDescription = Resolve<IRegionService>().GetFullName(440114)
            //        };
            //        Resolve<IUserAddressService>().Add(findAddress);
            //    }

            //    var orderResult = GetShopOrder(user, findAddress);
            //    var beforeAccount = GetShareAccount(user.Id);

            //    TaskActuator.ExecuteTask(ModuleType, orderResult.Item1,
            //        new
            //        {
            //            ShareOrderId = orderResult.Item1.Id, orderResult.Item1.TriggerType,
            //            OrderId = orderResult.Item1.EntityId
            //        });

            //    var afterAccount = GetShareAccount(user.Id);
            //    var ratios = Ratios(ShopModuleConfigId);
            //    var shareAmount = orderResult.Item2.PaymentAmount * ratios[0]; // 分润金额

            //    var shareRewardInput = new ShareRewardInput(orderResult.Item1, beforeAccount, afterAccount, user.Id,
            //        shareAmount, ShopModuleConfigId, user.Id);
            //    AssertResult(shareRewardInput);
            //}
        }

        [Fact]
        public void ModuleAttributeTest() {
            var moduleAttribute = Resolve<ITaskModuleConfigService>().GetModuleAttribute(ModuleType);
            Assert.NotNull(moduleAttribute);
            Assert.False(moduleAttribute.Id.IsGuidNullOrEmpty());
            Assert.NotNull(ConfigType);
        }
    }
}