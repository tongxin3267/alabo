using System;
using Alabo.App.Share.TaskExecutes.Domain.Services;
using Alabo.Framework.Tasks.Queues.Models;
using Microsoft.AspNetCore.Http;
using Xunit;
using ZKCloud.Open.Share.Models;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Tasks.Domain.Services
{
    public class ITaskModuleConfigServiceTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            //var model = Service<IAdminService>().GetRandom(entityId);
            //if (model != null)
            //{
            //    var newModel = Service<IAdminService>().GetSingleFromCache(model.Id);
            //    Assert.NotNull(newModel);
            //    Assert.Equal(newModel.Id, model.Id);
            //}
        } /*end*/

        [Fact]
        [TestMethod("AddOrUpdate_HttpContext_ShareModule_Type_IModuleConfig")]
        public void AddOrUpdate_HttpContext_ShareModule_Type_IModuleConfig_test()
        {
            HttpContext context = null;
            ShareModule shareModule = null;
            Type moduleType = null;
            IModuleConfig config = null;
            var result = Resolve<ITaskModuleConfigService>().AddOrUpdate(context, shareModule, moduleType, config);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Delete_HttpContext_Int64")]
        [TestIgnore]
        public void Delete_HttpContext_Int64_test()
        {
            //HttpContext context = null;
            //var id = 0;
            //var result = Service<ITaskModuleConfigService>().Delete(context, id);
            //Assert.True(result);
        }

        [Fact]
        [TestMethod("GetList_HttpContext")]
        [TestIgnore]
        public void GetList_HttpContext_test()
        {
            //HttpContext context = null;
            //var result = Service<ITaskModuleConfigService>().GetList(context);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetList")]
        public void GetList_test()
        {
            //var result = Service<ITaskModuleConfigService>().GetList();
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetModuleAttribute_Type")]
        [TestIgnore]
        public void GetModuleAttribute_Type_test()
        {
            //Type moduleType = null;
            //var result = Service<ITaskModuleConfigService>().GetModuleAttribute(moduleType);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_HttpContext_Int64")]
        [TestIgnore]
        public void GetSingle_HttpContext_Int64_test()
        {
            //HttpContext context = null;
            //var id = 0;
            //var result = Service<ITaskModuleConfigService>().GetSingle(context, id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("LockShareModuleAsync_HttpContext_Int64_Object")]
        public void LockShareModuleAsync_HttpContext_Int64_Object_test()
        {
            HttpContext context = null;
            var id = 0;
            object configValue = null;
            var result = Resolve<ITaskModuleConfigService>().LockShareModuleAsync(context, id, configValue);
            Assert.NotNull(result);
        }
    }
}