using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.UnitOfWork;
using Alabo.Validations.Aspects;
using System;
using System.Linq.Expressions;

namespace Alabo.Domains.Services.Save {

    /// <summary>
    ///     保存操作
    /// </summary>
    /// <typeparam name="TRequest">参数类型</typeparam>
    public interface ISave<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     保存
        /// </summary>
        /// <param name="request">参数</param>
        [UnitOfWork]
        void Save<TRequest>([Valid] TRequest request) where TRequest : IRequest, IKey, new();

        /// <summary>
        ///     根据条件判断是添加还是更新数据库
        /// </summary>
        /// <param name="model"></param>
        /// <param name="predicate">True更新数据，False新增数据</param>
        bool AddOrUpdate(TEntity model, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     更新或添加
        /// </summary>
        /// <param name="model"></param>
        bool AddOrUpdate(TEntity model);

        /// <summary>
        ///     根据条件判断是添加还是更新数据库
        /// </summary>
        /// <param name="model"></param>
        /// <param name="predicate">True更新数据，False新增数据</param>
        bool AddOrUpdate(TEntity model, bool predicate);
    }
}