using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Runtime.Config {

    /// <summary>
    ///     数据库类型的字符串定义
    /// </summary>
    public static class DatabaseTypes {

        /// <summary>
        ///     PostgreSQL
        /// </summary>
        public const string PostgreSql = "postgresql";

        /// <summary>
        ///     SQLite
        /// </summary>
        public const string SqLite = "sqlite";

        /// <summary>
        ///     MySQL
        /// </summary>
        public const string MySql = "mysql";

        /// <summary>
        ///     MSSQL
        /// </summary>
        public const string Mssql = "mssql";

        /// <summary>
        ///     The mongo database
        /// </summary>
        public const string MongoDb = "mongodb";
    }

    /// <summary>
    ///     Enum DatabaseType
    /// </summary>
    [ClassProperty(Name = "数据库连接方式")]
    public enum DatabaseType {

        /// <summary>
        ///     SqlService
        /// </summary>
        [Display(Name = "SqlService")] Mssql = 1,

        /// <summary>
        ///     The mongo database
        /// </summary>
        [Display(Name = "MongoDb")] MongoDb = 2
    }
}