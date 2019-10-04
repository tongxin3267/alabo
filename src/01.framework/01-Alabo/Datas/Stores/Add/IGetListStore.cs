using Alabo.Domains.Entities.Core;
using Alabo.Domains.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Alabo.Datas.Stores.Add {

    public interface IGetListStore<TEntity, TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity {

        /// <summary>
        ///     获取数据的数据列表
        /// </summary>
        IEnumerable<TEntity> GetList();

        /// <summary>
        ///     根据条件查询数据列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     根据条件查询数据列表
        /// </summary>
        /// <param name="query"></param>
        IEnumerable<TEntity> GetList(IExpressionQuery<TEntity> query);

        /// <summary>
        ///     获取所有的Id列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        IEnumerable<TKey> GetIdList(Expression<Func<TEntity, bool>> predicate = null);
    }
}