using System;
using Alabo.Framework.Basic.Notifications.Job;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.User
{
    public class MessageQueueScheduleTest : BaseScheduleTest
    {
        private MessageQueueJob messageQueueSchedule;

        private readonly IServiceProvider service;

        [Fact]
        [TestIgnore]
        public void GetTemplateTest()
        {
            //long code = 456470;
            //var messageTemplate = messageQueueSchedule.GetTemplate(code);
            //Assert.NotNull(messageTemplate);
            //Assert.Equal(messageTemplate.TemplateCode, code);
        } /*end*/
    }
}