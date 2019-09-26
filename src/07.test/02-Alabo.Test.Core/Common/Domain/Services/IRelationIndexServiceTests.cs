using Alabo.Framework.Basic.Relations.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.Common.Domain.Services
{
    public class IRelationIndexServiceTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IRelationIndexService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IRelationIndexService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        /*end*/

        [Fact]
        [TestMethod("AddUpdateOrDelete_Int64_String_String")]
        public void AddUpdateOrDelete_Int64_String_String_test()
        {
            var entityId = 0;
            var relationIds = "";
            var type = "";
            var result = Resolve<IRelationIndexService>().AddUpdateOrDelete(entityId, relationIds, type);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("AddUpdateOrDelete_Int64_String")]
        public void AddUpdateOrDelete_Int64_String_test()
        {
            //var entityId = 0;
            //var relationIds = "";
            //var result = Service<IRelationIndexService>().AddUpdateOrDelete( entityId, relationIds);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("AddUpdateOrDelete_String_Int64_String")]
        public void AddUpdateOrDelete_String_Int64_String_test()
        {
            var fullName = "";
            var entityId = 0;
            var relationIds = "";
            var result = Resolve<IRelationIndexService>().AddUpdateOrDelete(fullName, entityId, relationIds);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetEntityIds_String")]
        public void GetEntityIds_String_test()
        {
            var ids = "";
            var result = Resolve<IRelationIndexService>().GetEntityIds(ids);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetRelationIds_Int64")]
        public void GetRelationIds_Int64_test()
        {
            //var entityId = 0;
            //var result = Service<IRelationIndexService>().GetRelationIds( entityId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetRelationIds_String_Int64")]
        public void GetRelationIds_String_Int64_test()
        {
            var type = "";
            var entityId = 0;
            var result = Resolve<IRelationIndexService>().GetRelationIds(type, entityId);
            Assert.NotNull(result);
        }
    }
}