using System;
using Xunit;
using ZKCloud.Extensions;
using ZKCloud.Test.Base.Core;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Extensions
{
    public class GuidExtensionsTests : CoreTest
    {
        [Fact]
        [TestMethod("GuidId")]
        public void GuidId_test()
        {
            var result = GuidExtensions.GuidId();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("IsEqual_Guid_Guid")]
        public void IsEqual_Guid_Guid_test()
        {
            var input = Guid.Empty;
            var guid = Guid.Empty;
            var result = input.IsEqual(guid);
            Assert.True(result);
        }

        [Fact]
        [TestMethod("IsNotEqual_Guid_Guid")]
        public void IsNotEqual_Guid_Guid_test()
        {
            var input = Guid.NewGuid();
            var guid = Guid.NewGuid();
            var result = input.IsNotEqual(guid);
            Assert.True(result);
        }

        [Fact]
        [TestMethod("IsNull_Guid")]
        public void IsNull_Guid_test()
        {
            var input = Guid.Empty;
            var result = input.IsNull();
            Assert.True(result);
        }

        /*end*/
    }
}