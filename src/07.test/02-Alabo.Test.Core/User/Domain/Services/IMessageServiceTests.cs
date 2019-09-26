using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.User.Domain.Services
{
    public class IMessageServiceTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            //var model = Service<IMessageService>().GetRandom(entityId);
            //if (model != null)
            //{
            //    var newModel = Service<IMessageService>().GetSingleFromCache(model.Id);
            //    Assert.NotNull(newModel);
            //    Assert.Equal(newModel.Id, model.Id);
            //}
        } /*end*/

        [Fact]
        [TestMethod("GetMymessage_Int64")]
        [TestIgnore]
        public void GetMymessage_Int64_test()
        {
            //var ReceiverUserId = 0;
            //var result = Service<IMessageService>().GetMymessage( ReceiverUserId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("SendMsg_Int64_String_String")]
        public void SendMsg_Int64_String_String_test()
        {
            //var userid = 0;
            //var receiverUserName = "";
            //var Content = "";
            //var result = Service<IMessageService>().SendMsg(userid, receiverUserName, Content);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("SendMsgAll_Int64_List1_MessageType_String_String_Boolean")]
        public void SendMsgAll_Int64_List1_MessageType_String_String_Boolean_test()
        {
            //var userid = 0;
            //var messageType = (MessageType) 0;
            //var Title = "";
            //var Content = "";
            //var canReply = false;
            //var result = Service<IMessageService>()
            //    .SendMsgAll(userid, new List<long>(), messageType, Title, Content, canReply);
            //Assert.NotNull(result);
        }
    }
}