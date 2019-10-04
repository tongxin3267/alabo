using Alabo.Web.Mvc.Attributes;

namespace Alabo.Datas.Ef.Configs {

    /// <summary>
    ///     Ef日志级别
    /// </summary>
    [ClassProperty(Name = "EF日志级别")]
    public enum EfLogLevel {

        /// <summary>
        ///     输出全部日志，包括连接数据库，提交事务等大量信息
        /// </summary>
        All,

        /// <summary>
        ///     仅输出Sql
        /// </summary>
        Sql,

        /// <summary>
        ///     关闭日志
        /// </summary>
        Off
    }
}