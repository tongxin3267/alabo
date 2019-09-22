using System;
using Xunit;
using Alabo.App.Core.Common.Job;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core.Model;

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