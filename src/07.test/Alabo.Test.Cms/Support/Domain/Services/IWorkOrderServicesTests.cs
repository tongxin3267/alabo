using Alabo.App.Cms.Support.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Cms.Support.Domain.Services {

    public class IWorkOrderServicesTests : CoreTest {

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(43)]
        [InlineData(54)]
        [InlineData(51)]
        [InlineData(52)]
        [InlineData(33)]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId) {
            var model = Resolve<IWorkOrderServices>().GetRandom(entityId);
            if (model != null) {
                var newModel = Resolve<IWorkOrderServices>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("AddOrUpdate_ViewWorkOrder")]
        public void AddOrUpdate_ViewWorkOrder_test() {
            //ViewWorkOrder view = null;
            //var result = Service<IWorkOrderServices>().AddOrUpdate(view);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestIgnore]
        [TestMethod("GetSingle_ObjectId")]
        public void GetSingle_ObjectId_test() {
            //  ObjectId id = ObjectId.Empty;
            //var result = Service<IWorkOrderServices>().GetSingle(id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestIgnore]
        [TestMethod("GetView_ObjectId")]
        public void GetView_ObjectId_test() {
            //ObjectId id = ObjectId.Empty;
            //var result = Service<IWorkOrderServices>().GetView(id);
            //Assert.NotNull(result);
        }

        /*end*/
    }
}