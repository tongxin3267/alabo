namespace Alabo.Dynamics {

    /// <summary>
    ///     动态调用方法
    /// </summary>
    public class BaseServiceMethod {

        /// <summary>
        ///     服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        ///     方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        ///     参数
        /// </summary>
        public object Parameter { get; set; }
    }
}