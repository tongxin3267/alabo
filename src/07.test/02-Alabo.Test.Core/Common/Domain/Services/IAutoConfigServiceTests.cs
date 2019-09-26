using System;
using Alabo.App.Asset.Transfers.Domain.Configs;
using Xunit;
using Alabo.AutoConfigs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Industry.Shop.Activitys.ViewModels;
using Alabo.Runtime.Config;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Common.Domain.Services {

    public class IAutoConfigServiceTests : CoreTest {

        [Theory]
        [InlineData(typeof(ViewActivityPage))]
        [InlineData(typeof(ViewActivityProductPage))]
        //[InlineData(typeof(WebsiteConfig))] 有问题

        [InlineData(typeof(MoneyTypeConfig))]
        [InlineData(typeof(TransferConfig))]
        [TestMethod("GetValue_Type_Guid")]
        public void GetValue_Type_Guid_test(Type type) {
            var id = Guid.Empty;
            var result = Resolve<IAutoConfigService>().GetValue(type, id);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(typeof(ViewActivityPage))]
        [InlineData(typeof(ViewActivityProductPage))]
        [InlineData(typeof(AppSettingConfig))]
        //[InlineData(typeof(QrCodeConfig))]有问题
        [InlineData(typeof(MoneyTypeConfig))]
        [InlineData(typeof(TransferConfig))]
        [TestMethod("GetObjectList_Type")]
        public void GetObjectList_Type_test(Type type) {
            var result = Resolve<IAutoConfigService>().GetObjectList(type);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId) {
            var model = Resolve<IAutoConfigService>().GetRandom(entityId);
            if (model != null) {
                var newModel = Resolve<IAutoConfigService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        } /*end*/

        [Fact]
        [TestMethod("AddOrUpdate_AutoConfig")]
        [TestIgnore]
        public void AddOrUpdate_AutoConfig_test() {
            //AutoConfig config = null;
            //Service<IAutoConfigService>().AddOrUpdate(config);
        }

        [Fact]
        [TestMethod("AddOrUpdate_Object")]
        public void AddOrUpdate_Object_test() {
            //Object value = null;
            //var result = Service<IAutoConfigService>().AddOrUpdate(value);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Check_String")]
        [TestIgnore]
        public void Check_String_test() {
            //var script = "";
            //var result = Service<IAutoConfigService>().Check(script);
            //Assert.True(result);
        }

        /// <summary>
        ///     Configurations the URL test.
        ///     ���е��������Ӳ���
        /// </summary>
        [Fact]
        [TestIgnore]
        public void ConfigUrl_Test() {
            //var result = Service<IAutoConfigService>().GetAllTypes();
            //foreach (var item in result) {
            //    var attribute = item.GetAttribute<ClassPropertyAttribute>();
            //    if (attribute != null) {
            //        if (attribute.PageType == ViewPageType.Edit) {
            //            var url = $"Admin/AutoConfig/Edit?key={item.FullName}";
            //            AccessAdminUrl(url);
            //        }
            //        if (attribute.PageType == ViewPageType.List) {
            //            var url = $"Admin/AutoConfig/Edit?key={item.FullName}";
            //            AccessAdminUrl(url);
            //            url = $"Admin/AutoConfig/Edit?List={item.FullName}";
            //            AccessAdminUrl(url);
            //        }
            //    }
            //}
        }

        [Fact]
        [TestMethod("FromIEnumerable_IEnumerable1_Func2_Func2")]
        public void FromIEnumerable_IEnumerable1_Func2_Func2_test() {
            //var result = Service<IAutoConfigService>().FromIEnumerable(null, null, null);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAllTypes")]
        public void GetAllTypes_test() {
            var result = Resolve<IAutoConfigService>().GetAllTypes();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetConfig_String")]
        [TestIgnore]
        public void GetConfig_String_test() {
            //var key = "";
            //var result = Service<IAutoConfigService>().GetConfig(key);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetList_Func2_Func2_Func2")]
        public void GetList_Func2_Func2_Func2_test() {
            //var result = Service<IAutoConfigService>().GetList(null, null, null);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetList_Func2")]
        [TestIgnore]
        public void GetList_Func2_test() {
            //var result = Service<IAutoConfigService>().GetList(null);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetList_String")]
        [TestIgnore]
        public void GetList_String_test() {
            //var key = "";
            //var result = Service<IAutoConfigService>().GetList(key);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetTypeByName_String")]
        [TestIgnore]
        public void GetTypeByName_String_test() {
            //var name = "";
            //var result = Service<IAutoConfigService>().GetTypeByName(name);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetValue_String")]
        [TestIgnore]
        public void GetValue_String_test() {
            //var key = "";
            //var result = Service<IAutoConfigService>().GetValue(key);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetValue")]
        public void GetValue_test() {
            var result = Resolve<IAutoConfigService>().GetValue<WebSiteConfig>();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("InitDefaultData")]
        public void InitDefaultData_test() {
            Resolve<IAutoConfigService>().InitDefaultData();
        }

        [Fact]
        [TestMethod("MoneyTypes")]
        public void MoneyTypes_test() {
            var result = Resolve<IAutoConfigService>().MoneyTypes();
            Assert.NotNull(result);
        }
    }
}