using Alabo.Domains.Entities;
using System.Collections.Generic;

namespace Alabo.Domains.Services.Tree {

    public interface ITree<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     交换排序
        /// </summary>
        void SwapSort();

        /// <summary>
        ///     获取缺失的父标识列表
        /// </summary>
        /// <param name="entities"></param>
        List<string> GetMissingParentIds(IEnumerable<TEntity> entities);
    }
}