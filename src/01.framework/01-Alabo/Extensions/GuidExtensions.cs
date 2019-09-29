using System;

namespace Alabo.Extensions
{
    public static class GuidExtensions
    {
        /// <summary>
        ///     用Guid创建Id,去掉分隔符
        /// </summary>
        public static string GuidId()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        ///     判断Guid是否为空
        /// </summary>
        /// <param name="input"></param>
        public static bool IsNull(this Guid input)
        {
            if (input.ToString() == "00000000-0000-0000-0000-000000000000") return true;

            return false;
        }

        /// <summary>
        ///     GUID 的比较，已忽略大小写
        /// </summary>
        /// <param name="input"></param>
        /// <param name="guid"></param>
        public static bool IsEqual(this Guid input, Guid guid)
        {
            return guid.ToString().ToLower() == input.ToString().ToLower();
        }

        /// <summary>
        ///     GUID 是否不一样，已忽略大小写
        /// </summary>
        /// <param name="input"></param>
        /// <param name="guid"></param>
        public static bool IsNotEqual(this Guid input, Guid guid)
        {
            return guid.ToString().ToLower() != input.ToString().ToLower();
        }
    }
}