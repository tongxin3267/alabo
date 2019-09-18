using System.Collections.Generic;
using System.Linq;
using Xunit;
using Alabo.App.Core.Reports.Domain.Services;
using Alabo.App.Core.Reports.Model;
using Alabo.App.Open.Share.Domain.Services;
using Alabo.Extensions;
using ZKCloud.Open.Share.Models;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Open.Share.Domain.Service
{
    public class IRewardServiceTests : CoreTest
    {
        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetRewardView_Int64")]
        public void GetRewardView_Int64_test(long id)
        {
            var reward = Resolve<IRewardService>().GetRandom(id);
            var result = Resolve<IRewardService>().GetRewardView(reward.Id);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("AddOrUpdate_Reward")]
        public void AddOrUpdate_Reward_test(long id)
        {
            var reward = Resolve<IRewardService>().GetRandom(id);
            Resolve<IRewardService>().AddOrUpdate(reward);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetSingle_Nullable_System_Int64")]
        public void GetSingle_Nullable_System_Int64_test(long id)
        {
            var reward = Resolve<IRewardService>().GetRandom(id);
            var result = Resolve<IRewardService>().GetSingle(reward.Id);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IRewardService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IRewardService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        /*end*/

        [Fact]
        [TestMethod("AddOrUpdate_IEnumerable1")]
        public void AddOrUpdate_IEnumerable1_test()
        {
            //Service<IRewardService>().AddOrUpdate( null);
        }

        [Fact]
        [TestMethod("GetShareBaseConfig_Int64")]
        public void GetShareBaseConfig_Int64_test()
        {
            //var moduleConfigId = 0;
            //var result = Service<IRewardService>().GetShareBaseConfig( moduleConfigId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserPage_Object_Reward")]
        [TestIgnore]
        public void GetUserPage_Object_Reward_test()
        {
            //            Object query = null;
            //            Reward entity = null;
            //            var result = Service<IRewardService>().GetUserPage(query, entity);
            //            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserPage_Object")]
        [TestIgnore]
        public void GetUserPage_Object_test()
        {
            //            Object query = null;
            //            var result = Service<IRewardService>().GetUserPage(query);
            //            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetViewModuleConfig_HttpContext_Guid_Int64_Int32")]
        [TestIgnore]
        public void GetViewModuleConfig_HttpContext_Guid_Int64_Int32_test()
        {
            //            HttpContext context = null;
            //            var moduleId = Guid.Empty;
            //            var Id = 0;
            //            var copy = 0;
            //            var result = Service<IRewardService>().GetViewModuleConfig(context, moduleId, Id, copy);
            //            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetViewRewardPageList_RewardInput_HttpContext")]
        [TestIgnore]
        public void GetViewRewardPageList_RewardInput_HttpContext_test()
        {
            //            RewardInput userInput = null;
            //            HttpContext context = null;
            //            var result = Service<IRewardService>().GetViewRewardPageList(userInput, context);
            //            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetViewShareModuleList_HttpContext_Guid_String_Nullable_System_Int32")]
        [TestIgnore]
        public void GetViewShareModuleList_HttpContext_Guid_String_Nullable_System_Int32_test()
        {
            //            HttpContext context = null;
            //            var moduleId = Guid.Empty;
            //            var name = "";
            //            var isEnable = 0;
            //            var result = Service<IRewardService>().GetViewShareModuleList(context, moduleId, name, isEnable);
            //            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("TryGetModuleAttribute_String")]
        public void TryGetModuleAttribute_String_test()
        {
            var shareModuleList = Resolve<IReportService>().GetValue<ShareModuleReports>();
            var list = shareModuleList.ShareModuleList.DeserializeJson<List<ShareModule>>().ToList();
            foreach (var item in list)
            {
                var result = Resolve<IRewardService>().TryGetModuleAttribute(item.Id.ToString());
                Assert.NotNull(result);
            }
        }
    }
}