using Alabo.Domains.Entities;

namespace Alabo.Domains.Services.Random
{
    public interface IRandom<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     从数据库中随机读取一条记录
        ///     一般用于单元测试
        ///     1：表示第一条记录
        ///     -1：表示最后一条记录
        ///     其他随机读取
        /// </summary>
        /// <param name="id">Id标识</param>
        TEntity GetRandom(long id);
    }
}