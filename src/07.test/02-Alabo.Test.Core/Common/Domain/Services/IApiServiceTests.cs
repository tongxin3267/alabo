using Alabo.Framework.Core.WebApis.Service;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.Common.Domain.Services
{
    public class IApiServiceTests : CoreTest
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
        [TestMethod("ApiImageUrl_String")]
        public void ApiImageUrl_String_test()
        {
            var imageUrl = "";
            var result = Resolve<IApiService>().ApiImageUrl(imageUrl);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ApiUserAvator_Int64")]
        public void ApiUserAvator_Int64_test()
        {
            var userId = 0;
            var result = Resolve<IApiService>().ApiUserAvator(userId);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ConvertToApiImageUrl_String")]
        public void ConvertToApiImageUrl_String_test()
        {
            var content = "";
            var result = Resolve<IApiService>().ConvertToApiImageUrl(content);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("CreateApiUrl")]
        [TestIgnore]
        public void CreateApiUrl_test()
        {
            //var result = Service<IApiService>().CreateApiUrl();
            //         Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAllApiUrl")]
        [TestIgnore]
        public void GetAllApiUrl()
        {
            var result = Resolve<IApiService>().GetAllApiUrl();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAllApiUrl")]
        [TestIgnore]
        public void GetAllApiUrl_test()
        {
            //var result = Service<IApiService>().GetAllApiUrl();
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("InstanceToApiImageUrl_Object")]
        [TestIgnore]
        public void InstanceToApiImageUrl_Object_test()
        {
            //Object instance = null;
            //var result = Service<IApiService>().InstanceToApiImageUrl( instance);
            //Assert.NotNull(result);
        }
    }
}