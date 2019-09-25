namespace Alabo.Validations
{
    /// <summary>
    ///     ErrorMessageFormats 类存放格式化错误消息
    ///     https://www.cnblogs.com/lonelyxmas/p/9065756.html
    ///     正则替换示列： ErrorMessage = "(\w*)格式错误"   替换:ErrorMessage = ErrorMessage.NotMatchFormat
    ///     ErrorMessage = "(\w*)格式错误"   替换:ErrorMessage = ErrorMessage.NotMatchFormat
    /// </summary>
    public class ErrorMessage
    {
        /// <summary>
        ///     "{0}不能为空"
        /// </summary>
        public const string NameNotAllowEmpty = "{0}不能为空";

        /// <summary>
        ///     "{0}不能为空"
        /// </summary>
        public const string NameNotCorrect = "{0}不正确 ";

        /// <summary>
        ///     The maximum string length
        ///     {0}输入长度不能超过{1}
        /// </summary>
        public const string MaxStringLength = "{0}输入长度不能超过{1}";

        /// <summary>
        ///     The minimum string length
        ///     {0}输入长度不能小于超过{1}
        /// </summary>
        public const string MinStringLength = "{0}输入长度不能小于{1}";

        /// <summary>
        ///     The maximum string length
        ///     {0}大小需在{1}-{2}之间
        /// </summary>
        public const string NameNotInRang = "{0}大小需在{1}-{2}之间";

        /// <summary>
        ///     The string length
        ///     {0}输入长度需在{1}-{2}之间
        /// </summary>
        public const string StringLength = "{0}输入长度需在{1}-{2}之间";

        /// <summary>
        ///     The double in rang、
        ///     {0}必须大于等于{1}
        /// </summary>
        public const string DoubleInRang = "{0}必须大于等于{1}";

        /// <summary>
        ///     {0}不能小于等于0
        /// </summary>
        public const string LessThanOrEqualZero = "{0}不能小于等于0";

        /// <summary>
        ///     The not match format
        ///     {0}格式不正确
        /// </summary>
        public const string NotMatchFormat = "{0}格式不正确";

        /// <summary>
        ///     The is userd
        ///     {0}已被使用
        /// </summary>
        public const string IsUserd = "{0}已被使用";
    }
}