namespace Alabo.Regexs {

    /// <summary>
    ///     正则表达式
    /// </summary>
    public static class RegularExpressionHelper {

        /// <summary>
        ///     邮箱
        /// </summary>
        public const string Email = @"^[\w-]+@[\w-]+\.[\w-]+$";

        /// <summary>
        ///     中国手机号
        ///     * 手机号码:
        ///     * 13[0-9], 14[5,7], 15[0, 1, 2, 3, 5, 6, 7, 8, 9], 17[0, 1, 6, 7, 8], 18[0-9]
        ///     * 移动号段: 134,135,136,137,138,139,147,150,151,152,157,158,159,170,178,182,183,184,187,188
        ///     * 联通号段: 130,131,132,145,152,155,156,170,171,176,185,186
        ///     * 电信号段: 133,134,153,170,177,180,181,189
        /// </summary>
        public const string ChinaMobile = @"^0?(13[0-9]|15[0-9]|18[0-9]|17[0-9]|19[0-9]|16[0-9]|14[0-9])[0-9]{8}$";

        /// <summary>
        ///     整数
        /// </summary>
        public const string Digits = @"^-?[\d]+$";

        /// <summary>
        ///     整数和小数
        /// </summary>
        public const string Decimal = @"^-?[\d]+(\.[\d]+)?$";

        /// <summary>
        ///     大于0的数
        /// </summary>
        public const string DecimalThanZero = @"^(?!(0[0-9]{0,}$))[0-9]{1,}[.]{0,}[0-9]{0,}$";

        /// <summary>
        ///     电话号码
        /// </summary>
        public const string Tel = @"^\+?[\d\s-]+$";

        public const string UserName = @"^[a-z0-9A-Z_-]{3,16}$";
    }
}