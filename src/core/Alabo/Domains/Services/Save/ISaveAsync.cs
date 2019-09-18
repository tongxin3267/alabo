using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.UnitOfWork;
using Alabo.Validations.Aspects;

namespace Alabo.Domains.Services.Save
{
    /// <summary>
    ///     保存操作
    /// </summary>
    /// <typeparam name="TRequest">参数类型</typeparam>
    public interface ISaveAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     保存
        /// </summary>
        /// <param name="request">参数</param>
        [UnitOfWork]
        Task SaveAsync<TRequest>([Valid] TRequest request) where TRequest : IRequest, IKey, new();

        Task<bool> AddOrUpdateAsync(TEntity model, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     更新或添加
        /// </summary>
        /// <param name="model"></param>
        Task<bool> AddOrUpdateAsync(TEntity model);

        /// <summary>
        ///     根据条件判断是添加还是更新数据库
        /// </summary>
        /// <param name="model"></param>
        /// <param name="predicate">True更新数据，False新增数据</param>
        Task<bool> AddOrUpdateAsync(TEntity model, bool predicate);
    }
}