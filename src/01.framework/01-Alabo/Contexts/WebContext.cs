using Alabo.Helpers;

namespace Alabo.Contexts
{
    /// <summary>
    ///     Web上下文
    /// </summary>
    public class WebContext : IContext
    {
        /// <summary>
        ///     跟踪号
        /// </summary>
        public string TraceId => HttpWeb.HttpContext?.TraceIdentifier;

        /// <summary>
        ///     添加对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">对象</param>
        public void Add<T>(string key, T value)
        {
            if (HttpWeb.HttpContext == null) {
                return;
            }

            HttpWeb.HttpContext.Items[key] = value;
        }

        /// <summary>
        ///     获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">键名</param>
        public T Get<T>(string key)
        {
            if (HttpWeb.HttpContext == null) {
                return default;
            }

            return Convert.To<T>(HttpWeb.HttpContext.Items[key]);
        }

        /// <summary>
        ///     移除对象
        /// </summary>
        /// <param name="key">键名</param>
        public void Remove(string key)
        {
            HttpWeb.HttpContext?.Items.Remove(key);
        }
    }
}