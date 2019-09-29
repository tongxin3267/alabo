using System;
using System.Collections.Generic;
using Alabo.App.Asset.Accounts.Domain.Entities;
using Alabo.App.Share.OpenTasks.Configs.UserRecommendedRelationship;
using Alabo.App.Share.TaskExecutes.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Extensions;
using Alabo.Users.Dtos;
using Alabo.Users.Entities;
using Xunit;

namespace Alabo.Test.Open.Tasks.Configs.UserRecommendedRelationship
{
    /// <summary>
    ///     请登录网站http://test.5ug.com 手动添加条相关的记录
    /// </summary>
    public class NLevelDistributionModuleTests : BaseShareTest
    {
        public NLevelDistributionModuleTests()
        {
            RegModuleConfigId = 318;
            ShopModuleConfigId = 319;
            ModuleType = typeof(NLevelDistributionModule);
        }

        [Theory]
        [InlineData("A001")]
        [InlineData("A002")]
        [InlineData("B001")]
        [InlineData("B003")]
        [InlineData("B005")]
        [InlineData("admin")]
        public void UserRegTest(string userName)
        {
            var user = Resolve<IUserService>().GetSingle(userName);
            if (user != null)
            {
                var shareOrder = UserRegShareOrder(user);

                var userMap = Resolve<IUserMapService>().GetParentMapFromCache(user.Id);
                var map = userMap.ParentMap.DeserializeJson<List<ParentMap>>();

                var ratios = Ratios(RegModuleConfigId);

                var shareUserList = new List<User>();
                var shareAmountList = new List<decimal>();
                var beforeAccounts = new List<Account>();
                for (var i = 0; i < ratios.Count; i++)
                {
                    if (map.Count < i + 1) break;

                    var item = map[i];
                    var shareUser = Resolve<IUserService>().GetNomarlUser(item.UserId);
                    if (shareUser == null) break;

                    shareUserList.Add(shareUser);
                    var ratio = Convert.ToDecimal(ratios[i]);
                    var shareAmount = shareOrder.Amount * ratio; //分润金额
                    shareAmountList.Add(shareAmount);
                    beforeAccounts.Add(GetShareAccount(shareUser.Id));
                }

                TaskActuator.ExecuteTask(ModuleType, shareOrder,
                    new {ShareOrderId = shareOrder.Id, shareOrder.TriggerType, BaseFenRunAmount = shareOrder.Amount});

                for (var i = 0; i < shareUserList.Count; i++)
                {
                    var afterAccount = GetShareAccount(shareUserList[i].Id);
                    var shareAmount = shareAmountList[i]; // 分润金额
                    var shareRewardInput = new ShareRewardInput(shareOrder, beforeAccounts[i], afterAccount,
                        shareUserList[i].Id, shareAmount, RegModuleConfigId, user.Id);
                    AssertResult(shareRewardInput);
                }
            }
        }

        [Theory]
        [InlineData("B003")]
        [InlineData("B004")]
        public void ShopOrderTest(string userName)
        {
            var user = Resolve<IUserService>().GetSingle(userName);
            if (user != null)
            {
                var orderResult = GetShopOrder(user);
                var shareOrder = orderResult.Item1;

                var userMap = Resolve<IUserMapService>().GetParentMapFromCache(user.Id);
                var map = userMap.ParentMap.DeserializeJson<List<ParentMap>>();

                var ratios = Ratios(ShopModuleConfigId);

                var shareUserList = new List<User>();
                var shareAmountList = new List<decimal>();
                var beforeAccounts = new List<Account>();
                for (var i = 0; i < ratios.Count; i++)
                {
                    if (map.Count < i + 1) break;

                    var item = map[i];
                    var shareUser = Resolve<IUserService>().GetNomarlUser(item.UserId);
                    if (shareUser == null) break;

                    shareUserList.Add(shareUser);
                    var ratio = Convert.ToDecimal(ratios[i]);
                    var shareAmount = shareOrder.Amount * ratio; //分润金额
                    shareAmountList.Add(shareAmount);
                    beforeAccounts.Add(GetShareAccount(shareUser.Id));
                }

                TaskActuator.ExecuteTask(ModuleType, orderResult.Item1,
                    new
                    {
                        ShareOrderId = orderResult.Item1.Id, orderResult.Item1.TriggerType,
                        OrderId = orderResult.Item2.Id
                    });

                for (var i = 0; i < shareUserList.Count; i++)
                {
                    var afterAccount = GetShareAccount(shareUserList[i].Id);
                    var shareAmount = shareAmountList[i]; // 分润金额
                    var shareRewardInput = new ShareRewardInput(shareOrder, beforeAccounts[i], afterAccount,
                        shareUserList[i].Id, shareAmount, RegModuleConfigId, user.Id);
                    AssertResult(shareRewardInput);
                }
            }
        } /*end*/

        [Fact]
        public void ModuleAttributeTest()
        {
            var moduleAttribute = Resolve<ITaskModuleConfigService>().GetModuleAttribute(ModuleType);
            Assert.NotNull(moduleAttribute);
            Assert.False(moduleAttribute.Id.IsGuidNullOrEmpty());
        }
    }
}