using System.Linq;
using Xunit;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Open.Tasks.Configs.Area;
using Alabo.Extensions;

namespace Alabo.Test.Open.Tasks.Configs.Area {

    /// <summary>
    ///     请登录网站http://test.5ug.com 手动添加条相关的记录
    /// </summary>
    public class ProvinceModuleTests : BaseShareTest {

        public ProvinceModuleTests() {
            RegModuleConfigId = 316;
            ShopModuleConfigId = 317;
            //ModuleType = typeof(ProvinceShareModule);
            //ConfigType = typeof(ProvinceShareConfig);
        }

        [Theory]
        [InlineData("B003")]
        [InlineData("B004")]
        public void ShopOrderTest(string userName) {
            //var user = Resolve<IUserService>().GetSingle(userName);
            //if (user != null)
            //{
            //    var userAddresss = Resolve<IUserAddressService>().GetList(user.Id);
            //    // 宁远县
            //    var findAddress = userAddresss.FirstOrDefault(r => r.RegionId == 431126);
            //    if (findAddress == null)
            //    {
            //        var threeAddress = Resolve<IRegionService>().GetThreeAddress(431126);

            //        findAddress = new UserAddress
            //        {
            //            RegionId = threeAddress.Country,
            //            Province = threeAddress.Province,
            //            City = threeAddress.City,
            //            Address = "分润测试地址",
            //            UserId = user.Id,
            //            AddressDescription = Resolve<IRegionService>().GetFullName(431126)
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