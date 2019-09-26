﻿using Alabo.Extensions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Alabo.Regexs
{
    /// <summary>
    ///     正则严重
    /// </summary>
    public static class RegexHelper
    {
        public static System.Text.RegularExpressions.Regex Email { get; } =
            new System.Text.RegularExpressions.Regex(@"^[\w-]+@[\w-]+\.[\w-]+$");

        /// <summary>
        ///     中国手机号
        ///     RegexHelper.ChinaMobile.IsMatch(mobile)
        /// </summary>

        public static System.Text.RegularExpressions.Regex ChinaMobile { get; } =
            new System.Text.RegularExpressions.Regex(RegularExpressionHelper.ChinaMobile);

        /// <summary>
        ///     整数
        /// </summary>

        public static System.Text.RegularExpressions.Regex Digits { get; } =
            new System.Text.RegularExpressions.Regex(RegularExpressionHelper.Digits);

        /// <summary>
        ///     整数和小数
        /// </summary>
        public static System.Text.RegularExpressions.Regex Decimal { get; } =
            new System.Text.RegularExpressions.Regex(RegularExpressionHelper.Decimal);

        /// <summary>
        ///     电话号码
        /// </summary>
        public static System.Text.RegularExpressions.Regex Tel { get; } =
            new System.Text.RegularExpressions.Regex(RegularExpressionHelper.Tel);

        /// <summary>
        ///     验证是否为中国的手机号码
        /// </summary>
        /// <param name="mobile"></param>
        public static bool CheckMobile(string mobile)
        {
            if (mobile.IsNullOrEmpty()) return false;

            if (mobile.Length != 11) return false;

            var rg = new System.Text.RegularExpressions.Regex(RegularExpressionHelper.ChinaMobile);
            var m = rg.Match(mobile);
            return m.Success;
        }

        public static bool CheckEmail(string mail)
        {
            if (mail.IsNullOrEmpty()) return false;
            //if (mail.Length != 11) {
            //    return false;
            //}
            var rg = new System.Text.RegularExpressions.Regex(RegularExpressionHelper.Email);
            var m = rg.Match(mail);
            return m.Success;
        }

        /// <summary>
        ///     验证是否为六位数支付密码
        /// </summary>
        /// <param name="password"></param>
        public static bool CheckPayPasswrod(string password)
        {
            if (password.IsNullOrEmpty()) return false;

            if (password.Length != 6) return false;

            var rg = new System.Text.RegularExpressions.Regex(RegularExpressionHelper.Digits);
            var m = rg.Match(password);
            return m.Success;
        }

        /// <summary>
        ///     获取匹配值集合
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="resultPatterns">结果模式字符串数组,范例：new[]{"$1","$2"}</param>
        /// <param name="options">选项</param>
        public static Dictionary<string, string> GetValues(string input, string pattern, string[] resultPatterns,
            RegexOptions options = RegexOptions.IgnoreCase)
        {
            var result = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(input)) return result;

            var match = System.Text.RegularExpressions.Regex.Match(input, pattern, options);
            if (match.Success == false) return result;

            AddResults(result, match, resultPatterns);
            return result;
        }

        /// <summary>
        ///     添加匹配结果
        /// </summary>
        private static void AddResults(Dictionary<string, string> result, Match match, string[] resultPatterns)
        {
            if (resultPatterns == null)
            {
                result.Add(string.Empty, match.Value);
                return;
            }

            foreach (var resultPattern in resultPatterns) result.Add(resultPattern, match.Result(resultPattern));
        }

        /// <summary>
        ///     获取匹配值
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="resultPattern">结果模式字符串,范例："$1"用来获取第一个()内的值</param>
        /// <param name="options">选项</param>
        public static string GetValue(string input, string pattern, string resultPattern = "",
            RegexOptions options = RegexOptions.IgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            var match = System.Text.RegularExpressions.Regex.Match(input, pattern, options);
            if (match.Success == false) return string.Empty;

            return string.IsNullOrWhiteSpace(resultPattern) ? match.Value : match.Result(resultPattern);
        }

        /// <summary>
        ///     分割成字符串数组
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="options">选项</param>
        public static string[] Split(string input, string pattern, RegexOptions options = RegexOptions.IgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(input)) return new string[] { };

            return System.Text.RegularExpressions.Regex.Split(input, pattern, options);
        }

        /// <summary>
        ///     替换
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="replacement">替换字符串</param>
        /// <param name="options">选项</param>
        public static string Replace(string input, string pattern, string replacement,
            RegexOptions options = RegexOptions.IgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            return System.Text.RegularExpressions.Regex.Replace(input, pattern, replacement, options);
        }

        /// <summary>
        ///     验证输入与模式是否匹配
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>
        public static bool IsMatch(string input, string pattern)
        {
            return IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        ///     验证输入与模式是否匹配
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="options">选项</param>
        public static bool IsMatch(string input, string pattern, RegexOptions options)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, pattern, options);
        }
    }
}