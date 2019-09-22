using Xunit;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Common.Domain.Services
{
    public class IMessageQueueServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
        } /*end*/
    }
}