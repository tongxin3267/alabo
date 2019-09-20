using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.UnitOfWork;
using Alabo.Validations.Aspects;

namespace Alabo.Domains.Services.Update
{
    /// <summary>
    ///     修改操作
    /// </summary>
    public interface IUpdateAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     修改
        /// </summary>
        /// <param name="request">修改参数</param>
        [UnitOfWork]
        Task<bool> UpdateAsync<TUpdateRequest>([Valid] TUpdateRequest request) where TUpdateRequest : IRequest, new();

        /// <summary>
        ///     更新单个实体
        /// </summary>
        /// <param name="model"></param>
        Task<bool> UpdateAsync([Valid] TEntity model);

        /// <summary>
        ///     更新单个实体
        /// </summary>
        /// <param name="updateAction"></param>
        /// <param name="predicate">查询条件</param>
        Task<bool> UpdateAsync(Action<TEntity> updateAction, Expression<Func<TEntity, bool>> predicate = null);
    }
}