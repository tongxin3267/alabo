using Xunit;
using ZKCloud.Extensions;
using ZKCloud.Test.Base.Core;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Extensions
{
    public class DecimalExtensionTests : CoreTest
    {
        [Theory]
        [InlineData(1.02, 1.02, 3)]
        [InlineData(1.02, 1.02, 2)]
        [InlineData(1, 1, 2)]
        [TestMethod("EqualsDigits_Decimal_Decimal_Int32")]
        public void EqualsDigits_Decimal_Decimal_Int32_test(decimal value1, decimal value2, int digits)
        {
            var result = value1.EqualsDigits(value2, digits);
            Assert.True(result);
        }

        [Theory]
        [InlineData(1.02, 1.04, 3)]
        [InlineData(1.02, 1.04, 2)]
        [InlineData(1, 1.04, 2)]
        [TestMethod("LessThanDigits_Decimal_Decimal_Int32")]
        public void LessThanDigits_Decimal_Decimal_Int32_test(decimal value1, decimal value2, int digits)
        {
            var result = value1.LessThanDigits(value2, digits);
            Assert.True(result);
        }

        [Theory]
        [InlineData(1.12, 1.04, 3)]
        [InlineData(1.12, 1.04, 2)]
        [InlineData(1.1, 1.04, 2)]
        [TestMethod("MoreThanDigits_Decimal_Decimal_Int32")]
        public void MoreThanDigits_Decimal_Decimal_Int32_test(decimal value1, decimal value2, int digits)
        {
            var result = value1.MoreThanDigits(value2, digits);
            Assert.True(result);
        }

        /*end*/
    }
}