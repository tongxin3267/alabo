using System.Threading.Tasks;

namespace Alabo.Extensions {

    public static class TaskExtensions {

        private static T ToObject<T>(object source) {
            return (T)source;
        }

        /// <summary>
        ///     将同步转换成异步值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        public static async Task<T> ToObjectAsync<T>(this object source) {
            return await Task.FromResult(ToObject<T>(source));
        }

        /// <summary>
        ///     将异步值转为同步值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instanse"></param>
        public static string FromObject<T>(this T instanse) {
            return instanse as string;
        }

        private static async Task<string> FromObjectAsync<T>(T instanse) {
            return await Task.FromResult(instanse as string);
        }
    }
}