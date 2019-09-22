using System;

namespace Alabo.Domains.Attributes
{
    /// <summary>
    ///     ///
    ///     <summary>
    ///         服务方法特性
    ///     </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    /// </summary>
    public class MethodAttribute : Attribute
    {
        /// <summary>
        ///     通过Url的方式来执行
        ///     可以
        /// </summary>
        public bool RunInUrl { get; set; } = false;
    }
}