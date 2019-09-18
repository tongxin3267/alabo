using System.Data.Common;

namespace Alabo.Domains.Repositories.Model
{
    /// <summary>
    ///     批量执行Sql语句参数
    /// </summary>
    public class BatchParameter
    {
        /// <summary>
        ///     Gets or sets the SQL. sql语句
        /// </summary>
        /// <value>The SQL.</value>
        public string Sql { get; set; }

        /// <summary>
        ///     Gets or sets the parameters. sql参数
        /// </summary>
        /// <value>The parameters.</value>
        public DbParameter[] Parameters { get; set; }
    }
}