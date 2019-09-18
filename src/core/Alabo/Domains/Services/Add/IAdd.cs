using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.UnitOfWork;
using Alabo.Validations.Aspects;

namespace Alabo.Domains.Services.Add
{
    /// <summary>
    ///     创建操作
    /// </summary>
    public interface IAdd<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     创建
        /// </summary>
        /// <param name="request">创建参数</param>
        [UnitOfWork]
        bool Add<TRequest>([Valid] TRequest request) where TRequest : IRequest, new();

        /// <summary>
        ///     添加单个实体
        /// </summary>
        /// <param name="model"></param>
        bool Add([Valid] TEntity model);
    }
}