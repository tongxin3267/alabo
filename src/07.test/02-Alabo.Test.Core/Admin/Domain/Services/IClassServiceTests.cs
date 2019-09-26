using System;
using Xunit;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Shop.Activitys.ViewModels;
using Alabo.Framework.Core.Reflections.Services;
using Alabo.Runtime.Config;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Admin.Domain.Services {

    public class IClassServiceTests : CoreTest {

        [Theory]
        [InlineData(typeof(ViewActivityPage))]
        [InlineData(typeof(ViewActivityProductPage))]
        [InlineData(typeof(AppSettingConfig))]
        [InlineData(typeof(MoneyTypeConfig))]
        [InlineData(typeof(TransferConfig))]
        [TestMethod("GetClassDescription_String")]
        public void GetClassDescription_String_test(Type type) {
            var fullName = type.FullName;
            var result = Resolve<IClassService>().GetClassDescription(fullName);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Alabo.App.Shop.Activitys.ViewModels.ViewActivityProductPage")]
        [InlineData("Alabo.App.Core.Admin.Domain.CallBacks.PlatformAuthorityConfig")]
        [InlineData("Alabo.App.Core.Admin.Domain.CallBacks.PostRoleConfig")]
        [InlineData("Alabo.App.Shop.Activitys.ViewModels.ViewActivityPage")]
        [TestMethod("GetEditPropertys_String")]
        public void GetEditPropertys_String_test(string fullName) {
            var result = Resolve<IClassService>().GetEditPropertys(fullName);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(typeof(ViewActivityPage))]
        [InlineData(typeof(ViewActivityProductPage))]
        [InlineData(typeof(AppSettingConfig))]
        [InlineData(typeof(MoneyTypeConfig))]
        [InlineData(typeof(TransferConfig))]
        [TestMethod("GetListPropertys_String")]
        public void GetListPropertys_String_test(Type type) {
            var fullName = type.FullName;
            var result = Resolve<IClassService>().GetListPropertys(fullName);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(typeof(ViewActivityPage))]
        [InlineData(typeof(ViewActivityProductPage))]
        [InlineData(typeof(AppSettingConfig))]
        [InlineData(typeof(MoneyTypeConfig))]
        [InlineData(typeof(TransferConfig))]
        [TestMethod("GetAllPropertys_String")]
        public void GetAllPropertys_String_test(Type type) {
            var fullName = type.FullName;
            var result = Resolve<IClassService>().GetAllPropertys(fullName);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(typeof(ViewActivityPage))]
        [InlineData(typeof(ViewActivityProductPage))]
        [InlineData(typeof(AppSettingConfig))]
        [InlineData(typeof(MoneyTypeConfig))]
        [InlineData(typeof(TransferConfig))]
        [TestMethod("GetEditPropertys_Type")]
        public void GetEditPropertys_Type_test(Type type) {
            var result = Resolve<IClassService>().GetEditPropertys(type);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(typeof(ViewActivityPage))]
        [InlineData(typeof(ViewActivityProductPage))]
        [InlineData(typeof(AppSettingConfig))]
        [InlineData(typeof(MoneyTypeConfig))]
        [InlineData(typeof(TransferConfig))]
        [TestMethod("GetListPropertys_Type")]
        public void GetListPropertys_Type_test(Type type) {
            var result = Resolve<IClassService>().GetListPropertys(type);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(typeof(ViewActivityPage))]
        [InlineData(typeof(ViewActivityProductPage))]
        [InlineData(typeof(AppSettingConfig))]
        [InlineData(typeof(MoneyTypeConfig))]
        [InlineData(typeof(TransferConfig))]
        [TestMethod("GetAllPropertys_Type")]
        public void GetAllPropertys_Type_test(Type type) {
            var result = Resolve<IClassService>().GetAllPropertys(type);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId) {
            //var model = Service<IAdminService>().GetRandom(entityId);
            //if (model != null)
            //{
            //    var newModel = Service<IAdminService>().GetSingleFromCache(model.Id);
            //    Assert.NotNull(newModel);
            //    Assert.Equal(newModel.Id, model.Id);
            //}
        } /*end*/
    }
}