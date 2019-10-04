namespace Alabo.Domains.Repositories.Mongo.Context {

    /// <summary>
    ///     mongo数据库链接对象
    /// </summary>
    public class MongoDbConnection {

        /// <summary>
        ///     链接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        ///     数据库名称
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// 是否拥有Mongodb数据库的root权限
        /// 设置为true时，如果该拥有mongodb数据库所有权限，则可以访问当前服务器下所有的数据库
        /// </summary>
        public bool IsRoot { get; set; } = false;
    }
}