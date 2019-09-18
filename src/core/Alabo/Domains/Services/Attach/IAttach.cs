using Alabo.Domains.Entities;
using Alabo.Runtime.Config;

namespace Alabo.Domains.Services.Attach
{
    public interface IAttach<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     获取表名
        /// </summary>
        string GetTableName();

        /// <summary>
        ///     获取数据类型
        /// </summary>
        DatabaseType GetDatabaseType();
    }
}