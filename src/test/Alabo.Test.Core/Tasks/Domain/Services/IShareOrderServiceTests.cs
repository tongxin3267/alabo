using Xunit;
using Alabo.App.Core.Tasks.Domain.Entities.Extensions;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Tasks.Domain.Services
{
    public class IShareOrderServiceTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IShareOrderService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IShareOrderService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        } /*end*/

        [Fact]
        [TestMethod("AddSingle_ShareOrder")]
        [TestIgnore]
        public void AddSingle_ShareOrder_test()
        {
            //ShareOrder shareOrder = null;
            //var result = Service<IShareOrderService>().AddSingle( shareOrder);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("AddTaskMessage_Int64_TaskMessage")]
        public void AddTaskMessage_Int64_TaskMessage_test()
        {
            var orderId = 0;
            TaskMessage taskMessage = null;
            Resolve<IShareOrderService>().AddTaskMessage(orderId, taskMessage);
        }

        [Fact]
        [TestMethod("Delete_Int64")]
        public void Delete_Int64_test()
        {
            var id = 0;
            var result = Resolve<IShareOrderService>().Delete(id);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPageList_Object")]
        public void GetPageList_Object_test()
        {
            object query = null;
            var result = Resolve<IShareOrderService>().GetPageList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_Int64")]
        [TestIgnore]
        public void GetSingle_Int64_test()
        {
            //var id = 0;
            //var result = Service<IShareOrderService>().GetSingle( id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingleNative_Int64")]
        public void GetSingleNative_Int64_test()
        {
            var id = 0;
            var result = Resolve<IShareOrderService>().GetSingleNative(id);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUnHandledIdList")]
        public void GetUnHandledIdList_test()
        {
            var result = Resolve<IShareOrderService>().GetUnHandledIdList();
            Assert.NotNull(result);
        }
    }
}