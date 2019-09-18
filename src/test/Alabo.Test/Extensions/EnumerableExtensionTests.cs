using System.Collections.Generic;
using Xunit;
using ZKCloud.Extensions;

namespace ZKCloud.Test.Extensions
{
    public class EnumerableExtensionTests
    {
        [Fact]
        public void IsDistinctTest()
        {
            var list = new List<string>
            {
                "A",
                "B",
                "C"
            };
            Assert.True(list.IsDistinct());

            list.Add("C");
            Assert.False(list.IsDistinct());
        } /*end*/
    }
}