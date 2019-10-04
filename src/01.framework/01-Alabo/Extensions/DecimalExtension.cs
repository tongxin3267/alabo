using System;

namespace Alabo.Extensions {

    public static class DecimalExtension {

        /// <summary>
        ///     Equalses the specified value2.
        ///     判断两个数字,在多少位范围内是否相等
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="digits">The digest.</param>
        public static bool EqualsDigits(this decimal value1, decimal value2, int digits = 2) {
            return Math.Round(value1, digits) == Math.Round(value2, digits);
        }

        /// <summary>
        ///     Equalses the specified value2.
        ///     判断两个数字,在多少位范围内，是否大于
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="digits">The digest.</param>
        public static bool MoreThanDigits(this decimal value1, decimal value2, int digits = 2) {
            return Math.Round(value1, digits) > Math.Round(value2, digits);
        }

        /// <summary>
        ///     Equalses the specified value2.
        ///     判断两个数字,在多少位范围内，是否小于
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="digits">The digest.</param>
        public static bool LessThanDigits(this decimal value1, decimal value2, int digits = 2) {
            return Math.Round(value1, digits) < Math.Round(value2, digits);
        }
    }
}