namespace Alabo.Domains.Repositories.Mongo.Context
{
    /// <summary>
    ///     mongo数据库链接对象
    /// </summary>
    public class MongoDbConnection
    {
        /// <summary>
        ///     链接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        ///     数据库名称
        /// </summary>
        public string Database { get; set; }
    }
}