using Alabo.Web.Mvc.Attributes;

namespace Alabo.Datas.Enums
{
    /// <summary>
    ///     数据库类型
    /// </summary>
    [ClassProperty(Name = "数据库类型")]
    public enum DatabaseType
    {
        /// <summary>
        ///     Sql Server数据库
        /// </summary>
        SqlServer,

        /// <summary>
        ///     MySql数据库
        /// </summary>
        MySql,

        /// <summary>
        ///     PostgreSQL数据库
        /// </summary>
        PgSql
    }
}