using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZKCloud.Schedules;

namespace ZKCloud.Test.Schedules
{
   public class TaskQueueModuleTest
    {
        [Fact]
        public void GetTaskQueueModuleId_Test()
        {
            var result = TaskQueueModule.GetTaskQueueModuleIds();
            Assert.NotNull(result);

        }
    }
}
