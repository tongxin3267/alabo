namespace Alabo.Domains.Repositories.EFCore {

    public class MsSqlDbConnection {

        /// <summary>
        ///     链接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        ///     数据库名称
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 服务器
        /// </summary>
        public string ServerVersion { get; set; }
    }
}