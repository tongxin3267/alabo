using Alabo.Datas.Enums;
using Alabo.Domains.Entities;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Attach {

    public interface IAttachAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey> {

        Task<string> GetTableName();

        /// <summary>
        ///     获取数据类型
        /// </summary>
        Task<DatabaseType> GetDatabaseType();
    }
}