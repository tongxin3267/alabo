using System;
using System.Collections.Generic;
using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Time;

namespace Alabo.Domains.Services.Tree
{
    public abstract class TreeBase<TEntity, TKey> : TimeBase<TEntity, TKey>, ITree<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        protected TreeBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public void SwapSort()
        {
            throw new NotImplementedException();
        }

        public List<string> GetMissingParentIds(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
            //return Store.GetMissingParentIds(entities);
        }
    }
}