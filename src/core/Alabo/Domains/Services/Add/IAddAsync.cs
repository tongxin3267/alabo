using System.Threading.Tasks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.UnitOfWork;
using Alabo.Validations.Aspects;

namespace Alabo.Domains.Services.Add
{
    /// <summary>
    ///     创建操作
    /// </summary>
    public interface IAddAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     创建
        /// </summary>
        /// <param name="request">创建参数</param>
        [UnitOfWork]
        Task<string> AddAsync<TRequest>([Valid] TRequest request) where TRequest : IRequest, new();

        /// <summary>
        ///     添加实体，异步
        /// </summary>
        /// <param name="entity">实体</param>
        Task AddAsync([Valid] TEntity entity);
    }
}