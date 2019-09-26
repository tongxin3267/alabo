using Alabo.Framework.Basic.Notifications.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.Common.Domain.Services
{
    public class IMessageQueueServiceTests : CoreTest
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
        [TestMethod("Add_MessageQueue")]
        [TestIgnore]
        public void Add_MessageQueue_test()
        {
            //MessageQueue entity = null;
            //Service<IMessageQueueService>().Add( entity);
        }

        [Fact]
        [TestMethod("AddRawQueue_String_String_String")]
        public void AddRawQueue_String_String_String_test()
        {
            var mobile = "";
            var content = "";
            var ipAdress = "";
            Resolve<IMessageQueueService>().AddRawQueue(mobile, content, ipAdress);
        }

        [Fact]
        [TestMethod("AddTemplateQueue_Int64_String_String_IDictionary2")]
        public void AddTemplateQueue_Int64_String_String_IDictionary2_test()
        {
            var code = 0;
            var mobile = "";
            var ipAdress = "";
            Resolve<IMessageQueueService>().AddTemplateQueue(code, mobile, ipAdress);
        }

        [Fact]
        [TestMethod("Cancel_Int64")]
        public void Cancel_Int64_test()
        {
            var id = 0;
            Resolve<IMessageQueueService>().Cancel(id);
        }

        [Fact]
        [TestMethod("ErrorQueue_Int64_String_Exception")]
        [TestIgnore]
        public void ErrorQueue_Int64_String_Exception_test()
        {
            //var id = 0;
            //var message = "";
            //Exception exception = null;
            //Service<IMessageQueueService>().ErrorQueue( id, message, exception);
        }

        [Fact]
        [TestMethod("ErrorQueue_Int64_String_String")]
        public void ErrorQueue_Int64_String_String_test()
        {
            var id = 0;
            var message = "";
            var summary = "";
            Resolve<IMessageQueueService>().ErrorQueue(id, message, summary);
        }

        [Fact]
        [TestMethod("ErrorQueue_Int64_String")]
        public void ErrorQueue_Int64_String_test()
        {
            var id = 0;
            var message = "";
            Resolve<IMessageQueueService>().ErrorQueue(id, message);
        }

        [Fact]
        [TestMethod("GetSingle_Int64")]
        [TestIgnore]
        public void GetSingle_Int64_test()
        {
            //var id = 0;
            //var result = Service<IMessageQueueService>().GetSingle( id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUnHandledIdList")]
        public void GetUnHandledIdList_test()
        {
            var result = Resolve<IMessageQueueService>().GetUnHandledIdList();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("HandleQueue_Int64_String_String")]
        public void HandleQueue_Int64_String_String_test()
        {
            var id = 0;
            var message = "";
            var summary = "";
            Resolve<IMessageQueueService>().HandleQueue(id, message, summary);
        }

        [Fact]
        [TestMethod("HandleQueueAndUpdateContent_Int64_String_String")]
        public void HandleQueueAndUpdateContent_Int64_String_String_test()
        {
            var id = 0;
            var message = "";
            var summary = "";
            Resolve<IMessageQueueService>().HandleQueueAndUpdateContent(id, message, summary);
        }

        [Fact]
        [TestMethod("HandleQueueAsync_Int64")]
        public void HandleQueueAsync_Int64_test()
        {
            var queueId = 0;
            var result = Resolve<IMessageQueueService>().HandleQueueAsync(queueId);
            Assert.NotNull(result);
        }
    }
}